using AdventCalendar2024.Shared;
using AdventCalendar2024.Task13MachineEquations;
using Xunit;

namespace AdventCalendar2024.Task14RobotSimulations;

public class Task14UnitTest : TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task14Solver();
    }

    [Theory]
    [InlineData("T14-Sample01", 12, 11, 7)]
    [InlineData("T14-Fin", 218433348, 101, 103)]
    public override void P1Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult, solverParams);
    }

    [Theory]
    [InlineData("T14-Fin", 0, 101, 103)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult, solverParams);
    }
}