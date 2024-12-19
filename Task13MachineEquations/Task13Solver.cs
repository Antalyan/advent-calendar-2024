using System.Text.RegularExpressions;
using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task13MachineEquations;

public class Task13Solver : ITaskSolver
{
    private readonly List<Machine> _machines = new();

    private static int ExtractDigits(string input, string pattern)
    {
        Match match = Regex.Match(input, pattern);
        if (match.Success && int.TryParse(match.Groups[1].Value, out int number))
        {
            return number;
        }

        throw new Exception($"Valid number not found in {input}");
    }

    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        string? line = reader.ReadLine();
        while (line != null)
        {
            CoordinateLarge buttonA = (ExtractDigits(line, @"X\+(\d+)"), ExtractDigits(line, @"Y\+(\d+)"));
            line = reader.ReadLine();
            CoordinateLarge buttonB = (ExtractDigits(line!, @"X\+(\d+)"), ExtractDigits(line!, @"Y\+(\d+)"));
            line = reader.ReadLine();
            CoordinateLarge prize = (ExtractDigits(line!, @"X=(\d+)"), ExtractDigits(line!, @"Y=(\d+)"));
            _machines.Add(new Machine(buttonA, buttonB, prize));
            reader.ReadLine();
            line = reader.ReadLine();
        }
    }

    private CoordinateLarge? SolveEquationSystem(LinearEquation first, LinearEquation second)
    {
        // Remove B variable
        long left = first.ACoef * second.BCoef - second.ACoef * first.BCoef;
        long right = first.NumCoef * second.BCoef - second.NumCoef * first.BCoef;
        if (right % left != 0)
        {
            return null;
        }

        long a = right / left;
        long b = (first.NumCoef - (a * first.ACoef)) / first.BCoef;
        if ((first.NumCoef - (a * first.ACoef)) % first.BCoef != 0)
        {
            return null;
        }

        return (a, b);
    }

    private long CountTokens()
    {
        return _machines.Select(machine => SolveEquationSystem(
                new LinearEquation(machine.ButtonA.X, machine.ButtonB.X, machine.Prize.X),
                new LinearEquation(machine.ButtonA.Y, machine.ButtonB.Y, machine.Prize.Y)))
            .Where(result => result != null)
            .Select(result => (CoordinateLarge)result!)
            .Select(result => 3L * result.X + result.Y).Sum();
    }

    private void IncreasePrizePositions(long correction)
    {
        foreach (var machine in _machines)
        {
            machine.Prize = (machine.Prize.X + correction, machine.Prize.Y + correction);
        }
    }

    public long SolveTaskP1()
    {
        return CountTokens();
    }

    public long SolveTaskP2()
    {
        IncreasePrizePositions(10000000000000);
        return CountTokens();
    }
}