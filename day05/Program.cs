public class Node
{
    public int Value;
    public Node? Larger;
    public Node? Smaller;

    public Node(int value, int larger)
    {
        Value = value;
        Larger = new Node(larger);
    }
    public Node(int value)
    {
        Value = value;
    }
}

class Program
{
    static void Main()
    {
        var lines = File.ReadLines("input.txt");
        var nodes = new Dictionary<int, List<int>>();
        var numbers = new List<List<int>>();



        foreach (string line in lines)
        {
            var parts = line.Split('|', StringSplitOptions.None);
            if (parts.Count() == 2)
            {
                int smaller = int.Parse(parts[0]);
                int larger = int.Parse(parts[1]);
                if (nodes.TryGetValue(smaller, out var list))
                {
                    list.Add(larger);
                }
                else
                {
                    nodes.Add(smaller, new List<int> { larger });
                }
                nodes.TryAdd(larger, new List<int>());
                continue;
            }
            parts = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Count() > 0)
            {
                var nums = new List<int>();
                foreach (string part in parts)
                {
                    nums.Add(int.Parse(part));
                }
                numbers.Add(nums);
            }
        }

        int combined = 0;

        foreach (var list in numbers)
        {
            nodes.TryGetValue(list[0], out var larger);
            bool completed = true;
            for (int i = 1; i < list.Count(); i++)
            {
                if (larger.Contains(list[i]))
                {
                    larger = nodes[list[i]];
                    continue;
                }
                else
                {
                    completed = false;
                    break;
                }
            }
            if (completed)
            {
                combined += list[list.Count / 2];
            }
        }
        Console.WriteLine(combined);

    }
}