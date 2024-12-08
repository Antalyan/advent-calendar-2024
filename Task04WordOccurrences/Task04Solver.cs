using AdventCalendar2024.Shared;

namespace AdventCalendar2024.Task04WordOccurrences;

public class Task04Solver : ITaskSolver
{
    private char[,] _letters;

    public void LoadTaskDataFromFile(string filePath)
    {
        using var reader = new StreamReader(filePath);
        List<string> wordList = new List<string>();
        string? line;
        // Read each line one by one
        while ((line = reader.ReadLine()) != null)
        {
            wordList.Add(line);
        }

        _letters = new char[wordList.Count, wordList[0].Length];
        foreach (var (wordIndex, word) in wordList.Select((item, index) => (index, item)))
        {
            foreach (var (letterIndex, letter) in word.Select((item, index) => (index, item)))
            {
                _letters[wordIndex, letterIndex] = letter;
            }
        }
    }

    private bool CellIsInside(int x, int y)
    {
        int rows = _letters.GetLength(0);
        int cols = _letters.GetLength(1);
        return x >= 0 && y >= 0 && x < rows && y < cols;
    }

    private bool CheckLetterOnPosition(int x, int y, char expectedLetter)
    {
        return CellIsInside(x, y) && _letters[x, y] == expectedLetter;
    }

    private int CountWordsStartingFromCell(int x, int y, string searchedWord)
    {
        int words = 0;
        foreach (var (modx, mody) in CoordinationHelper.GetAllCoordModifiers())
        {
            (int X, int Y) currentPos = (x, y);
            bool wordPresent = true;
            foreach (var letter in searchedWord)
            {
                if (!CheckLetterOnPosition(currentPos.X, currentPos.Y, letter))
                {
                    wordPresent = false;
                    break;
                }

                currentPos = (currentPos.X + modx, currentPos.Y + mody);
            }

            if (wordPresent)
            {
                words += 1;
            }
        }

        return words;
    }

    private int CountWordInAllDirections(string word)
    {
        int occurrences = 0;
        for (int x = 0; x < _letters.GetLength(0); x++)
        {
            for (int y = 0; y < _letters.GetLength(1); y++)
            {
                occurrences += CountWordsStartingFromCell(x, y, word);
            }
        }

        return occurrences;
    }

    private bool CheckCrossCenteredInCell(int x, int y, char centralLetter, char borderLetter1, char borderLetter2)
    {
        if (!CheckLetterOnPosition(x, y, centralLetter))
        {
            return false;
        }

        foreach (var (modx, mody) in CoordinationHelper.GetLeftDiagonalCoordModifiers())
        {
            (int X, int Y) currentPos = (x + modx, y + mody);
            if (!CellIsInside(currentPos.X, currentPos.Y))
            {
                return false;
            }

            char letterOnCurrentPos = _letters[currentPos.X, currentPos.Y];
            if (letterOnCurrentPos != borderLetter1 && letterOnCurrentPos != borderLetter2)
            {
                return false;
            }

            char mirrorExpectedLetter = letterOnCurrentPos == borderLetter1 ? borderLetter2 : borderLetter1;
            var mirrorCoord = CoordinationHelper.GetMirroredCoordModifier(modx, mody);
            (int X, int Y) mirroredPos = (x + mirrorCoord.X, y + mirrorCoord.Y);

            if (!CheckLetterOnPosition(mirroredPos.X, mirroredPos.Y, mirrorExpectedLetter))
            {
                return false;
            }
        }

        return true;
    }

    private int CountCrossWords(char centralLetter, char borderLetter1, char borderLetter2)
    {
        int occurrences = 0;
        for (int x = 1; x < _letters.GetLength(0) - 1; x++)
        {
            for (int y = 1; y < _letters.GetLength(1) - 1; y++)
            {
                if (CheckCrossCenteredInCell(x, y, centralLetter, borderLetter1, borderLetter2))
                {
                    occurrences += 1;
                }
            }
        }

        return occurrences;
    }
    
    public void SolveTask()
    {
        Console.WriteLine($"XMAS Word count: {CountWordInAllDirections("XMAS")}");
        Console.WriteLine($"MAS cross count: {CountCrossWords('A', 'M', 'S')}");
    }
}