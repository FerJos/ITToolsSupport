using System.Diagnostics;

namespace WinFormsApp_ITTools_TaskKill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Process newProcess(string cmdLine)
        {
            Process proceso = new Process();
            proceso.StartInfo.FileName = "cmd.exe";
            proceso.StartInfo.Arguments = "/c" + cmdLine;
            proceso.StartInfo.UseShellExecute = true;
            proceso.StartInfo.CreateNoWindow = false;
            proceso.StartInfo.WorkingDirectory = Environment.GetEnvironmentVariable("USERPROFILE");
            return proceso;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Enabled)
            {
                Process proceso = newProcess("tasklist.exe");
                proceso.StartInfo.Arguments = proceso.StartInfo.Arguments + "& pause";
                try
                {
                    proceso.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudo iniciar el proceso. " + ex.Message, "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Enabled)
            {
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                textBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;

                if (radioButton1.Checked && !radioButton2.Checked)
                {
                    int? getPID = null;
                    try
                    {
                        getPID = Convert.ToInt32(textBox1.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo iniciar el proceso. " + ex.Message, "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    if (getPID != null && getPID >= 0)
                    {
                        Process proceso = newProcess("taskkill.exe /F /T /PID " + getPID.ToString());
                        proceso.StartInfo.UseShellExecute = false;
                        proceso.StartInfo.CreateNoWindow = true;

                        try
                        {
                            proceso.Start();
                            proceso.WaitForExit();
                            
                            switch (proceso.ExitCode)
                            {
                                case 0:
                                    MessageBox.Show("Se finalizó la tarea y sus procesos secundarios correctamente. (Código: 0)", "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    break;
                                case 128:
                                    MessageBox.Show("No se encontró la tarea '" + getPID.ToString() + "'. Haga click en 'Ver lista' para obtener el PID. (Código: 128)", "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    break;
                                default:
                                    MessageBox.Show("Posibles problemas al intentar finalizar la tarea. (Código: " + proceso.ExitCode.ToString() + ")", "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    MessageBox.Show(proceso.StandardOutput.ToString(), "Proceso - Standard Output", MessageBoxButtons.OK, MessageBoxIcon.None);
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("No se pudo iniciar el proceso. " + ex.Message, "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (!radioButton1.Checked && radioButton2.Checked)
                {
                    Process proceso = newProcess("taskkill.exe /F /T /IM " + textBox1.Text);
                    proceso.StartInfo.UseShellExecute = false;
                    proceso.StartInfo.CreateNoWindow = true;
                    if (checkBox2.Checked)
                    {
                        proceso.StartInfo.RedirectStandardOutput = true;
                    }

                    try
                    {
                        proceso.Start();
                        proceso.WaitForExit();

                        if (proceso.StartInfo.RedirectStandardOutput)
                        {
                            MessageBox.Show(proceso.StandardOutput.ReadToEnd(), "Proceso - Output", MessageBoxButtons.OK, MessageBoxIcon.None);
                        }

                        switch (proceso.ExitCode)
                        {
                            case 0:
                                MessageBox.Show("Se finalizó la tarea y sus procesos secundarios correctamente. (Código: 0)", "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (checkBox1.Checked)
                                {
                                    Close();
                                    return;
                                }
                                break;
                            case 128:
                                MessageBox.Show("No se encontró la tarea '" + textBox1.Text + "'. Haga click en 'Ver lista' para obtener el IM. (Código: 128)", "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                            default:
                                MessageBox.Show("Posibles problemas al intentar finalizar la tarea. (Código: " + proceso.ExitCode.ToString() + ")", "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo iniciar el proceso. " + ex.Message, "Proceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                textBox1.Enabled = true;
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button2.Enabled = textBox1.Text != "";
        }
    }
}
