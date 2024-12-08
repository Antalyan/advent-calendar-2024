using System.Text.RegularExpressions;
using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task02ListReports;

public class Task02Solver : ITaskSolver
{
    private readonly List<List<int>> _reports = [];

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
                List<int> levels = [];
                foreach (var col in columns)
                {
                    if (!int.TryParse(col, out int level))
                    {
                        Console.Error.WriteLine($"Incorrect value in list: {col}");
                        return;
                    }

                    levels.Add(level);
                }

                if (levels.Count > 0)
                {
                    _reports.Add(levels);
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine($"Reports in list: {_reports.Count}");
    }

    private int GetFailureIndex(List<int> report, bool shouldBeAscending)
    {
        for (int i = 1; i < report.Count; i++)
        {
            if ((shouldBeAscending && report[i - 1] >= report[i]) ||
                (!shouldBeAscending && report[i - 1] <= report[i]))
            {
                return i - 1;
            }

            int levelDifference = int.Abs(report[i - 1] - report[i]);
            if (levelDifference > 3)
            {
                return i - 1;
            }
        }

        return -1;
    }

    private bool IsReportSafe(List<int> report, int tolerateFailures = 0)
    {
        if (report.Count < 2)
        {
            return true;
        }

        bool shouldBeAscending = report[0] < report[1];
        int failureIndex = GetFailureIndex(report, shouldBeAscending);

        if (failureIndex > -1)
        {
            if (tolerateFailures > 0)
            {
                // Try to remove first two elements (if sorting type is chosen incorrectly) and elements when problem was found
                List<int> problematicIndices =
                [
                    0,
                    1,
                    failureIndex,
                    failureIndex + 1
                ];
                return problematicIndices.Any(index =>
                {
                    var subList = report.ToList();
                    subList.RemoveAt(index);
                    return IsReportSafe(subList, tolerateFailures - 1);
                });
            }

            return false;
        }

        return true;
    }

    private int GetSafetyWithToleranceLinear()
    {
        return _reports.Count(r => IsReportSafe(r, 1));
    }

    private int GetSafetyWithToleranceQuadratic()
    {
        return _reports.Count(r =>
        {
            for (int i = 0; i < r.Count; i++)
            {
                var listCopy = r.ToList();
                listCopy.RemoveAt(i);
                if (IsReportSafe(listCopy))
                {
                    return true;
                }
            }

            return false;
        });
    }

    public void SolveTask()
    {
        Console.WriteLine($"Safe reports: {_reports.Count(r => IsReportSafe(r))}");
        Console.WriteLine($"Safe reports with toleration 1 (linear): {GetSafetyWithToleranceLinear()}");
        Console.WriteLine($"Safe reports with toleration 1 (quadratic): {GetSafetyWithToleranceQuadratic()}");
    }
}