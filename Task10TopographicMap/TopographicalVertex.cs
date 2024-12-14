namespace AdventCalendar2024.Task10TopographicMap;

public class TopographicalVertex(Coordinate coordinates, int value)
{
    public int Value => value;
    public Coordinate Coordinates => coordinates;

    public List<TopographicalVertex> SurroundingVertices { get; } = new();

    public override int GetHashCode()
    {
        return HashCode.Combine(coordinates.X, coordinates.Y);
    }
    
    public override bool Equals(object? obj)
    {
        if (obj is TopographicalVertex other)
        {
            return Coordinates == other.Coordinates;
        }
        return false;
    }
}