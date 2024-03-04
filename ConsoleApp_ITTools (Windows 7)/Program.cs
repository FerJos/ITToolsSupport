﻿using System;

namespace ConsoleApplication1
{
    class Program
    {
        static Class1 class1 = new Class1();

        static string getLetterVolume()
        {
            Console.Write("Escriba la letra de la unidad a la que desea trabajar (sin incluir los dos puntos): ");
            return class1.convertInputToLetterVolume(Console.ReadLine());
        }

        static bool userAcceptInput(string question)
        {
            Console.Write(question + " (S/N): ");

            string userInput = Console.ReadLine() ?? "";
            userInput = userInput.ToUpper();
            switch (userInput)
            {
                case "S":
                    return true;
                case "N":
                    return false;
                default:
                    return userAcceptInput(question);
            }
        }

        static void Main(string[] args)
        {
            string appName = "Console App IT Tools (Windows 7)";
            Console.Title = appName;
            Console.SetWindowSize(130, 35);

            string[] itemsList = class1.getOptionsList();
            bool runConsoleApp = true;

            while (runConsoleApp)
            {
                Console.WriteLine(appName + " - Herramientas para el mantenimiento y/o reparación del equipo Windows.");
                if (class1.isRunningAsAdmin())
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Esta aplicación se está ejecutando como administrador.");
                    Console.ResetColor();
                }
                Console.WriteLine();

                for (int i = 0; i < itemsList.Length; i++)
                {
                    Console.WriteLine(itemsList[i]);
                }

                Console.WriteLine("(*) Herramienta que requiere permisos administrativos.");
                Console.Write("Elija una opción: ");

                string userInput = Console.ReadLine() ?? "0";
                if (userInput == "" || userInput.ToLower() == "exit")
                {
                    userInput = "0";
                }
                Console.Clear();

                switch (userInput)
                {
                    case "!":
                        Console.WriteLine("Esta aplicación de consola ejecuta las herramientas integradas del sistema operativo ©Microsoft Windows para realizar mantenimiento y/o reparación al equipo.\n");

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "0":
                        Console.WriteLine("Saliendo del programa...");
                        runConsoleApp = false;
                        break;
                    case "1":
                        if (userAcceptInput("¿Desea que el comprobador de recursos intente reparar los archivos con problemas?"))
                        {
                            Console.WriteLine("# SCANNOW");
                            class1.runProcessConsole("sfc.exe /SCANNOW", true, false);
                        }
                        else
                        {
                            Console.WriteLine("# VERIFYONLY");
                            class1.runProcessConsole("sfc.exe /VERIFYONLY", true, false);
                        }

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        userInput = getLetterVolume();
                        class1.runProcessConsole("chkdsk.exe " + userInput + ": /f", true, false);

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "3":
                        userInput = getLetterVolume();
                        class1.runProcessConsole("chkdsk.exe " + userInput + ": /f /r /b", true, false);

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "4":
                        userInput = getLetterVolume();
                        class1.runProcessConsole("chkdsk.exe " + userInput + ": /f /r /b /x", true, false);

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "5":
                        // Opción 5 eliminada.
                        Console.Clear();
                        break;
                    case "6":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("cleanmgr.exe", false, true);

                        Console.Clear();
                        break;
                    case "7":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("SystemPropertiesProtection.exe", false, true);

                        Console.Clear();
                        break;
                    case "8":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("control.exe", false, true);

                        Console.Clear();
                        break;
                    case "9":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("msinfo32.exe", false, true);

                        Console.Clear();
                        break;
                    case "10":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("eventvwr.msc", false, true);

                        Console.Clear();
                        break;
                    case "11":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("MdSched.exe", false, true);

                        Console.Clear();
                        break;
                    case "12":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("lusrmgr.msc", false, true);

                        Console.Clear();
                        break;
                    case "13":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("mmc.exe", false, true);

                        Console.Clear();
                        break;
                    case "14":
                        class1.runProcessConsole("WMIC.exe", true, false);

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "15":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("diskmgmt.msc", false, true);

                        Console.Clear();
                        break;
                    case "16":
                        class1.runProcessConsole("diskpart.exe", true, false);

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "17":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("dxdiag.exe", false, true);

                        Console.Clear();
                        break;
                    case "18":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("hdwwiz.exe", true, true);

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "19":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        Console.WriteLine("No cierre esta ventana.");
                        class1.runProcessConsole("fsmgmt.msc", false, true);

                        Console.Clear();
                        break;
                    case "20":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("mrt.exe", true, true);

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "21":
                        Console.WriteLine("Se ha iniciado una nueva ventana. Continúe desde la nueva ventana...");
                        class1.runProcessConsole("gpedit.msc", false, true);

                        Console.Clear();
                        break;
                    case "22":
                        // Opción 22 eliminada.
                        Console.Clear();
                        break;
                    case "23":
                        class1.runProcessConsole("ipconfig /all", false, false);

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "24":
                        if (userAcceptInput("El equipo se reiniciará dentro de 5 segundos, cualquier trabajo no guardado se perderá. ¿Continuar?"))
                        {
                            Console.WriteLine("El equipo se reiniciará dentro de 5 segundos...");
                            class1.runProcessConsole("shutdown /r /f /t 005", false, true);
                            runConsoleApp = false;
                            break;
                        }

                        Console.Clear();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opción inválida. Ingrese una opción válida.");
                        Console.ResetColor();

                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
