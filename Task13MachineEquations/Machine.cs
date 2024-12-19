namespace AdventCalendar2024.Task13MachineEquations;

public class Machine(CoordinateLarge buttonA, CoordinateLarge buttonB, CoordinateLarge prize)
{
    public CoordinateLarge ButtonA { get; } = buttonA;
    public CoordinateLarge ButtonB { get; } = buttonB;
    public CoordinateLarge Prize { get; set; } = prize;
}