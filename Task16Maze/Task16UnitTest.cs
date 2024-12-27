using AdventCalendar2024.Shared;
using AdventCalendar2024.Task15Warehouse;
using Xunit;

namespace AdventCalendar2024.Task16Maze;

public class Task16UnitTest : TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task16Solver();
    }

    [Theory]
    [InlineData("T16-Sample01", 7036)]
    [InlineData("T16-Sample02", 11048)]
    [InlineData("T16-Fin", 102460)]
    public override void P1Test(string fileName, long expectedResult, params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult, solverParams);
    }

    // [Theory]
    // [InlineData("T15-Sample02", 9021)]
    // [InlineData("T15-Sample03", 618)]
    // [InlineData("T15-Fin", 1452348)]
    // public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    // {
    //     base.P2Test(fileName, expectedResult, solverParams);
    // }
}