namespace AdventCalendar2024.Shared;

public interface ITaskSolver
{
    public void LoadTaskDataFromFile(string filePath);
    public long SolveTaskP1();
    public long SolveTaskP2();
    public void SetSolverParams(params object[] solverParams);
}