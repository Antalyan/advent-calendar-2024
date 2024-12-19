namespace AdventCalendar2024.Task13MachineEquations;

public class LinearEquation
{
    public LinearEquation(long aCoef, long bCoef, long numCoef)
    {
        ACoef = aCoef;
        BCoef = bCoef;
        NumCoef = numCoef;
    }

    public long ACoef { get; }
    public long BCoef { get; }
    public long NumCoef { get; }
}