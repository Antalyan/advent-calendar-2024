using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task15Warehouse;

public class Task15UnitTest : TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task15Solver();
    }

    [Theory]
    [InlineData("T15-Sample01", 2028)]
    [InlineData("T15-Sample02", 10092)]
    [InlineData("T15-Fin", 1430536)]
    public override void P1Test(string fileName, long expectedResult, params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult, solverParams);
    }

    [Theory]
    [InlineData("T15-Sample02", 9021)]
    [InlineData("T15-Sample03", 618)]
    [InlineData("T15-Fin", 1452348)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult, solverParams);
    }
}