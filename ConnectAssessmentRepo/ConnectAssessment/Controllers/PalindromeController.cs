using ConnectAssessment.Common.Service;
using ConnectAssessment.Data.Models.Requests;
using ConnectAssessment.Data.Models.Responss;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PalindromeController : ControllerBase
{
    private readonly IPalindromeService _palindromeService;
    public PalindromeController(IPalindromeService palindromeService) => _palindromeService = palindromeService;

    [HttpPost("Palindrome")]
    public ActionResult<PalindromeResponse> CheckPalindrome([FromBody] PalindromeRequest request)
    {
        var isPalindrome = _palindromeService.IsPalindrome(request.statement, out var normalized);
        return Ok(new PalindromeResponse { IsPalindrome = isPalindrome, Normalized = normalized, statement = request .statement});
    }
}