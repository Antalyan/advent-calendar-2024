using System.Text.RegularExpressions;
using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task01Lists;

public class Task01Solver: ITaskSolver
{
    private readonly List<int> _listA = [];
    private readonly List<int> _listB = [];
    
    public void LoadTaskDataFromFile(string filePath)
    {
        try
        {
            using var reader = new StreamReader(filePath);
            string? line;
            // Read each line one by one
            while ((line = reader.ReadLine()) != null)
            {
                string[] columns = Regex.Split(line, @"\s+");
                if (columns.Length != 2)
                {
                    Console.Error.WriteLine($"Length exceeded on reading cols: {columns.Length}, {columns}");
                    return;
                }

                if (!int.TryParse(columns[0], out int valA) || !int.TryParse(columns[1], out int valB))
                {
                    Console.Error.WriteLine($"Values not parsed: {columns[0]}, {columns[1]}");
                    return;
                }
                
                _listA.Add(valA);
                _listB.Add(valB);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine($"Elements in list: {_listA.Count}");
    }

    private int GetListDistance()
    {
        return Enumerable.Range(0, _listA.Count).Aggregate(0, (acc, index) => acc + int.Abs(_listA[index] - _listB[index]));
    }
    
    private int GetSimilarityScore()
    {
        int indexB = 0;
        return Enumerable.Range(0, _listA.Count).Aggregate(0, (acc, indexA) =>
        {
            while (indexB < _listB.Count && _listB[indexB] < _listA[indexA])
            {
                // Move pointer forwards until finding next suitable value
                indexB++;
            }
            
            int occurrencesFound = 0;
            int sameBPosition = indexB;
            while (sameBPosition < _listB.Count && _listA[indexA] == _listB[sameBPosition])
            {
                occurrencesFound += 1;
                sameBPosition++;
            }

            return acc + (_listA[indexA] * occurrencesFound);
        });
    }

    public long SolveTaskP1()
    {
        _listA.Sort();
        _listB.Sort();
        return GetListDistance();
    }
    
    public long SolveTaskP2()
    {
        _listA.Sort();
        _listB.Sort();
        return GetSimilarityScore();
    }
}