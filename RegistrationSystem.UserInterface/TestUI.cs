using RegistrationSystem.Data.Layer.Models;
using RegistrationSystem.Repository.Layer;
using RegistrationSystem.Repository.Layer.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistrationSystem.UserInterface
{
    public partial class TestUI : Form
    {
        public TestUI()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var user1 = new User
            {
                DateOfBirth = new DateTime(1999,4,24),
                Email = "giorginikoleishili115@gmail.com",
                FirstName = "giorgi",
                Language = "kartuli",
                LastName = "nikoleishvili",
                Mobile = 577607123,
                Password = "mevar",
                PrivateID = "01001095795",
                RegistrarionIP = "1231.34235.231423",
                RegistrationDate = DateTime.Now,
                Resident = "Sakartvelo",
                UserAddress = new Address
                {
                    Addres1 = "gldani",
                    Address2 = "zestafoni",
                    City = "tbilisi",
                    Country = "gori",
                    Region = "cxinvali",
                }



            };


            //var a = Repository.Layer.Repository.GetRepositoryInstance.IsUserInformationValidate(user1);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("Country", "chorvila");
            dict.Add("City", "mogvali");

            //Repository.Layer.Repository.GetRepositoryInstance.AddUserInDataBase(user1);

            Repository.Layer.Repository.GetRepositoryInstance.EditUserInformation(1, dict);

        }
    }
}
