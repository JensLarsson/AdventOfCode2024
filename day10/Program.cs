var lines = File.ReadAllLines("input.txt");
int[,] grid = new int[lines[0].Length, lines.Length];
List<GridPosition> startPositions = new();
List<GridPosition> endPositions = new();

for (int y = 0; y < lines.Length; y++)
{
    for (int x = 0; x < lines[y].Length; x++)
    {
        int value = lines[y][x] - '0';
        grid[x, y] = value;
        if (value == 0)
        {
            startPositions.Add(new GridPosition(x, y));
        }
        if (value == 9)
        {
            endPositions.Add(new GridPosition(x, y));
        }
    }
}

int total = 0;
foreach (var startPos in startPositions)
{
    foreach (var endPos in endPositions)
    {
        var paths = AStar.FindPaths(grid, startPos, endPos, (a, b) => a < b && b - a == 1);
        total += paths.Count;
    }
}
Console.WriteLine(total);