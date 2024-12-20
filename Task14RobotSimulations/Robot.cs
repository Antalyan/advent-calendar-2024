namespace AdventCalendar2024.Task14RobotSimulations;

public class Robot(Coordinate initialPosition, Coordinate speed)
{
    public Coordinate InitialPosition { get; } = initialPosition;
    public Coordinate Speed { get; } = speed;

    private int GetModularValue(int val, int mod)
    {
        int modVal = val % mod;
        return modVal < 0 ? modVal + mod : modVal;
    }

    public Coordinate CountPositionAfterTurns(int turns, Coordinate areaSize)
    {
        return (GetModularValue(InitialPosition.X + Speed.X * turns, areaSize.X),
            GetModularValue(InitialPosition.Y + Speed.Y * turns, areaSize.Y));
    }
}