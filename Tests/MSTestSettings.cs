// Make sure tests are not executed in parallel, because the Converts class has static properties that are changed in the tests.
// Lucst

[assembly: DoNotParallelize]
