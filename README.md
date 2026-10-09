# ModbusControl

ModbusControl is a Windows-based Modbus client application for testing, monitoring, and interacting with Modbus/TCP and Modbus serial/RTU devices.
It also includes command-line tools for exporting and importing Modbus registers.

<img src="ModbusControl-main.png" alt="ModbusControl main window" title="ModbusControl" width="500"/>

## Features
- RTU serial, Modbus/TCP and RTU-over-TCP protocols
- 16-bit and 32-bit registers
- Int, uint, hex, binary and float formats
- Expandable bit fields
- Enumerations
- Strings
- Save and load projects as '.mbc' XML files
- ModbusExport.exe — command-line tool for exporting Modbus registers
- ModbusImport.exe — command-line tool for importing Modbus registers

[**User manual**](https://github.com/lsteynen/ModbusControl/blob/master/Manuals/ModbusControl%20-%20User%20manual.pdf) |
[**Release notes**](https://github.com/lsteynen/ModbusControl/blob/master/Manuals/ModbusControl%20-%20Release%20notes.pdf)

## Installer
[**MSI installer**](https://github.com/lsteynen/ModbusControl/releases/latest)

ModbusControl is a self-contained .NET 9 app, which means no .NET 9 runtime needs to be installed

## License
ModbusControl is open-source and released under the [GNU General Public License v3.0](https://github.com/lsteynen/ModbusControl/blob/master/LICENSE).

## Development
Built with C# on .NET 9 using Visual Studio 2026.


It’s recommended to start Visual Studio with the '/noscale' option. This forces Visual Studio to run without DPI scaling (equivalent to 100% scaling or 96 DPI). 
If scaling is enabled, modifying form layouts may lead to unexpected behavior because mixed DPI values can be introduced. To avoid layout issues, keep the environment at 96 DPI whenever possible.
