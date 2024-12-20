using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task07Equations;

using Equation = (long Result, List<long>Members);

public class Task07Solver : ITaskSolver
{
    private readonly List<Equation> _equations = new();

    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        string? line;

        while ((line = reader.ReadLine()) != null)
        {
            var parsedEq = line.Split(" ");
            _equations.Add(new Equation
            {
                Result = long.Parse(parsedEq[0].Remove(parsedEq[0].Length - 1)),
                Members = parsedEq.Skip(1).Select(long.Parse).ToList()
            });
        }
    }

    //TODO move to eq class
    private bool IsEquationSolvable(Equation equation, int currentIndex, long? calculationResult,
        bool allowConcat = false)
    {
        if (currentIndex == equation.Members.Count)
        {
            return calculationResult == equation.Result;
        }

        long additionResult = (calculationResult ?? 0) + equation.Members[currentIndex];
        bool additionSucceeded = additionResult <= equation.Result &&
                                 IsEquationSolvable(equation, currentIndex + 1, additionResult, allowConcat);
        long multiplicationResult = (calculationResult ?? 1) * equation.Members[currentIndex];
        bool multiplicationSucceeded = multiplicationResult <= equation.Result &&
                                       IsEquationSolvable(equation, currentIndex + 1, multiplicationResult,
                                           allowConcat);
        long concatResult = long.Parse((calculationResult.ToString() ?? "") + equation.Members[currentIndex]);
        bool concatSucceeded = allowConcat && concatResult <= equation.Result &&
                               IsEquationSolvable(equation, currentIndex + 1, concatResult, allowConcat);
        return additionSucceeded || multiplicationSucceeded || concatSucceeded;
    }

    private long CountSolvableEquationsSum(bool allowConcat = false)
    {
        var solvableEquations = _equations.Where(eq => IsEquationSolvable(eq, 0, null, allowConcat));
        return solvableEquations.Sum(eq => eq.Result);
    }
    
    public long SolveTaskP1()
    {
        return CountSolvableEquationsSum();
    }
    
    public long SolveTaskP2()
    {
        return CountSolvableEquationsSum(true);
    }
    
    public void SetSolverParams(params object[] solverParams)
    {
    }
}