using System;
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
        private async void btnStart_Click(object sender, EventArgs e)
        {
            ClearPanels();
            btnStart.Enabled = false;
            if (tbUserId.Text == "")
            {
                MessageBox.Show("Check your id or adress", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnStart.Enabled = true;
                return;
            }

            await SetName();
            await RunGetFriendsAsyncAndParallel();
            btnStart.Enabled = true;
        }

        private async Task RunGetFriendsAsyncAndParallel()
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            List<Person> people = await client.GetFriendsAsync(tbUserId.Text);
            if (people ==  null)
            {
                MessageBox.Show("Error, this account is closed for unauth users or for not friends", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Task> personTasks = new List<Task>();
            lbCount.Text = people.Count.ToString();

            //Способ с низкой загрузкой ЦП, но долгим выводом
            //while (people.Count > 0)
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        if (people.Count <= 0)
            //        {
            //            break;
            //        }
            //        personTasks.Add(AddPersonCard(people[0]));
            //        people.RemoveAt(0);
            //    }

            //    await Task.WhenAll(personTasks);
            //    personTasks.Clear();
            //}

            //Способ с быстрыи выводом, но с большой нагрузкой на ЦП
            foreach (var item in people)
            {
                AddPersonCard(item);
            }
            sw.Stop();
            lbTime.Text = sw.ElapsedMilliseconds.ToString();
        }

        private async void AddPersonCard(Person person)
        {
            PersonCard pc = new PersonCard();
            await pc.SetPerson(person);
            frPanel.Controls.Add(pc);
        }

        private async Task SetName()
        {
            Person p = await client.GetPersonAsync(tbUserId.Text);
            lbName.Text = p.FullName;
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

