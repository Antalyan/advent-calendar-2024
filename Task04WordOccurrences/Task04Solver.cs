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

    private bool CellIsInside(Coordinate coordinate)
    {
        int rows = _letters.GetLength(0);
        int cols = _letters.GetLength(1);
        return coordinate.X >= 0 && coordinate.Y >= 0 && coordinate.X < rows && coordinate.Y < cols;
    }

    private bool CheckLetterOnPosition(Coordinate coordinate, char expectedLetter)
    {
        return CellIsInside(coordinate) && _letters[coordinate.X, coordinate.Y] == expectedLetter;
    }

    private int CountWordsStartingFromCell(Coordinate coordinate, string searchedWord)
    {
        int words = 0;
        foreach (var (modx, mody) in CoordinationHelper.GetAllCoordModifiers())
        {
            Coordinate currentPos = (coordinate.X, coordinate.Y);
            bool wordPresent = true;
            foreach (var letter in searchedWord)
            {
                if (!CheckLetterOnPosition(currentPos, letter))
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
                occurrences += CountWordsStartingFromCell((x, y), word);
            }
        }

        return occurrences;
    }

    private bool CheckCrossCenteredInCell(Coordinate coordinate, char centralLetter, char borderLetter1, char borderLetter2)
    {
        if (!CheckLetterOnPosition(coordinate, centralLetter))
        {
            return false;
        }

        foreach (var (modx, mody) in CoordinationHelper.GetLeftDiagonalCoordModifiers())
        {
            Coordinate currentPos = (coordinate.X + modx, coordinate.Y + mody);
            if (!CellIsInside(currentPos))
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
            Coordinate mirroredPos = (coordinate.X + mirrorCoord.X, coordinate.Y + mirrorCoord.Y);

            if (!CheckLetterOnPosition(mirroredPos, mirrorExpectedLetter))
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
                if (CheckCrossCenteredInCell((x, y), centralLetter, borderLetter1, borderLetter2))
                {
                    occurrences += 1;
                }
            }
        }

        return occurrences;
    }
    
    public long SolveTaskP1()
    {
        return CountWordInAllDirections("XMAS");
    }
    
    public long SolveTaskP2()
    {
        return CountCrossWords('A', 'M', 'S');
    }
}