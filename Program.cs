using AdventCalendar2024.Shared;
using AdventCalendar2024.Task01Lists;

string sourceDirectory = "";
string sourceFile = Path.Combine(sourceDirectory, @"Task01Lists\Data01.txt");
ITaskSolver taskSolver = new Task01Solver();
taskSolver.LoadTaskDataFromFile(sourceFile);
taskSolver.SolveTask();
