public class PalindromeService : IPalindromeService
{
    public string GetLongestPalindromicSubstring(string s)
    {
        var best = string.Empty;

        for (var i = 0; i < s.Length; i++)
        {
            var odd = ExpandCenter(i, i, s);
            if (best.Length < odd.Length)
            {
                best = odd;
            }

            var even = ExpandCenter(i, i + 1, s);
            if (best.Length < even.Length)
            {
                best = even;
            }
        }

        return best;
    }

    private static string ExpandCenter(int left, int right, string s)
    {
        while (left >= 0 && right < s.Length && s[left] == s[right])
        {
            left--;
            right++;
        }

        return s.Substring(left + 1, right - left - 1);
    }
}
