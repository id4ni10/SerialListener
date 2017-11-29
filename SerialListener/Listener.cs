using System;
using System.IO.Ports;
using System.Linq;
using System.Text;
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

            Activate();

            BringToFront();
        }

        private void setup_Click(object sender, EventArgs e)
        {
            textBox.Clear();

            progressBar.Step = 1;
            progressBar.Value = 0;
            progressBar.Maximum = 999;

            Cursor = Cursors.WaitCursor;

            textBox.Text += "Iniciando os testes..." + Environment.NewLine;

            try
            {
                Progress();

                if (comboBox1.DataSource == null)
                    throw new Exception("Objeto COM não encontrado!");

                serialPort.PortName = comboBox1.SelectedText;
                serialPort.BaudRate = 9600;
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.Handshake = Handshake.None;
                serialPort.Encoding = Encoding.Default;

                textBox.Text += $"Conectando: COM: {serialPort.PortName} BaudRate: {serialPort.BaudRate}";

                serialPort.Open();

                Progress();
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
        }

        private void Progress()
        {
            for (int i = 0; i < 333; i++)
            {
                progressBar.Value += 1;

                Application.DoEvents();
            }
        }

        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            textBox.Text += $"{Environment.NewLine}{serialPort.ReadExisting()}";
        }
    }
}
