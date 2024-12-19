using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task06GuardMap;

public class Task06UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task06Solver();
    }

    [Theory]
    [InlineData("T06-Sample01", 41)]
    [InlineData("T06-Fin", 5145)]
    public override void P1Test(string fileName, long expectedResult)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T06-Sample01", 6)]
    [InlineData("T06-Fin", 1523)]
    public override void P2Test(string fileName, long expectedResult)
    {
        base.P2Test(fileName, expectedResult);
    }
}