class Program
{
    static void Main()
    {
        var lines = File.ReadLines("input.txt");
        int[,] matrix = GetIntArray(lines.ToList());
        int matchesPartA = 0;
        int matchesPartB = 0;

        int[][,] kernelsPartA = {
            new int[,] { { 'X', 'M', 'A', 'S' } },
            new int[,] { { 'S', 'A', 'M', 'X' } },
            new int[,] { { 'X' }, { 'M' }, { 'A' }, { 'S' } },
            new int[,] { { 'S' }, { 'A' }, { 'M' }, { 'X' } },
            new int[,] {
                { 'S', -1, -1, -1 },
                { -1, 'A', -1, -1 },
                { -1, -1, 'M', -1 },
                { -1, -1, -1, 'X' },
            },
            new int[,] {
                { 'X', -1, -1, -1 },
                { -1, 'M', -1, -1 },
                { -1, -1, 'A', -1 },
                { -1, -1, -1, 'S' },
            },
            new int[,] {
                { -1, -1, -1, 'X' },
                { -1, -1, 'M', -1 },
                { -1, 'A', -1, -1 },
                { 'S', -1, -1, -1 },
            },
            new int[,] {
                { -1, -1, -1, 'S' },
                { -1, -1, 'A', -1 },
                { -1, 'M', -1, -1 },
                { 'X', -1, -1, -1 },
            }
        };

        int[][,] kernelsPartB = {
            new int[,] {
                { 'M', -1, 'S' },
                { -1, 'A', -1 },
                { 'M', -1, 'S' },
            },
            new int[,] {
                { 'S', -1, 'M' },
                { -1, 'A', -1 },
                { 'S', -1, 'M' },
            },
            new int[,] {
                { 'M', -1, 'M' },
                { -1, 'A', -1 },
                { 'S', -1, 'S' },
            },
            new int[,] {
                { 'S', -1, 'S' },
                { -1, 'A', -1 },
                { 'M', -1, 'M' },
            }
        };

        foreach (var kernel in kernelsPartA)
        {
            matchesPartA += CountKernelMatches(matrix, kernel);
        }

        Console.WriteLine($"Part1 Number of Kernel Matches: {matchesPartA}");

        foreach (var kernel in kernelsPartB)
        {
            matchesPartB += CountKernelMatches(matrix, kernel);
        }

        Console.WriteLine($"Part2 Number of Kernel Matches: {matchesPartB}");
    }

    static int[,] GetIntArray(List<string> lines)
    {
        int rows = lines.Count;
        int cols = lines.First().Length;

        int[,] matrix = new int[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                matrix[y, x] = lines[y][x];
            }
        }
        return matrix;
    }

    static int CountKernelMatches(int[,] input, int[,] kernel)
    {
        int inputRows = input.GetLength(0);
        int inputCols = input.GetLength(1);
        int kernelRows = kernel.GetLength(0);
        int kernelCols = kernel.GetLength(1);

        int matchCount = 0;

        for (int y = 0; y <= inputRows - kernelRows; y++)
        {
            for (int x = 0; x <= inputCols - kernelCols; x++)
            {
                if (IsKernelMatch(input, kernel, y, x))
                {
                    matchCount++;
                }
            }
        }
        return matchCount;
    }

    static bool IsKernelMatch(int[,] input, int[,] kernel, int startRow, int startCol)
    {
        int kernelRows = kernel.GetLength(0);
        int kernelCols = kernel.GetLength(1);

        for (int kY = 0; kY < kernelRows; kY++)
        {
            for (int kX = 0; kX < kernelCols; kX++)
            {
                if (kernel[kY, kX] == -1)
                {
                    continue;
                }

                if (input[startRow + kY, startCol + kX] != kernel[kY, kX])
                {
                    return false;
                }
            }
        }
        return true;
    }
}