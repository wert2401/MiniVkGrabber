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

namespace MiniGraber
{
    public partial class MainForm : Form
    {
        VkLogic vk;
        int id;
        public MainForm()
        {
            InitializeComponent();
            vk = new VkLogic(FileManager.GetToken());
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            ClearPanels();
            if (!int.TryParse(tbUserId.Text, out id))//Проверка введен ли id или адрес
            {
                int.TryParse(await vk.GetPersonId(tbUserId.Text), out id);
            }
            if (id == 0)
            {
                MessageBox.Show("Person is not found. Check yout adress.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                return;
            }

            SetName();

            string response = await vk.GetPersonFriends(id.ToString());
            if (response == "Error")
            {
                MessageBox.Show("Person is not found. Check yout adress.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnStart.Enabled = true;
                return;
            }
            List<Person> people = JSONProcessor.ParsePeople(response);
            lbCount.Text = people.Count.ToString();
            foreach (Person item in people)
            {
                rtbResponse.Text += item.ToString();
                PersonCard pc = new PersonCard();
                pc.SetPerson(item);
                frPanel.Controls.Add(pc);
            }
            btnStart.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tbUserId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnStart.PerformClick();
            }
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            bool online = await MyHttpClient.CheckConnection();
            if (!online)
            {
                btnStart.Enabled = false;
                btnStart.Text = "No connection!";
            }
        }

        private void ClearPanels()
        {
            rtbResponse.Text = "";
            frPanel.Controls.Clear();
        }

        private async void SetName()
        {
            string p = await vk.GetPerson(id.ToString());
            Person name = JSONProcessor.ParsePerson(p);
            lbName.Text = name.FullName;
        }
    }
}

