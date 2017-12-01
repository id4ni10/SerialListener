using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SerialListener
{
    public partial class Listener : Form
    {
        public Listener()
        {
            InitializeComponent();

            CheckForIllegalCrossThreadCalls = false;

            var names = SerialPort.GetPortNames();

            if (names.Any())
                comboBox1.DataSource = names;
            else
                comboBox1.Enabled = false;
        }

        protected override void OnResize(EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon.Visible = true;

                Hide();
            }
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;

            Show();

            ResumeLayout();
        }

        private async void setup_Click(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                if (serialPort.IsOpen)
                    serialPort.Close();

                textBox.Clear();

                progressBar.Step = 1;
                progressBar.Value = 0;
                progressBar.Maximum = 99;

                Cursor = Cursors.WaitCursor;

                textBox.Text += $"Iniciando os testes...{Environment.NewLine}";

                Progress();

                try
                {
                    if (comboBox1.DataSource == null)
                        throw new Exception("Objeto COM não encontrado!");

                    serialPort.PortName = comboBox1.SelectedItem.ToString();
                    serialPort.BaudRate = 9600;
                    serialPort.DataBits = 8;
                    serialPort.Parity = Parity.None;
                    serialPort.StopBits = StopBits.OnePointFive;
                    serialPort.Handshake = Handshake.None;
                    serialPort.Encoding = Encoding.Default;

                    textBox.Text += $"Conectando: {serialPort.PortName} BaudRate: {serialPort.BaudRate}{Environment.NewLine}";

                    Progress();

                    //Email.send(email.Text);

                    Progress();

                    serialPort.Open();

                    textBox.Text += $"Serial OK!{Environment.NewLine}";
                }
                catch (Exception exp)
                {
                    textBox.Text += $"{Environment.NewLine}{exp.Message}";
                }
                finally
                {
                    progressBar.Value = 0;

                    Cursor = Cursors.Default;
                }
            });
        }

        private void Progress()
        {
            for (int i = 0; i < 33; i++)
                progressBar.Value += 1;

            Thread.Sleep(1000);
        }

        DateTime sent = DateTime.Now;

        private async void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    var existing = serialPort.ReadExisting();

                    if (!string.IsNullOrWhiteSpace(existing))
                    {
                        if (alerta.Value < Convert.ToInt32(existing))
                        {
                            var value = $"{Environment.NewLine}Alerta: {existing}°C";

                            textBox.Text += value;

                            if (sent.AddHours(1) < DateTime.Now) {

                                //TODO: check this

                                //Email.send(email.Text, value);

                                sent = DateTime.Now;
                            }
                        }
                    }

                    textBox.SelectionStart = textBox.TextLength;
                    textBox.ScrollToCaret();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            });
        }

        protected override void OnClosed(EventArgs e)
        {
            if (serialPort.IsOpen)
                serialPort.Close();

            base.OnClosed(e);
        }
    }
}
