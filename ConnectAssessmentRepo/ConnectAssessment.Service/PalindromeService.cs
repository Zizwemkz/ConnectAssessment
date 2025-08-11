using ConnectAssessment.Common.Service;

namespace ConnectAssessment.Service
{
    public class PalindromeService : IPalindromeService
    {
        private const string NullOrWhitespaceMessage = "Input cannot be null, empty, or whitespace.";
        private const string PalindromeMessage = "is a palindrome";
        private const string NotPalindromeMessage = "is not a palindrome";

        public bool IsPalindrome(string statement, out string normalized)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(statement))
                    throw new ArgumentException(NullOrWhitespaceMessage, nameof(statement));

                normalized = new string(statement
                    .Where(char.IsLetterOrDigit)
                    .Select(char.ToLower)
                    .ToArray());

                var isPalindrome = normalized.SequenceEqual(normalized.Reverse());
                normalized = isPalindrome ? $"{normalized +" : "+ PalindromeMessage}" : $"{normalized +" : "+ NotPalindromeMessage}";
                return isPalindrome;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while checking palindrome.", ex);
            }
        }
    }
}
