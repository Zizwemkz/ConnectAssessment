using ConnectAssessment.Common.Repository;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Data.Models.Entities;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Data.Models.Responss;

public class CustomerSettlementService : ICustomerSettlementService
{
    private readonly ICustomerRepository _customerRepo;
    private readonly ISettlementRepository _settlementRepo;
    private readonly IBankApiService _bankApi;
    private readonly ITransactionFeeService _feeService;

    public CustomerSettlementService(
        ICustomerRepository customerRepo,
        ISettlementRepository settlementRepo,
        IBankApiService bankApi,
        ITransactionFeeService feeService)
    {
        _customerRepo = customerRepo;
        _settlementRepo = settlementRepo;
        _bankApi = bankApi;
        _feeService = feeService;
    }

    public async Task<SettleCustomerResponse> SettleCustomerAsync(SettleCustomerRequest request)
    {
        var customer = await _customerRepo.GetByIdAsync(request.CustomerId);
        if (customer == null)
            return new SettleCustomerResponse { Success = false, Message = "tbCustomer not found" };

        var fee = _feeService.CalculateFee(request.Amount);
        var toTransfer = request.Amount - fee;

        var success = await _bankApi.TransferFundsAsync(customer.AccountNumber, toTransfer);

        var settlement = new tbSettlementTransaction
        {
            CustomerId = customer.Id,
            Amount = request.Amount,
            TransactionFee = fee,
            Date = DateTime.UtcNow,
            Success = success
        };
        await _settlementRepo.AddAsync(settlement);

        return new SettleCustomerResponse
        {
            Success = success,
            TransactionFee = fee,
            Message = success ? "Settled successfully" : "Failed to settle"
        };
    }
}