using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task05Printer;

public class Task05Solver : ITaskSolver
{
    private readonly Graph _rules = new();
    private readonly List<List<int>> _printPageList = new();

    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        string? line;

        while ((line = reader.ReadLine()) != null && !string.IsNullOrWhiteSpace(line))
        {
            var numbers = line.Split("|");
            if (int.TryParse(numbers[0], out int a) && int.TryParse(numbers[1], out int b))
            {
                _rules.AddEdge(a, b);
            }
        }

        while ((line = reader.ReadLine()) != null)
        {
            var numbers = line.Split(",");
            _printPageList.Add(numbers.ToList().Select(int.Parse).ToList());
        }
    }

    private bool SequenceFulfillsRules(List<int> sequence)
    {
        foreach (var rule in _rules.Edges)
        {
            int fromIndex = sequence.IndexOf(rule.From);
            int toIndex = sequence.IndexOf(rule.To);
            if (fromIndex > -1 && toIndex > -1 && fromIndex > toIndex)
            {
                return false;
            }
        }

        return true;
    }

    private int CountMiddleNumbersOfRightSequences()
    {
        return _printPageList.Aggregate(0, (prev, sequence) =>
        {
            if (SequenceFulfillsRules(sequence))
            {
                Console.WriteLine($"Sequence passed: {string.Join(", ", sequence)}");
                return prev + sequence[sequence.Count / 2];
            }

            return prev;
        });
    }

    private List<int> CreateFixedSequence(List<int> originalSequence)
    {
        return _rules.GetTopologicalSort(originalSequence.ToHashSet());
    }

    private int CountMiddleNumbersOfFixedSequences()
    {
        return _printPageList.Aggregate(0, (prev, sequence) =>
        {
            if (!SequenceFulfillsRules(sequence))
            {
                // Console.WriteLine($"Sequence failed: {string.Join(", ", sequence)}");
                var correctedSequence = CreateFixedSequence(sequence);
                // Console.WriteLine($"Sequence corrected: {string.Join(", ", correctedSequence)}");
                if (!SequenceFulfillsRules(correctedSequence))
                {
                    Console.Error.WriteLine($"Correcting failed for sequence {string.Join(", ", sequence)}");
                }

                return prev + correctedSequence[correctedSequence.Count / 2];
            }

            return prev;
        });
    }

    private void PrepareBeforeTask()
    {
    }

    public void SolveTask()
    {
        PrepareBeforeTask();
        Console.WriteLine($"Middle sequence score: {CountMiddleNumbersOfRightSequences()}");
        Console.WriteLine($"Middle corrected sequence score: {CountMiddleNumbersOfFixedSequences()}");
    }
}