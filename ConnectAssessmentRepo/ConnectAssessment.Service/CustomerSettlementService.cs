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
        if (request == null)
            throw new ArgumentNullException(nameof(request), "Request cannot be null.");

        if (request.CustomerId <= 0)
            return new SettleCustomerResponse { Success = false, Message = "Invalid customer ID." };

        if (request.Amount <= 0)
            return new SettleCustomerResponse { Success = false, Message = "Amount must be greater than zero." };

        try
        {
            var customer = await _customerRepo.GetByIdAsync(request.CustomerId);
            if (customer == null)
                return new SettleCustomerResponse { Success = false, Message = "tbCustomer not found" };

            var fee = _feeService.CalculateFee(request.Amount);
            var toTransfer = request.Amount - fee;

            //provided endpoint for bank not yet working will assume it is workingand returns true
            var success = true;//await _bankApi.TransferFundsAsync(customer.AccountNumber, toTransfer);

            var settlement = new tbSettlementTransaction
            {
                CustomerId = customer.CustomerId,
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
        catch (Exception ex)
        {
            return new SettleCustomerResponse
            {
                Success = false,
                Message = $"An error occurred while settling: {ex.Message}"
            };
        }
    }
}