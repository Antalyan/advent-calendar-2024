namespace AdventCalendar2024.Task09DiskFragmentation;

public class DiskMap
{
    private readonly int[] _blocks;

    private const int EmptySpace = -1;

    public DiskMap(string diskMap)
    {
        int blockId = 0;
        _blocks = diskMap
            .SelectMany((c, i) => Enumerable.Repeat(i % 2 == 0 ? blockId++ : EmptySpace, int.Parse(c.ToString())))
            .ToArray();
    }

    private void MovePointerToNextFreePos(ref int pointer)
    {
        while (_blocks[pointer] != EmptySpace)
        {
            pointer++;
        }
    }

    private void MovePointerToPreviousOccupiedPos(ref int pointer)
    {
        while (_blocks[pointer] == EmptySpace)
        {
            pointer--;
        }
    }

    private void SwapPointedBlockValues(int pointerA, int pointerB)
    {
        (_blocks[pointerA], _blocks[pointerB]) = (_blocks[pointerB], _blocks[pointerA]);
    }

    private int GetFileSizeByLastPos(int endPointer)
    {
        int currentPointer = endPointer;
        while (currentPointer > 0 && _blocks[endPointer] == _blocks[currentPointer])
        {
            currentPointer--;
        }

        return endPointer - currentPointer;
    }

    private bool IsConsecutiveFreeSpace(int startPointer, int desiredLength)
    {
        for (int i = 0; i < desiredLength; i++)
        {
            if (_blocks[startPointer + i] != EmptySpace)
            {
                return false;
            }
        }

        return true;
    }

    // To further optimimize, the linked list of free spaces could be pre-computed
    private int FindStartOfFreeSpace(int startPointer, int endPointer, int desiredLength)
    {
        for (int i = startPointer; i <= endPointer - desiredLength + 1; i++)
        {
            if (IsConsecutiveFreeSpace(i, desiredLength)) return i;
        }

        return -1;
    }

    public void DefragmentDiskByBlocks()
    {
        int leftPointer = 0;
        int rightPointer = _blocks.Length - 1;

        MovePointerToNextFreePos(ref leftPointer);
        MovePointerToPreviousOccupiedPos(ref rightPointer);
        while (leftPointer <= rightPointer)
        {
            SwapPointedBlockValues(leftPointer, rightPointer);
            MovePointerToNextFreePos(ref leftPointer);
            MovePointerToPreviousOccupiedPos(ref rightPointer);
        }
    }

    public void DefragmentDiskByFiles()
    {
        int leftPointer = 0;
        int rightPointer = _blocks.Length - 1;

        MovePointerToPreviousOccupiedPos(ref rightPointer);
        while (leftPointer < rightPointer)
        {
            int fileSize = GetFileSizeByLastPos(rightPointer);
            int relocationStart = FindStartOfFreeSpace(leftPointer, rightPointer, fileSize);
            if (relocationStart != -1)
            {
                for (int i = 0; i < fileSize; i++)
                {
                    SwapPointedBlockValues(relocationStart + i, rightPointer - i);
                }
            }
            else
            {
                rightPointer -= fileSize;
            }

            MovePointerToPreviousOccupiedPos(ref rightPointer);
        }
    }

    public long CountCheckSum()
    {
        return _blocks.Select((block, index) => block == EmptySpace ? 0 : (long)block * index).Sum();
    }
}