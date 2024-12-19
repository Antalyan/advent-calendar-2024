using Xunit;

namespace AdventCalendar2024.Shared;

public abstract class TaskUnitTestCommon
{
    protected abstract ITaskSolver CreateTaskSolver();
    
    private ITaskSolver CreateSolver(string fileName)
    {
        string sourceFile = Path.Combine(Constants.DataFilePath, $"{fileName}.txt");
        ITaskSolver taskSolver = CreateTaskSolver();
        taskSolver.LoadTaskDataFromFile(sourceFile);
        return taskSolver;
    }
    
    public virtual void P1Test(string fileName, long expectedResult)
    {
        var solver = CreateSolver(fileName);
        Assert.Equal(solver.SolveTaskP1(), expectedResult);
    }
    
    public virtual void P2Test(string fileName, long expectedResult)
    {
        var solver = CreateSolver(fileName);
        Assert.Equal(solver.SolveTaskP2(), expectedResult);
    }
}