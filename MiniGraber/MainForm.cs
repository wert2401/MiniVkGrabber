using System;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using MiniGraber.ClientLogic;

namespace MiniGraber
{
    public partial class MainForm : Form
    {
        Client client;
        public MainForm()
        {
            InitializeComponent();
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            if (tbUserId.Text == "")
            {
                MessageBox.Show("Check your id or adress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnStart.Enabled = true;
                return;
            }
            string resp = await Task.Run(GetPerson);
            rtbResponse.Text += resp;
            btnStart.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.Disconnect();
            Application.Exit();
        }

        private void tbUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStart.PerformClick();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            client = new Client();
            try
            {
                client.Connect("127.0.0.1", 8888);
            }
            catch
            {
                MessageBox.Show("Error, can`t connect to server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void ClearPanels()
        {
            rtbResponse.Text = "";
            frPanel.Controls.Clear();
        }

        private string GetPerson()
        {
            string resp = "";
            if (client.IsConnected)
            {
                try
                {
                    resp = client.SendRequest(new CommandObject("getFriends", tbUserId.Text));
                }
                catch (Exception)
                {
                    MessageBox.Show("Error, can`t connect to server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            return resp;
        }

        private async void SetName()
        {

        }
    }
}

