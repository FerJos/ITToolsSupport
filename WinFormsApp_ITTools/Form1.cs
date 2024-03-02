using ClassLibrary_ITTools;

namespace WinFormsApp_ITTools
{
    public partial class Form1 : Form
    {
        Class1 class1 = new Class1();

        public Form1()
        {
            InitializeComponent();

            string[] itemsList = class1.getOptionsList();
            object[] nuevosItems = new object[itemsList.Length];
            for (int i = 0; i < itemsList.Length; i++)
            {
                nuevosItems[i] = itemsList[i];
            }
            comboBox1.Items.AddRange(nuevosItems);
        }

        private string getTextItem()
        {
            Object? getObjItem = comboBox1.SelectedItem;
            if (getObjItem != null)
            {
                string? getStringItem = getObjItem.ToString();
                if (getStringItem != null)
                {
                    return getStringItem;
                }
            }
            return "";
        }

        private bool userAcceptInput(string question)
        {
            DialogResult result = MessageBox.Show(question, "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        private void runProcess(string cmdLine, bool runAsAdmin)
        {
            var (success, result) = class1.runProcessWindows(cmdLine, runAsAdmin);
            if (success)
            {
                MessageBox.Show(result, "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show(result, "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Enabled)
            {
                comboBox1.Enabled = false;
                textBox1.Enabled = false;
                button1.Enabled = false;
                Hide();

                string userInput = comboBox1.SelectedIndex.ToString();

                switch (userInput)
                {
                    case "0":
                        MessageBox.Show("Esta aplicación de consola ejecuta las herramientas integradas del sistema operativo ©Microsoft Windows para realizar mantenimiento y/o reparación al equipo.\nA continuación se muestra la documentación de algunos de los comandos utilizados.\n\n* sfc - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/sfc\n* chkdsk - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/chkdsk?tabs=event-viewer\n* DISM - https://learn.microsoft.com/es-mx/windows-hardware/manufacture/desktop/what-is-dism?view=windows-11\n* cleanmgr - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/cleanmgr\n* ipconfig - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/ipconfig\n* shutdown - https://learn.microsoft.com/es-mx/windows-server/administration/windows-commands/shutdown", "Documentación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case "1":
                        Close();
                        return;
                    case "2":
                        if (userAcceptInput("¿Desea que el comprobador de recursos intente reparar los archivos con problemas?"))
                        {
                            runProcess("sfc.exe /SCANNOW", true);
                        }
                        else
                        {
                            runProcess("sfc.exe /VERIFYONLY", true);
                        }
                        break;
                    case "3":
                        if (textBox1.Text == "")
                        {
                            if (!userAcceptInput("No se ha especificado una letra de unidad. ¿Desea continuar de todos modos? Se utilizará la unidad C: en su lugar."))
                            {
                                MessageBox.Show("Se canceló la operación.", "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                        }

                        runProcess("chkdsk.exe " + class1.convertInputToLetterVolume(textBox1.Text) + ": /f", true);
                        break;
                    case "4":
                        runProcess("chkdsk.exe " + class1.convertInputToLetterVolume(textBox1.Text) + ": /f /r /b", true);
                        break;
                    case "5":
                        runProcess("chkdsk.exe " + class1.convertInputToLetterVolume(textBox1.Text) + ": /f /r /b /x", true);
                        break;
                    case "6":
                        MessageBox.Show("CheckHealth. Paso 1/4.", "Ejecutar DISM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        runProcess("DISM.exe /Online /Cleanup-Image /CheckHealth", true);
                        MessageBox.Show("ScanHealth. Paso 2/4.", "Ejecutar DISM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        runProcess("DISM.exe /Online /Cleanup-Image /ScanHealth", true);
                        MessageBox.Show("RestoreHealth. Paso 3/4.", "Ejecutar DISM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (userAcceptInput("¿Ejecutar '/RestoreHealth' para realizar operaciones de reparación automáticamente? Esta operación puede tardar varios minutos."))
                        {
                            runProcess("DISM.exe /Online /Cleanup-Image /RestoreHealth", true);
                        }
                        MessageBox.Show("startComponentCleanup. Paso 4/4.", "Ejecutar DISM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (userAcceptInput("¿Ejecutar '/startComponentCleanup' para limpiar los componentes reemplazados y reducir el tamaño del almacén de componentes?"))
                        {
                            if (userAcceptInput("¿Ejecutar '/ResetBase' para restablecer la base de componentes reemplazados? ADVERTENCIA: Las actualizaciones de Windows instaladas no se pueden desinstalar si ejecuta '/startComponentCleanup' con '/ResetBase'."))
                            {
                                runProcess("DISM.exe /Online /Cleanup-Image /startComponentCleanup /ResetBase", true);
                            }
                            else
                            {
                                runProcess("DISM.exe /Online /Cleanup-Image /startComponentCleanup", true);
                            }
                        }
                        break;
                    case "7":
                        runProcess("cleanmgr.exe", false);
                        break;
                    case "8":
                        runProcess("SystemPropertiesProtection.exe", false);
                        break;
                    case "9":
                        runProcess("control.exe", false);
                        break;
                    case "10":
                        runProcess("msinfo32.exe", false);
                        break;
                    case "11":
                        runProcess("eventvwr.msc", false);
                        break;
                    case "12":
                        runProcess("MdSched.exe", false);
                        break;
                    case "13":
                        runProcess("lusrmgr.msc", false);
                        break;
                    case "14":
                        runProcess("mmc.exe", false);
                        break;
                    case "15":
                        runProcess("WMIC.exe", true);
                        break;
                    case "16":
                        runProcess("diskmgmt.msc", false);
                        break;
                    case "17":
                        runProcess("diskpart.exe", true);
                        break;
                    case "18":
                        runProcess("dxdiag.exe", false);
                        break;
                    case "19":
                        runProcess("hdwwiz.exe", true);
                        break;
                    case "20":
                        runProcess("fsmgmt.msc", false);
                        break;
                    case "21":
                        runProcess("mrt.exe", true);
                        break;
                    case "22":
                        runProcess("gpedit.msc", false);
                        break;
                    case "23":
                        runProcess("WSReset.exe", false);
                        break;
                    case "24":
                        runProcess("ipconfig /all", false);
                        break;
                    case "25":
                        if (userAcceptInput("El equipo se reiniciará dentro de 5 segundos, cualquier trabajo no guardado se perderá. ¿Continuar?"))
                        {
                            Console.WriteLine("El equipo se reiniciará dentro de 5 segundos...");
                            runProcess("shutdown /r /f /t 005", false);
                            Close();
                            return;
                        }
                        break;
                    case "26":
                        runProcess("echo ¡Hola, mundo!", false);
                        break;
                    case "27":
                        runProcess("echo ¡Hola, mundo!", true);
                        break;
                    default:
                        MessageBox.Show("Opción no implementada.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                Show();
                comboBox1.Enabled = true;
                textBox1.Enabled = true;
                button1.Enabled = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.Enabled && getTextItem() != "";
        }
    }
}
