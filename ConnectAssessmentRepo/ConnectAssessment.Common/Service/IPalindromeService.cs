using System;

namespace ConnectAssessment.Common.Service
{
    public interface IPalindromeService
    {
        bool IsPalindrome(string input, out string normalized);
    }
}
