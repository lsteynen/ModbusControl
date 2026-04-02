# ModbusControl

ModbusControl is a set of 3 Windows Modbus client programs. 
Written in C# with CS, for .NET framework, currenty with VS2019.
## Features
- ModbusControl - a Windows GUI program
- ModbusImport - a command line import tool
- ModbusExport - a command line export tool
- Supports RTU serial port, Modbus/TCP and RTU-over-TCP.
- Supports 16 and 32-bit registers; int, uint, hex, binary and float formats, bit fields and strings. 
- User manual.

## Development
It is best to start Visual Studio with the 'noscale' option. This launches Visual Studio without scaling (i.e., with scaling set to 100%, 96 DPI).
If scaling is enabled, unexpected behavior may occur when modifying form layouts because mixed DPI values can be introduced. It is recommended to keep everything at 96 DPI.

**Command:**
"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\devenv.exe" /noscale

