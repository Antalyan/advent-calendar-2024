using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task05PrinterOrder;

public class Task05UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task05Solver();
    }

    [Theory]
    [InlineData("T05-Sample01", 143)]
    [InlineData("T05-Fin", 5651)]
    public override void P1Test(string fileName, long expectedResult)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T05-Sample01", 123)]
    [InlineData("T05-Fin", 4743)]
    public override void P2Test(string fileName, long expectedResult)
    {
        base.P2Test(fileName, expectedResult);
    }
}