using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task12GardenArea;

public class Task12UnitTest
{
    private static ITaskSolver CreateSolver(string fileName)
    {
        string sourceFile = Path.Combine(Constants.DataFilePath, $"{fileName}.txt");
        ITaskSolver taskSolver = new Task12Solver();
        taskSolver.LoadTaskDataFromFile(sourceFile);
        return taskSolver;
    }
    
    [Theory]
    [InlineData("T12-Sample01", 140)]
    [InlineData("T12-Sample02", 1930)]
    [InlineData("T12-Fin", 1431440)]
    public void P1Test(string fileName, long expectedResult)
    {
        var solver = CreateSolver(fileName);
        Assert.Equal(solver.SolveTaskP1(), expectedResult);
    }
    
    [Theory]
    [InlineData("T12-Sample01", 80)]
    [InlineData("T12-Sample02", 1206)]
    [InlineData("T12-Sample03", 236)]
    [InlineData("T12-Sample04", 368)]
    [InlineData("T12-Sample05", 436)]
    [InlineData("T12-Fin", 869070)]
    public void P2Test(string fileName, long expectedResult)
    {
        var solver = CreateSolver(fileName);
        Assert.Equal(solver.SolveTaskP2(), expectedResult);
    }
}