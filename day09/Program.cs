var line = File.ReadAllText("input.txt")
    .Select(c => c - '0')
    .ToArray();

List<int> nums = line
    .SelectMany((count, i)
        => Enumerable.Repeat(i % 2 == 0 ? i / 2 : -1, count))
    .ToList();

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
ulong total = nums
    .Select((num, i) => (ulong)(i * (num >= 0 ? num : 0)))
    .Aggregate(0UL, (sum, value) => sum + value);

Console.WriteLine($"part 1: {total}");

List<int> numsB = new();
List<int> emptyIndexes = new();
for (int i = 0; i < line.Length; i++)
{
    int count = line[i];
    if (i % 2 == 0)
    {
        bool breakEarly = false;
        while (emptyIndexes.Contains(i / 2))
        {
            emptyIndexes.Remove(i / 2);
            numsB.Add(0);
            breakEarly = true;
            continue;
        }
        if (breakEarly)
        {
            continue;
        }
        line[i] = 0;
        for (int ii = 0; ii < count; ii++)
        {
            numsB.Add(i / 2);
        }
    }
    else
    {
        while (count > 0)
        {
            bool retry = false;
            bool found = false;
            for (int ii = line.Length - 1; ii >= 0; ii--)
            {
                if (ii % 2 == 0)
                {
                    int backCount = line[ii];
                    if (line[ii] != 0 && backCount <= count)
                    {
                        found = true;
                        line[ii] = 0;
                        for (int iii = 0; iii < backCount; iii++)
                        {
                            emptyIndexes.Add(ii / 2);
                            numsB.Add(ii / 2);
                            count--;
                        }
                        if (count > 0)
                        {
                            retry = true;
                        }
                    }
                }
            }
            if (!found)
            {
                while (count > 0)
                {
                    numsB.Add(0);
                    count--;
                }
            }
            if (!retry)
            {
                break;
            }
        }

    }
}

total = numsB
    .Select((num, i) => (ulong)(i * (num >= 0 ? num : 0)))
    .Aggregate(0UL, (sum, value) => sum + value);
Console.WriteLine($"part 2: {total}");