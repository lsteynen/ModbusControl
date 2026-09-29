# Installer
The MSI installer is now built with the WiX Toolset instead of Caphyon Advanced Installer.\
Using the HeatWave Visual Studio extension.\
Components.wxs is configured to harvests all required files from the relevant directories.\

### Installer properties
- Using WiX V7
- Single .msi file
- "Per Machine" (all users) install
- Has Add/Remove Programs icon
- Makes File association for .mbc files to open with ModbusControl.exe
- Harvests all required files from the relevant directories
- Adds "ModbusControl" folders with a few shortcuts to the Start menu
- Adds a desktop shortcut to ModbusControl.exe
- Creates a "C:\ProgramData\ModbusControl" folder for ModbusControl to store its global settings, because that requires admin rights.

### Release a new version 
- Update the ModbusControl version in "Modbuscontrol/Shared/AssemblyInfoShared.cs". The build number is automatically incremented.
- Make ModbusControl release build
- Make ModbusImport release build
- Make ModbusExport release build
- Do ModbusControl publish
- Check if Demo.mbc and MbcValidation.xsd are up to date
- Check if the User Manual docx is up to date, recreate PDF if needed
- Add change log to Release notes docx, recreate PDF
- Update ProductVersion in "Installer\Package.wxs" version 
- Make Installer release build
- Get the .msi file



