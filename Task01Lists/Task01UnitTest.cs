using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task01Lists;

public class Task01UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task01Solver();
    }

    [Theory]
    [InlineData("T01-Sample01", 11)]
    [InlineData("T01-Fin", 1319616)]
    public override void P1Test(string fileName, long expectedResult, params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T01-Sample01", 31)]
    [InlineData("T01-Fin", 27267728)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult);
    }
}