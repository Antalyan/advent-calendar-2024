using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task04WordOccurrences;

public class Task04UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task04Solver();
    }

    [Theory]
    [InlineData("T04-Sample01", 18)]
    [InlineData("T04-Fin", 2530)]
    public override void P1Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T04-Sample01", 9)]
    [InlineData("T04-Fin", 1921)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult);
    }
}