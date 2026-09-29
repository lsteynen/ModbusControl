namespace ModbusControl
{
    internal static class Program
    {
        
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.SetHighDpiMode(HighDpiMode.SystemAware);

            Application.Run(new MainForm());
        }
    }
}