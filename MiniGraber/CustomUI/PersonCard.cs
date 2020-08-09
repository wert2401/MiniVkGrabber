using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MiniGraber.Utils;
using MiniGraber.Objects;
using System.Threading;

namespace MiniGraber
{
    public partial class PersonCard : UserControl
    {
        Person person;
        public PersonCard()
        {
            InitializeComponent();
        }

        public async Task SetPerson(Person person)
        {
            this.person = person;
            tbBdate.Text = $"{person.bdate}";
            tbCity.Text = $"{person.city.title}";
            tbName.Text = person.FullName;
            tbID.Text = person.id.ToString();
            roundPicture1.Click += new EventHandler(OpenPersonPage);
            await SetImage(person);
        }
        private async Task SetImage(Person person)
        {
            roundPicture1.Image = await MyHttpClient.GetImage(person.photo_200_orig);
        }

        private void OpenPersonPage(object sender, System.EventArgs e)
        {
            ExternalActions.OpenLink("https://www.vk.com/id" + person.id);
        }
    }
}
