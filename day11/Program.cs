public record Key(ulong num, int depth)
{
    ulong Num = num;
    int Depth = depth;
}
public class Program
{
    static Dictionary<Key, ulong> solved = new();
    static void Main()
    {
        var line = File.ReadAllText("input.txt");
        List<ulong> nums = line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => ulong.Parse(s))
            .ToList();

        long start = GC.GetAllocatedBytesForCurrentThread();
        ulong nodes = 0;
        foreach (var num in nums)
        {
            nodes += GetValue(num, 75, 0);
        }

        long end = GC.GetAllocatedBytesForCurrentThread();
        Console.WriteLine($"Memory allocated: {end - start} bytes");
        Console.WriteLine(nodes);
    }
    static ulong GetValue(ulong num, int maxDepth, int depth)
    {
        if (depth++ < maxDepth)
        {
            ulong result = 0;
            var key = new Key(num, maxDepth - depth);
            if (solved.TryGetValue(key, out result)) { }
            else if (num == 0)                      //make 1
            {
                result = GetValue(1, maxDepth, depth);
            }
            else if ((int)Math.Floor(Math.Log10(num) + 1) is int length && length % 2 == 0)//split into two values
            {
                Span<char> numSpan = stackalloc char[length];
                num.TryFormat(numSpan, out int charsWritten);

                int mid = charsWritten / 2;
                ReadOnlySpan<char> firstHalf = numSpan[..mid];
                ReadOnlySpan<char> secondHalf = numSpan[mid..];

                var a = GetValue(ulong.Parse(firstHalf), maxDepth, depth);
                result = a + GetValue(ulong.Parse(secondHalf), maxDepth, depth);
            }
            else                                   //multiply with 2024
            {
                result = GetValue(num * 2024, maxDepth, depth);
            }
            solved.TryAdd(key, result);
            return result;
        }
        return 1;
    }
}