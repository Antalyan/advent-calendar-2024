namespace AdventCalendar2024.Shared;

public interface ITaskSolver
{
    public void LoadTaskDataFromFile(string filePath);
    public void SolveTask();
}