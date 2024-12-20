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
    
    public virtual void P1Test(string fileName, long expectedResult, params object[] solverParams)
    {
        var solver = CreateSolver(fileName);
        solver.SetSolverParams(solverParams);
        Assert.Equal(expectedResult, solver.SolveTaskP1());
    }
    
    public virtual void P2Test(string fileName, long expectedResult, params object[] solverParams)
    {
        var solver = CreateSolver(fileName);
        solver.SetSolverParams(solverParams);
        Assert.Equal(expectedResult, solver.SolveTaskP2());
    }
}