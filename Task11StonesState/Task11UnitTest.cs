using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task11StonesState;

public class Task11UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task11Solver();
    }

    [Theory]
    [InlineData("T11-Sample01", 55312)]
    [InlineData("T11-Fin", 217812)]
    public override void P1Test(string fileName, long expectedResult)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T11-Fin", 259112729857522)]
    public override void P2Test(string fileName, long expectedResult)
    {
        base.P2Test(fileName, expectedResult);
    }
}