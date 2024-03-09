using System.Diagnostics;
using System.Security.Principal;

namespace ClassLibrary_ITTools
{
    public class Class1
    {
        /// <summary>
        /// Solicitar una array de tipo string de las opciones disponibles.
        /// </summary>
        /// <returns>Array</returns>
        public string[] getOptionsList()
        {
            string[] tabla = {
                "!)     Documentación de los comandos de Windows.",
                "0)     Salir del programa.",
                "1)*    sfc - Comprobador de recursos.",
                "2)*    CHKDSK - Corregir errores del disco.",
                "3)*    CHKDSK - Corregir errores del disco, encontrar sectores defectuosos y recupera la información legible.",
                "4)*    CHKDSK - Desmontar la unidad, corregir errores del disco, encontrar sectores defectuosos y recupera la información legible.",
                "5)*    DISM - {/CheckHealth | /ScanHealth | /RestoreHealth}.",
                "6)     Cleanmgr - Libera espacio en el disco.",
                "7)     Abrir Protección del sistema.",
                "8)     Abrir Panel de control.",
                "9)     Abrir Información del sistema.",
                "10)    Abrir Visor de eventos.",
                "11)    Abrir Diagnóstico de memoria de Windows.",
                "12)**  Abrir Usuarios y grupos locales.",
                "13)    Microsoft Management Console.",
                "14)*   [EN DESUSO] Windows Management Instrumentation Console",
                "15)    Administración de discos.",
                "16)*   DISKPART.",
                "17)    Herramienta de diagnóstico de DirectX.",
                "18)*   Asistente para agregar hardware.",
                "19)    Carpetas compartidas.",
                "20)*   Herramienta de eliminación de software malintencionado de ©Microsoft Windows.",
                "21)**  Editor de directivas de grupo local.",
                "22)    Windows Store Reset.",
                "23)    ipConfig /All",
                "24)    Salir del programa y reiniciar el equipo (Se perderá cualquier trabajo no guardado)."
            };

            return tabla;
        }

        /// <summary>
        /// Verifica si la aplicación se está ejecutando con privilegios de administrador.
        /// </summary>
        /// <returns>
        /// Devuelve <c>true</c> si la aplicación se está ejecutando con privilegios de administrador; de lo contrario, <c>false</c>.
        /// </returns>
        public bool isRunningAsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private Process newProcess(string cmdLine, bool runAsAdmin)
        {
            Process proceso = new Process();
            proceso.StartInfo.FileName = "cmd.exe";
            proceso.StartInfo.Arguments = "/c" + cmdLine;
            proceso.StartInfo.UseShellExecute = false;
            proceso.StartInfo.CreateNoWindow = false;
            if (runAsAdmin)
            {
                proceso.StartInfo.WorkingDirectory = Environment.GetEnvironmentVariable("SystemRoot") + @"\System32";
                proceso.StartInfo.Verb = "runas";
            }
            else
            {
                proceso.StartInfo.WorkingDirectory = Environment.GetEnvironmentVariable("USERPROFILE");
            }
            return proceso;
        }

        /// <summary>
        /// Ejecuta un nuevo proceso utilizando la aplicación cmd.exe. Recomendado para la aplicación de Consola.
        /// </summary>
        /// <param name="cmdLine">La línea de comandos que se pasará al proceso cmd.exe.</param>
        /// <param name="runAsAdmin">Especifica si el proceso debe ejecutarse con privilegios de administrador.</param>
        public void runProcessConsole(string cmdLine, bool runAsAdmin, bool hideNewConsole)
        {
            Process proceso = newProcess(cmdLine, runAsAdmin);
            if (runAsAdmin && !isRunningAsAdmin())
            {
                Console.WriteLine("Se requiere permisos elevados para ejecutar este comando.");
                if (!hideNewConsole)
                {
                    Console.WriteLine("Se abrirá una nueva ventana una vez que un usuario administrador permita la ejecución del comando. No cierre esta ventana.");
                    proceso.StartInfo.UseShellExecute = true;
                    proceso.StartInfo.Arguments += "& pause";
                }
            }
            try
            {
                proceso.Start();
                proceso.WaitForExit();
                if (proceso.ExitCode == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.WriteLine("\n\n__________\nEl proceso a finalizado con el siguiente código: " + proceso.ExitCode + "\nExit time: " + proceso.ExitTime.ToString());
                Console.ResetColor();
            }
            catch (Exception errExc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n__________\nEl proceso no se pudo iniciar con el siguiente error: " + errExc.Message);
                Console.ResetColor();
                Console.Beep();
            }

            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        /// <summary>
        /// Ejecuta un nuevo proceso utilizando la aplicación cmd.exe. Recomendado para la aplicación Windows Forms.
        /// </summary>
        /// <param name="cmdLine">La línea de comandos que se pasará al proceso cmd.exe.</param>
        /// <param name="runAsAdmin">Especifica si el proceso debe ejecutarse con privilegios de administrador.</param>
        /// <returns>Número entero, 0 = Correcto, 1 = Posibles problemas, 2 = Error. Una cadena que representa la salida del proceso ejecutado.</returns>
        public (int, string) runProcessWindows(string cmdLine, bool runAsAdmin, bool showConsole)
        {
            Process proceso = newProcess(cmdLine, runAsAdmin);
            proceso.StartInfo.CreateNoWindow = true;
            if (showConsole)
            {
                proceso.StartInfo.UseShellExecute = true;
                proceso.StartInfo.CreateNoWindow = false;
                proceso.StartInfo.Arguments += "& pause";
            }
            try
            {
                proceso.Start();
                proceso.WaitForExit();
                if (proceso.ExitCode != 0)
                {
                    return (1, "El proceso a finalizado con el siguiente código: " + proceso.ExitCode.ToString() + "\nExit time: " + proceso.ExitTime.ToString());
                }
                return (0, "El proceso a finalizado con el siguiente código: " + proceso.ExitCode.ToString() + "\nExit time: " + proceso.ExitTime.ToString());
            }
            catch (Exception errExc)
            {
                return (2, "El proceso no se pudo iniciar con el siguiente error: " + errExc.Message);
            }
        }

        public string convertInputToLetterVolume(string? userInput)
        {
            string userInputAux = userInput ?? "c";
            if (userInputAux == " " || userInputAux.Length != 1)
            {
                userInputAux = "C";
            }
            else
            {
                userInputAux = userInputAux.ToUpper();
            }
            return userInputAux;
        }
    }
}