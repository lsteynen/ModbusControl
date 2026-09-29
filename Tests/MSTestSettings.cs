// Make sure tests are not executed in parallel, because the Converts class has static properties that are changed in the tests
// Check MSTestSettings.cs: do not activate [assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]
// Luc Steynen

//[assembly: Parallelize(Scope = ExecutionScope.MethodLevel)]
