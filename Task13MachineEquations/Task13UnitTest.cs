using AdventCalendar2024.Shared;
using AdventCalendar2024.Task12GardenArea;
using Xunit;

namespace AdventCalendar2024.Task13MachineEquations;

public class Task13UnitTest : TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task13Solver();
    }

    [Theory]
    [InlineData("T13-Sample01", 480)]
    [InlineData("T13-Fin", 37901)]
    public override void P1Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult);
    }

    [Theory]
    [InlineData("T13-Sample01", 459236326669 + 416082282239)]
    [InlineData("T13-Fin", 77407675412647)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult);
    }
}