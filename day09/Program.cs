using System.Security.Principal;

var line = File.ReadAllText("input.txt");
Console.WriteLine(line);

List<int> nums = new();
for (int i = 0; i < line.Length; i++)
{
    int count = line[i] - '0';
    for (int ii = 0; ii < count; ii++)
    {
        int idCheck = i % 2 == 0 ? i / 2 : -1;
        nums.Add(idCheck);
    }
}
Console.WriteLine(string.Join(',', nums));

int start = 0;
int end = nums.Count() - 1;

while (start < end)
{
    if (nums[start] < 0)
    {
        while (nums[end] < 0)
        {
            end--;
        }
        nums[start] = nums[end];
        nums[end] = -1;
        end--;
    }
    start++;
}
Console.WriteLine(string.Join(',', nums));

ulong total = 0;
for (int i = 0; i < nums.Count; i++)
{
    if (nums[i] < 0)
    {
        break;
    }
    total += (ulong)(i * nums[i]);
}
Console.WriteLine(total);