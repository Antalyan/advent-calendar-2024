using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task12GardenArea;

public class Task12UnitTest : TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task12Solver();
    }

    [Theory]
    [InlineData("T12-Sample01", 140)]
    [InlineData("T12-Sample02", 1930)]
    [InlineData("T12-Fin", 1431440)]
    public override void P1Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult);
    }

    [Theory]
    [InlineData("T12-Sample01", 80)]
    [InlineData("T12-Sample02", 1206)]
    [InlineData("T12-Sample03", 236)]
    [InlineData("T12-Sample04", 368)]
    [InlineData("T12-Sample05", 436)]
    [InlineData("T12-Fin", 869070)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult);
    }
}