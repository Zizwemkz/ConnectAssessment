using ConnectAssessment.Common.Service;

namespace ConnectAssessment.Service
{
    public class PalindromeService : IPalindromeService
    {
        public bool IsPalindrome(string input, out string normalized)
        {
            normalized = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(input))
                    throw new ArgumentException("Input cannot be null, empty, or whitespace.", nameof(input));

                normalized = new string(input
                    .Where(char.IsLetterOrDigit)
                    .Select(char.ToLower)
                    .ToArray());

                return normalized.SequenceEqual(normalized.Reverse());
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while checking palindrome.", ex);
            }
        }
    }
}
