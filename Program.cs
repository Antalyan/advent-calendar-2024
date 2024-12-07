using AdventCalendar2024.Shared;
using AdventCalendar2024.Task01Lists;
using AdventCalendar2024.Task05PrinterOrder;

string sourceDirectory = "";
string dayNumber = "05";
string dayName = "PrinterOrder";
bool useSampleData = true;
string sourceFile = Path.Combine(sourceDirectory, $@"Task{dayNumber}{dayName}\Data{(useSampleData ? "Sample" : "")}{dayNumber}.txt");
ITaskSolver taskSolver = new Task05Solver();
taskSolver.LoadTaskDataFromFile(sourceFile);
taskSolver.SolveTask();
