using System;
using System.Collections.Generic;

public record GridPosition(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;
    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}

public static class AStar
{
    public static List<GridPosition> FindPath(int[,] grid, GridPosition start, GridPosition goal, Func<int, int, bool> predicate)
    {
        var closedSet = new HashSet<GridPosition>();
        var openSet = new PriorityQueue<GridPosition>();
        var cameFrom = new Dictionary<GridPosition, GridPosition>();
        var gScore = new Dictionary<GridPosition, int>();
        var fScore = new Dictionary<GridPosition, int>();

        openSet.Enqueue(start, 0);
        gScore[start] = 0;
        fScore[start] = ManhattanDistance(start, goal);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current.Equals(goal))
            {
                return ReconstructPath(cameFrom, current);
            }

            closedSet.Add(current);

            foreach (var neighbor in GetNeighbors(grid, current, predicate))
            {
                if (closedSet.Contains(neighbor))
                {
                    continue;
                }

                var tentativeGScore = gScore[current] + grid[neighbor.X, neighbor.Y];

                if (!openSet.Contains(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = tentativeGScore + ManhattanDistance(neighbor, goal);

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Enqueue(neighbor, fScore[neighbor]);
                    }
                }
            }
        }
        return null;
    }

    //This is no longer A*, but width first, but who cares
    public static List<List<GridPosition>> FindPaths(int[,] grid, GridPosition start, GridPosition goal, Func<int, int, bool> predicate)
    {
        var openSet = new Queue<GridPosition>();
        var cameFrom = new Dictionary<GridPosition, GridPosition>();
        var paths = new List<List<GridPosition>>();

        openSet.Enqueue(start);

        while (openSet.Count > 0)
        {
            var current = openSet.Dequeue();

            if (current.Equals(goal))
            {
                paths.Add(ReconstructPath(cameFrom, current));
                continue;
            }
            foreach (var neighbor in GetNeighbors(grid, current, predicate))
            {
                cameFrom[neighbor] = current;
                openSet.Enqueue(neighbor);
            }
        }
        return paths;
    }

    private static int ManhattanDistance(GridPosition a, GridPosition b)
    {
        return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
    }

    private static List<GridPosition> ReconstructPath(Dictionary<GridPosition, GridPosition> cameFrom, GridPosition current)
    {
        var path = new List<GridPosition> { current };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }

    private static IEnumerable<GridPosition> GetNeighbors(int[,] grid, GridPosition position, Func<int, int, bool> predicate)
    {
        if (position.X > 0 && predicate(grid[position.X, position.Y], grid[position.X - 1, position.y]))
        {
            yield return new GridPosition(position.X - 1, position.Y);
        }

        if (position.Y > 0 && predicate(grid[position.X, position.Y], grid[position.X, position.y - 1]))
        {
            yield return new GridPosition(position.X, position.Y - 1);
        }

        if (position.X < grid.GetLength(0) - 1 && predicate(grid[position.X, position.Y], grid[position.X + 1, position.y]))
        {
            yield return new GridPosition(position.X + 1, position.Y);
        }

        if (position.Y < grid.GetLength(1) - 1 && predicate(grid[position.X, position.Y], grid[position.X, position.y + 1]))
        {
            yield return new GridPosition(position.X, position.Y + 1);
        }
    }


    private class PriorityQueue<TItem>
    {
        private readonly List<(TItem item, int priority)> data = new List<(TItem, int)>();

        public int Count => data.Count;

        public void Enqueue(TItem item, int priority)
        {
            data.Add((item, priority));
            data.Sort((x, y) => x.priority.CompareTo(y.priority));
        }

        public TItem Dequeue()
        {
            var item = data[0].item;
            data.RemoveAt(0);
            return item;
        }

        public bool Contains(TItem item)
        {
            return data.Exists(x => EqualityComparer<TItem>.Default.Equals(x.item, item));
        }
    }
}