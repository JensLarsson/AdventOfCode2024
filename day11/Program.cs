public record Key(ulong num, int depth);
public class Program
{
    static void Main()
    {
        ulong nodes = File.ReadAllText("input.txt")
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => ulong.Parse(s))
            .Aggregate(0UL, (acc, num) => acc + GetValue(num, 75, new()));

        Console.WriteLine(nodes);
    }

    static ulong GetValue(ulong num, int maxDepth, Dictionary<Key, ulong> solved)
    {
        if (maxDepth-- > 0)
        {
            var key = new Key(num, maxDepth);
            if (solved.TryGetValue(key, out var result)) ;
            else if (num == 0)                      //make 1
            {
                result = GetValue(1, maxDepth, solved);
            }
            else if ((int)Math.Floor(Math.Log10(num) + 1) is int length && length % 2 == 0)
            {
                ulong divisor = (ulong)Math.Pow(10, length / 2);
                ulong leftPart = num / divisor;     //123456 / 1000 = 123 due to flooring the decimals
                ulong rightPart = num % divisor;    //123456 % 123  = 456

                var a = GetValue(leftPart, maxDepth, solved);
                result = a + GetValue(rightPart, maxDepth, solved);
            }
            else
            {//multiply with 2024
                result = GetValue(num * 2024, maxDepth, solved);
            }
            solved.TryAdd(key, result);
            return result;
        }
        return 1;
    }
}