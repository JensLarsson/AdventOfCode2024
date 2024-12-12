var lines = File.ReadAllLines("input.txt");
int height = lines[0].Length;
int width = lines.Length;
char[,] chars = new char[width, height];
HashSet<Vec2i> antiNodes = new();
Dictionary<char, List<Vec2i>> points = new();

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        char c = lines[y][x];
        chars[y, x] = c;
        if (char.IsAsciiLetterOrDigit(c))
        {
            if (!points.TryGetValue(c, out var list))
            {
                list = new List<Vec2i>();
                points.Add(c, list);
            }
            list.Add(new Vec2i(x, y));
        }
    }
}

foreach (var list in points.Values)
{
    if (list.Count <= 1)
    {
        continue;
    }
    for (int i = 0; i < list.Count; i++)
    {
        for (int ii = 0; ii < list.Count; ii++)
        {
            if (i == ii)
            {
                continue;
            }
            Vec2i direction = list[i] - list[ii];
            Vec2i antiNode = list[i] + direction;
            antiNodes.Add(list[i]);
            while (!(antiNode.X < 0 || antiNode.X >= width || antiNode.Y < 0 || antiNode.Y >= height))
            {
                chars[antiNode.Y, antiNode.X] = '#';
                antiNodes.Add(antiNode);
                antiNode += direction;
            }
        }
    }
}

//print result
for (int i = 0; i < chars.GetLength(0); i++)
{
    for (int ii = 0; ii < chars.GetLength(1); ii++)
    {
        Console.Write(chars[i, ii]);
    }
    Console.Write('\n');
}

Console.WriteLine(antiNodes.Count());


record Vec2i(int x, int y)
{
    public int X = x;
    public int Y = y;
    public static Vec2i operator +(Vec2i a) => a;
    public static Vec2i operator +(Vec2i a, Vec2i b) => new Vec2i(a.X + b.X, a.Y + b.Y);
    public static Vec2i operator -(Vec2i a) => a;
    public static Vec2i operator -(Vec2i a, Vec2i b) => new Vec2i(a.X - b.X, a.Y - b.Y);
}