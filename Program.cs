using AdventCalendar2024.Shared;
using AdventCalendar2024.Task13MachineEquations;

string sourceDirectory = Constants.DataFilePath;
string dayNumber = "13";
string sourceFile = Path.Combine(sourceDirectory, $"T{dayNumber}-Fin.txt");
ITaskSolver taskSolver = new Task13Solver();
taskSolver.LoadTaskDataFromFile(sourceFile);
Console.WriteLine(taskSolver.SolveTaskP1());
Console.WriteLine(taskSolver.SolveTaskP2());
