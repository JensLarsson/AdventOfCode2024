
public class Program
{

    static Action<ulong, List<ulong>> onZero = (ulong num, List<ulong> list) => list.Add(1);
    static Action<ulong, List<ulong>> onElse = (ulong num, List<ulong> list) => list.Add(num * 2024);
    static Action<ulong, List<ulong>> onEvenLength = (ulong num, List<ulong> list) =>
    {
    };


    static void Main()
    {
        var line = File.ReadAllText("input.txt");
        List<ulong> nums = line
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => ulong.Parse(s))
            .ToList();

        ulong count = GetSize(25, nums);

        Console.WriteLine(count);
    }
    static ulong GetSize(int maxDepth, List<ulong> nums)
    {
        ulong nodes = 0;
        foreach (var num in nums)
        {
            GetValue(num, maxDepth, 0, ref nodes);
        }
        return nodes;
    }
    static ulong GetValue(ulong num, int maxDepth, int depth, ref ulong nodes)
    {
        depth++;
        if (depth <= maxDepth)
        {
            if (num == 0)
            {
                GetValue(1, maxDepth, depth, ref nodes);
            }
            else if (num.ToString().Length % 2 == 0)
            {
                Span<char> numSpan = stackalloc char[num.ToString().Length];
                num.TryFormat(numSpan, out int charsWritten);

                int mid = charsWritten / 2;
                ReadOnlySpan<char> firstHalf = numSpan[..mid];
                ReadOnlySpan<char> secondHalf = numSpan[mid..];

                GetValue(ulong.Parse(firstHalf), maxDepth, depth, ref nodes);
                GetValue(ulong.Parse(secondHalf), maxDepth, depth, ref nodes);
            }
            else
            {
                GetValue(num * 2024, maxDepth, depth, ref nodes);
            }
        }
        else
        {
            nodes++;
        }
        return nodes;
    }
}


