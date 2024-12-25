using AdventCalendar2024.Shared;
using AdventCalendar2024.Task14RobotSimulations;

string sourceDirectory = Constants.DataFilePath;
string dayNumber = "14";
string sourceFile = Path.Combine(sourceDirectory, $"T{dayNumber}-Fin.txt");
ITaskSolver taskSolver = new Task14Solver();
taskSolver.LoadTaskDataFromFile(sourceFile);
taskSolver.SetSolverParams(101, 103);
Console.WriteLine(taskSolver.SolveTaskP1());
Console.WriteLine(taskSolver.SolveTaskP2());
