namespace RusBlazor.Tests.MockData
{
    public static class MockDataHelper
    {
        public static SortedDictionary<int, string> BuildDummyData(int start, int amount, bool flags)
        {
            var results = new SortedDictionary<int, string>();
            for(int i = start; i < amount;)
            {
                results.Add(i, $"Value {i}");
                if (flags)
                {
                    if (i == 0)
                        i = 1;
                    else
                        i *= 2;
                }
                else
                {
                    i++;
                }
            }
            return results;
        }
    }
}
