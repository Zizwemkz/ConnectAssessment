using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ConnectAssessment.Common.Service;
using ConnectAssessment.Data.Models.Responss;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Data.Models.Entities;

[ApiController]
[Route("api/[controller]")]
public class CustomerSettlementController : ControllerBase
{
    private readonly ICustomerSettlementService _settlementService;
    public CustomerSettlementController(ICustomerSettlementService settlementService) => _settlementService = settlementService;

    [HttpPost("settle")]
    public async Task<ActionResult<SettleCustomerResponse>> SettleCustomer([FromBody] SettleCustomerRequest request)
    {
        var result = await _settlementService.SettleCustomerAsync(request);
        return Ok(result);
    }   
}