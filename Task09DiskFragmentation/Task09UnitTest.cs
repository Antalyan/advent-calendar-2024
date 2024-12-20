using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task09DiskFragmentation;

public class Task09UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task09Solver();
    }

    [Theory]
    [InlineData("T09-Sample01", 1928)]
    [InlineData("T09-Fin", 6154342787400)]
    public override void P1Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T09-Sample01", 2858)]
    [InlineData("T09-Fin", 6183632723350)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult);
    }
}