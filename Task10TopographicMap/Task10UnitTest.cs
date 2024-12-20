using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task10TopographicMap;

public class Task10UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task10Solver();
    }

    [Theory]
    [InlineData("T10-Sample01", 36)]
    [InlineData("T10-Fin", 822)]
    public override void P1Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T10-Sample01", 81)]
    [InlineData("T10-Fin", 1801)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult);
    }
}