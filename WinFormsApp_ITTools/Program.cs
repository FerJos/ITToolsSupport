using ClassLibrary_ITTools;

namespace WinFormsApp_ITTools
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Class1 class1 = new Class1();
            if (class1.isRunningAsAdmin())
            {
                MessageBox.Show("Esta aplicación se está ejecutando como administrador.", "Administrador", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Application.Run(new Form1());
        }
    }
}