using AdventCalendar2024.Shared;
using Xunit;

namespace AdventCalendar2024.Task03InstructionParser;

public class Task03UnitTest: TaskUnitTestCommon
{
    protected override ITaskSolver CreateTaskSolver()
    {
        return new Task03Solver();
    }

    [Theory]
    [InlineData("T03-Sample01", 161)]
    [InlineData("T03-Fin", 187825547)]
    public override void P1Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P1Test(fileName, expectedResult);
    }
    
    [Theory]
    [InlineData("T03-Sample01", 48)]
    [InlineData("T03-Fin", 85508223)]
    public override void P2Test(string fileName, long expectedResult,  params object[] solverParams)
    {
        base.P2Test(fileName, expectedResult);
    }
}