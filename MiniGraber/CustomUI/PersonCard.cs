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

namespace MiniGraber
{
    public partial class PersonCard : UserControl
    {
        Person person;
        public PersonCard()
        {
            InitializeComponent();
        }

        public void SetPerson(Person person)
        {
            this.person = person;
            tbBdate.Text = $"{person.bdate}";
            tbCity.Text = $"{person.city.title}";
            tbName.Text = person.FullName;
            roundPicture1.Click += new EventHandler(OpenPersonPage);
            SetImage(person);
        }
        private async void SetImage(Person person)
        {
            roundPicture1.Image = await MyHttpClient.GetImage(person.photo_200_orig);
        }

        private void OpenPersonPage(object sender, System.EventArgs e)
        {
            ExternalActions.OpenLink("https://www.vk.com/id" + person.id);
        }
    }
}
