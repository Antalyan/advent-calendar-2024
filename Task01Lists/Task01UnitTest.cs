using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task01Lists;

public class Task01UnitTest
{
    private static ITaskSolver CreateSolver(string fileName)
    {
        string sourceFile = Path.Combine(Constants.DataFilePath, $"{fileName}.txt");
        ITaskSolver taskSolver = new Task01Solver();
        taskSolver.LoadTaskDataFromFile(sourceFile);
        return taskSolver;
    }
    
    [Theory]
    [InlineData("T01-Sample01", 11)]
    public void P1Test(string fileName, long expectedResult)
    {
        var solver = CreateSolver(fileName);
        Assert.Equal(solver.SolveTaskP1(), expectedResult);
    }
    
    [Theory]
    [InlineData("T01-Sample01", 31)]
    public void P2Test(string fileName, long expectedResult)
    {
        var solver = CreateSolver(fileName);
        Assert.Equal(solver.SolveTaskP2(), expectedResult);
    }
}