using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task07Equations;

public class Task07UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task07Solver();
    }

    [Theory]
    [InlineData("T07-Sample01", 3749)]
    [InlineData("T07-Fin", 2314935962622)]
    public override void P1Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T07-Sample01", 11387)]
    [InlineData("T07-Fin", 401477450831495)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult);
    }
}