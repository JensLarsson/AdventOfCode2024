
var line = File.ReadAllText("input.txt");
List<ulong> nums = line
    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
    .Select(s => ulong.Parse(s))
    .ToList();

Action<ulong, List<ulong>> onZero = (ulong num, List<ulong> list) => list.Add(1);
Action<ulong, List<ulong>> onEvenLength = (ulong num, List<ulong> list) =>
{
    var s = num.ToString().ToCharArray();
    list.Add(ulong.Parse(s[..(s.Length / 2)]));
    list.Add(ulong.Parse(s[(s.Length / 2)..]));
};
Action<ulong, List<ulong>> onElse = (ulong num, List<ulong> list) => list.Add(num * 2024);

for (ulong i = 0; i < 25; i++)
{
    List<ulong> newL = new();
    foreach (ulong num in nums)
    {
        var action = num switch
        {
            0 => onZero,
            _ when num.ToString().Length % 2 == 0 => onEvenLength,
            _ => onElse
        };
        action.Invoke(num, newL);
    }
    nums = newL;
}

Console.WriteLine(nums.Count);

