class Vec2i(int x, int y)
{
    public int X = x;
    public int Y = y;
    public static Vec2i operator +(Vec2i a) => a;
    public static Vec2i operator +(Vec2i a, Vec2i b) => new Vec2i(a.X + b.X, a.Y + b.Y);
    public static Vec2i operator -(Vec2i a) => a;
    public static Vec2i operator -(Vec2i a, Vec2i b) => new Vec2i(a.X - b.X, a.Y - b.Y);
}

class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("input.txt");
        int height = lines.Count();
        int width = lines[0].Count();
        var playerPos = GetPlayerStartPosition(lines);
        var playerDirection = new Vec2i(0, 1);

        var traversedGrid = TraverseGrid(GetGrid(lines), playerPos, playerDirection);

        int count = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                count += traversedGrid[x, y] == '^' ? 1 : 0;
                Console.Write((char)traversedGrid[x, y]);
            }
            Console.Write("\n");
        }
        Console.WriteLine(count);

        int positions = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (traversedGrid[x, y] == '#')
                {
                    continue;
                }
                traversedGrid[x, y] = '#';
                if (TraverseGrid(traversedGrid, playerPos, playerDirection, count * 2) == null)
                {
                    positions++;
                }
                traversedGrid[x, y] = '.';
            }
        }
        Console.WriteLine(positions);
    }
    static Vec2i GetPlayerStartPosition(string[] text)
    {
        for (int y = 0; y < text.Count(); y++)
        {
            for (int x = 0; x < text[y].Count(); x++)
            {
                if (text[y][x] == '^')
                {
                    return new Vec2i(x, y);
                }
            }
        }
        return new Vec2i(-1, -1);
    }
    static int[,] GetGrid(string[] text)
    {
        int[,] grid = new int[text[0].Count(), text.Count()];
        for (int y = 0; y < text.Count(); y++)
        {
            for (int x = 0; x < text[y].Count(); x++)
            {
                grid[x, y] = text[y][x];
            }
        }
        return grid;
    }
    static int[,] TraverseGrid(int[,] grid, Vec2i playerPos, Vec2i playerDirection, int maxDistance = 10000)
    {
        int width = grid.GetLength(1);
        int height = grid.GetLength(1);
        while (true)
        {
            maxDistance--;
            if (maxDistance <= 0)
            {
                return null;
            }
            grid[playerPos.X, playerPos.Y] = '^';
            var nextPos = playerPos - playerDirection;

            //if outside grid
            if (nextPos.X < 0 || nextPos.X >= width || nextPos.Y < 0 || nextPos.Y >= height)
            {
                break;
            }

            if (grid[nextPos.X, nextPos.Y] == '#')
            {
                playerDirection = playerDirection switch
                {
                    { X: 0, Y: 1 } => new Vec2i(-1, 0),
                    { X: -1, Y: 0 } => new Vec2i(0, -1),
                    { X: 0, Y: -1 } => new Vec2i(1, 0),
                    { X: 1, Y: 0 } => new Vec2i(0, 1)
                };
            }
            else
            {
                playerPos = nextPos;
            }
        }
        return grid;
    }
}