using System.Collections;

var lines = File.ReadAllLines("input.txt");
Func<long, long, long> add = (long a, long b) => a + b;
Func<long, long, long> mult = (long a, long b) => a * b;
Func<long, long, long> comb = (long a, long b) => long.Parse($"{a}{b}");
long total = 0;

foreach (var line in lines)
{
    long result = long.Parse(line.Split(':', StringSplitOptions.None)[0]);
    var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..]
        .Select(num => long.Parse(num)).ToArray();
    //For each potential combination of operators
    for (int i = 0; i < MathF.Pow(2, numbers.Count() - 1); i++)
    {
        BitArray b = new BitArray(new int[] { i });
        bool[] bits = new bool[b.Count];
        b.CopyTo(bits, 0);
        long sum = 0;
        for (int ii = 0; ii < numbers.Count() - 1; ii++)
        {
            long a = ii == 0 ? numbers[ii] : sum;
            sum = (bits[ii] ? add : mult)(a, numbers[ii + 1]);
        }
        if (sum == result)
        {
            total += result;
            break;
        }
    }
}
Console.WriteLine($"Part one: {total}");
total = 0;
foreach (var line in lines)
{
    long result = long.Parse(line.Split(':', StringSplitOptions.None)[0]);
    var numbers = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)[1..]
        .Select(num => long.Parse(num)).ToArray();
    //For each potential combination of operators
    for (long i = 0; i < MathF.Pow(3, numbers.Count() - 1); i++)
    {
        long option = i;
        long sum = 0;
        for (long ii = 0; ii < numbers.Count() - 1; ii++)
        {
            var op = (option % 3) switch
            {
                0 => add,
                1 => mult,
                2 => comb
            };
            long a = ii == 0 ? numbers[ii] : sum;
            sum = op(a, numbers[ii + 1]);
            option /= 3;
        }
        if (sum == result)
        {
            total += result;
            break;
        }
    }
}
Console.WriteLine($"Part two: {total}");

