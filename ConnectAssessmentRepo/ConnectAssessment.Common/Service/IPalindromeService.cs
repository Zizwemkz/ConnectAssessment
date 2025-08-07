using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectAssessment.Common.Service
{
    public interface IPalindromeService
    {
        bool IsPalindrome(string input, out string normalized);
    }
}
