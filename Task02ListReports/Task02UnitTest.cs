using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task02ListReports;

public class Task02UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task02Solver();
    }

    [Theory]
    [InlineData("T02-Sample01", 2)]
    [InlineData("T02-Fin", 202)]
    public override void P1Test(string fileName, long expectedResult)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T02-Sample01", 4)]
    [InlineData("T02-Fin", 271)]
    public override void P2Test(string fileName, long expectedResult)
    {
        base.P2Test(fileName, expectedResult);
    }
}