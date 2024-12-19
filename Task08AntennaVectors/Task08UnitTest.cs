using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task08AntennaVectors;

public class Task08UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task08Solver();
    }

    [Theory]
    [InlineData("T08-Sample01", 14)]
    [InlineData("T08-Fin", 369)]
    public override void P1Test(string fileName, long expectedResult)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T08-Sample01", 34)]
    [InlineData("T08-Fin", 1169)]
    public override void P2Test(string fileName, long expectedResult)
    {
        base.P2Test(fileName, expectedResult);
    }
}