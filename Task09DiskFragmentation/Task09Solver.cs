using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task09DiskFragmentation;

public class Task09Solver : ITaskSolver
{
    private DiskMap _diskMap;

    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        _diskMap = new DiskMap(reader.ReadLine() ?? throw new InvalidOperationException("Empty disk map given"));
    }

    public void SolveTask()
    {
        // _diskMap.DefragmentDiskByBlocks();
        _diskMap.DefragmentDiskByFiles();
        Console.WriteLine($"Check sum: { _diskMap.CountCheckSum()}");
    }
}