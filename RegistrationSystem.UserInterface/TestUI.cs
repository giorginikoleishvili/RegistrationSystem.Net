using RegistrationSystem.Data.Layer.Models;
using RegistrationSystem.Repository.Layer;
using RegistrationSystem.Repository.Layer.Extentions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


        }


        private async void Button1_Click(object sender, EventArgs e)
        {
            var user1 = new User
            {
                DateOfBirth = new DateTime(1999, 4, 24),
                Email = "giorginikoleishili115@gmail.com",
                FirstName = "SDFSDFSDF",
                Language = "lazuri",
                LastName = "DFSSFSDFSDFSDFSFD",
                Mobile = 577607123,
                Password = "mevar",
                PrivateID = "01001095795",
                RegistrarionIP = "1231.34235.231423",
                RegistrationDate = DateTime.Now,
                Resident = "borjomi",
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
            

            //Repository.Layer.Repository.GetRepositoryInstance.RegistreUser(user1);
            var a = await Repository.Layer.Repository.GetRepositoryInstance.LoginUserAsync("giorginikoleishili115@gmail.com", "$MYHASH$V1$10000$FAallpo/M2dqA+G2sFvd4zLcxAuwDKHuPbv93Qg15xFRi3aH");

            dict.Add("City", "mogvali");


            //Repository.Layer.Repository.GetRepositoryInstance.EditUserInformation(1, dict);

        }
    }
}
