using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task11StonesState;

public class Task11Solver : ITaskSolver
{
    private readonly List<long> _stones = new();
    // (Stone, remaining gens): number of children in of stone after the next remaining gens
    private readonly Dictionary<(long, int), long> _cache = new();
    
    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var numberStrings = reader.ReadLine()!.Split(" ");
        _stones.AddRange(numberStrings.Select(long.Parse));
    }
    
    private long CountNextGens(long stoneValue, int remainingGens)
    {
        if (remainingGens == 0)
        {
            return 1; 
        }

        if (_cache.TryGetValue((stoneValue, remainingGens), out var cachedResult))
        {
            return cachedResult;
        }

        long result;

        if (stoneValue == 0)
        {
            result = CountNextGens(1, remainingGens - 1);
        }
        else
        {
            int numDigits = (int)Math.Log10(stoneValue) + 1;

            if (numDigits % 2 == 0)
            {
                long halfLength = numDigits / 2;
                long divisor = (long)Math.Pow(10, halfLength);

                long leftValue = stoneValue / divisor;
                long rightValue = stoneValue % divisor;

                result = CountNextGens(leftValue, remainingGens - 1) + CountNextGens(rightValue, remainingGens - 1);
            }
            else
            {
                result = CountNextGens(stoneValue * 2024, remainingGens - 1);
            }
        }

        _cache[(stoneValue, remainingGens)] = result;
        return result;
    }
    
    private long IterateStones(int iterCount)
    {
        return _stones.Select(s => CountNextGens(s, iterCount)).Sum();
    }


    public long SolveTaskP1()
    {
        return IterateStones(25);
    }
    
    public long SolveTaskP2()
    {
        return IterateStones(75);
    }
    
    public void SetSolverParams(params object[] solverParams)
    {
    }
}