using AdventCalendar2024.Shared;
using AdventCalendar2024.Task12GardenArea;

string sourceDirectory = Constants.DataFilePath;
string dayNumber = "12";
string sourceFile = Path.Combine(sourceDirectory, $"T{dayNumber}-Fin.txt");
ITaskSolver taskSolver = new Task12Solver();
taskSolver.LoadTaskDataFromFile(sourceFile);
Console.WriteLine(taskSolver.SolveTaskP1());
Console.WriteLine(taskSolver.SolveTaskP2());
