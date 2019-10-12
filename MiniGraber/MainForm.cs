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
using MiniGraber.Objects;
using System.Threading;

namespace MiniGraber
{
    public partial class MainForm : Form
    {
        Client client;
        public MainForm()
        {
            InitializeComponent();
        }
        // Починить
        private void btnStart_Click(object sender, EventArgs e)
        {
            ClearPanels();
            Task.Run(UpdateTable);
        }

        private void UpdateTable()
        {
            btnStart.BeginInvoke((Action)(() => { btnStart.Enabled = false; rtbResponse.Text += false; }));
            if (tbUserId.Text == "")
            {
                MessageBox.Show("Check your id or adress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnStart.BeginInvoke((Action)(() => { btnStart.Enabled = true; rtbResponse.Text += true; }));
                return;
            }
            List<Person> people = client.GetFriends(tbUserId.Text);
            Person p = client.GetPerson(tbUserId.Text);

            foreach (Person item in people)
            {
                PersonCard pc = new PersonCard();
                frPanel.BeginInvoke((Action)(() => { 
                    frPanel.Controls.Add(pc);
                    pc.SetPerson(item);
                    lbName.Text = p.FullName;
                    lbCount.Text = people.Count.ToString();
                }));
            }
            btnStart.BeginInvoke((Action)(() => { btnStart.Enabled = true; rtbResponse.Text += true; }));
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
            try
            {
                client = new Client();
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
            lbCount.Text = "";
            lbName.Text = "";
            frPanel.Controls.Clear();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearPanels();
        }
    }
}

