using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task11StonesState;

public class Task11Solver : ITaskSolver
{
    private List<long> _stones = new();
    
    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var numberStrings = reader.ReadLine()!.Split(" ");
        _stones.AddRange(numberStrings.Select(long.Parse));
    }

    // Returns number of added positions
    private List<long> ComputeNextStoneGeneration()
    {
        List<long> nextGen = new();
        foreach (var stoneValue in _stones)
        {
            if (stoneValue == 0)
            {
                nextGen.Add(1);
            } else if (stoneValue > 0 && (int)Math.Log10(stoneValue) % 2 == 1)
            {
                int length = (int)Math.Log10(stoneValue) + 1; 
                int halfLength = length / 2;

                long divisor = (long)Math.Pow(10, halfLength); 
                long leftValue = stoneValue / divisor;        
                long rightValue = stoneValue % divisor;       

                nextGen.Add(leftValue);
                nextGen.Add(rightValue);
            }
            else
            {
                nextGen.Add(stoneValue * 2024);
            }
        }
        return nextGen;
    }

    private void IterateStones(int iterCount)
    {
        for (int iterNum = 0; iterNum < iterCount; iterNum++)
        {
            _stones = ComputeNextStoneGeneration();
            Console.WriteLine($"Stone count: {_stones.Count} in {iterNum}");
            // Console.WriteLine($"Stones after sequence: {string.Join(", ", _stones)}");
        }
    }


    public void SolveTask()
    {
        IterateStones(75);
        Console.WriteLine($"Stone count: {_stones.Count}");
    }
}