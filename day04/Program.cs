﻿class Program
{
    static void Main()
    {
        var lines = File.ReadLines("input.txt");
        int[,] matrix = GetIntArray(lines.ToList());
        int matches = 0;


        int[,] kernel = {
            { 'X', 'M', 'A', 'S' }
        };
        matches += CountKernelMatches(matrix, kernel);

        kernel = new int[,] {
            { 'S', 'A', 'M', 'X' }
        };
        matches += CountKernelMatches(matrix, kernel);

        kernel = new int[,] {
            { 'X' },
            { 'M' },
            { 'A' },
            { 'S' },
        };
        matches += CountKernelMatches(matrix, kernel);

        kernel = new int[,] {
            { 'S' },
            { 'A' },
            { 'M' },
            { 'X' },
        };
        matches += CountKernelMatches(matrix, kernel);

        kernel = new int[,] {
            { 'S', -1, -1, -1 },
            { -1, 'A', -1, -1, },
            { -1, -1, 'M', -1, },
            { -1, -1, -1, 'X' },
        };
        matches += CountKernelMatches(matrix, kernel);

        kernel = new int[,] {
            { 'X', -1 ,-1, -1 },
            { -1, 'M', -1, -1, },
            { -1, -1, 'A', -1, },
            { -1, -1, -1, 'S' },
        };
        matches += CountKernelMatches(matrix, kernel);

        kernel = new int[,] {
            { -1, -1, -1, 'X'},
            { -1, -1, 'M', -1},
            { -1, 'A', -1, -1},
            { 'S', -1, -1, -1},
        };
        matches += CountKernelMatches(matrix, kernel);

        kernel = new int[,] {
            { -1, -1, -1, 'S'},
            { -1, -1, 'A', -1},
            { -1, 'M', -1, -1},
            { 'X', -1, -1, -1},
        };
        matches += CountKernelMatches(matrix, kernel);

        Console.WriteLine($"Number of Kernel Matches: {matches}");
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