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

namespace MiniGraber
{
    public partial class MainForm : Form
    {
        Requests req;
        public MainForm()
        {
            InitializeComponent();
        }
        // Починить
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            Task.Run(GetFriends;
            Task.Run(GetPerson);
            btnStart.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            req.Disconnect();
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
                req = new Requests("127.0.0.1", 8888);
            }
            catch
            {
                MessageBox.Show("Error, can`t connect to server", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void GetPerson()
        {
            if (tbUserId.Text == "")
            {
                MessageBox.Show("Check your id or adress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnStart.Enabled = true;
                return;
            }
            Person p = req.GetPerson(tbUserId.Text);
            lbName.Text = p.FullName;
        }

        private void GetFriends()
        {
            if (tbUserId.Text == "")
            {
                MessageBox.Show("Check your id or adress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnStart.Enabled = true;
                return;
            }
            List<Person> people = req.GetFriends(tbUserId.Text);
            lbCount.Text = people.Count.ToString();
            foreach (Person item in people)
            {
                PersonCard pc = new PersonCard();
                frPanel.Controls.Add(pc);
                pc.SetPerson(item);
            }
        }

        private void ClearPanels()
        {
            rtbResponse.Text = "";
            frPanel.Controls.Clear();
        }
        private async void SetName()
        {

        }
    }
}

