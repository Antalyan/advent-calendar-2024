using System.Text.RegularExpressions;
using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task03InstructionParser;

public class Task03Solver : ITaskSolver
{
    private string _inputString;

    public void LoadTaskDataFromFile(string filePath)
    {
        try
        {
            _inputString = File.ReadAllText(filePath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    private void UpdateScore(string inputString, ref int scoreToUpdate)
    {
        var numberPattern = @"\d{1,3}";
        var numMatches = Regex.Matches(inputString, numberPattern).Select(m => int.Parse(m.Value))
            .ToArray();
        scoreToUpdate += numMatches[0] * numMatches[1];
    }

    private int EvaluateInput()
    {
        string topPattern = @"mul\(\d{1,3},\d{1,3}\)";
        int score = 0;
        foreach (Match mulMatch in Regex.Matches(_inputString, topPattern))
        {
            UpdateScore(mulMatch.Value, ref score);
        }

        return score;
    }

    private int EvaluateInputWithEnables()
    {
        string topPattern = @"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)";
        int score = 0;
        bool instructionsEnabled = true;
        foreach (Match mulMatch in Regex.Matches(_inputString, topPattern))
        {
            switch (mulMatch.Value)
            {
                case "do()":
                {
                    instructionsEnabled = true;
                    break;
                }
                case "don't()":
                {
                    instructionsEnabled = false;
                    break;
                }
                default:
                {
                    if (instructionsEnabled)
                    {
                        UpdateScore(mulMatch.Value, ref score);
                    }

                    break;
                }
            }
        }

        return score;
    }
    
    public void SolveTask()
    {
        Console.WriteLine($"Multiplication sum result: {EvaluateInput()}");
        Console.WriteLine($"Multiplication sum result with enables: {EvaluateInputWithEnables()}");
    }
}