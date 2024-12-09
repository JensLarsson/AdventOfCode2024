public class Node(int value)
{
    public int Value { get; init; } = value;
    public Dictionary<int, Node> Larger = new();
    public Dictionary<int, Node> Smaller = new();

    public void AddLarger(Node node)
    {
        Larger.TryAdd(node.Value, node);
    }
    public void AddSmaller(Node node)
    {
        Smaller.TryAdd(node.Value, node);
    }

    public Node GetLarger(int value)
    {
        // if (!Larger.TryGetValue(value, out var larger))
        // {
        //     foreach (var n in Larger)
        //     {
        //         larger = n.Value.GetLarger(value);
        //         if (larger != null)
        //         {
        //             return larger;
        //         }
        //     }
        // }
        Larger.TryGetValue(value, out var larger);
        return larger;
    }

    public Node GetSmaller(int value)
    {
        // if (!Smaller.TryGetValue(value, out var smaller))
        // {
        //     foreach (var n in Smaller)
        //     {
        //         smaller = n.Value.GetSmaller(value);
        //         if (smaller != null)
        //         {
        //             return smaller;
        //         }
        //     }
        // }
        Smaller.TryGetValue(value, out var smaller);
        return smaller;
    }
}
class Program
{
    static void Main()
    {
        var lines = File.ReadLines("input.txt");
        var nodes = new Dictionary<int, Node>();
        var numbers = new List<List<int>>();

        foreach (string line in lines)
        {
            var parts = line.Split('|', StringSplitOptions.None);
            if (parts.Count() == 2)
            {
                int s = int.Parse(parts[0]);
                int l = int.Parse(parts[1]);
                nodes.TryGetValue(s, out var small);
                nodes.TryGetValue(l, out var large);
                small ??= new Node(s);
                large ??= new Node(l);
                small.AddLarger(large);
                large.AddSmaller(small);
                nodes.TryAdd(s, small);
                nodes.TryAdd(l, large);
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
        HashSet<List<int>> incorrectLists = new();

        foreach (var list in numbers)
        {
            if (IsOrdered(list, nodes[list[0]]) == list.Count())
            {
                combined += list[list.Count() / 2];
            }
            else
            {
                incorrectLists.Add(list);
            }
        }
        Console.WriteLine(combined);
        Console.WriteLine(incorrectLists.Count());
        combined = 0;
        foreach (var list in incorrectLists)
        {
            int i = 0;
            do
            {
                i = IsOrdered(list, nodes[list[0]]);
                if (i != list.Count())
                {
                    int temp = list[i];
                    list[i] = list[i - 1];
                    list[i - 1] = temp;
                }
            } while (i != list.Count());
            combined += list[list.Count() / 2];
        }
        Console.WriteLine(combined);
    }

    static int IsOrdered(List<int> numbers, Node ruleRoot)
    {
        Node larger = ruleRoot;
        int i;
        for (i = 1; i < numbers.Count; i++)
        {
            larger = larger.GetLarger(numbers[i]);
            if (larger == null)
            {
                break;
            }
        }
        return i;
    }
}