public class TwoSumService : ITwoSumService
{
    public bool TryFindPair(int[] nums, int target, out int[] pair)
    {
        var found = new Dictionary<int, int>();

        for (var i = 0; i < nums.Length; i++)
        {
            var diff = target - nums[i];
            if (found.ContainsKey(diff))
            {
                pair = new[] { found[diff], i };
                return true;
            }

            if (!found.ContainsKey(nums[i]))
            {
                found.Add(nums[i], i);
            }
        }

        pair = Array.Empty<int>();
        return false;
    }
}
