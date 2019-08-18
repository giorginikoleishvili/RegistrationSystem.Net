using RegistrationSystem.Data.Layer.Models;
using RegistrationSystem.Repository.Layer;
using RegistrationSystem.Repository.Layer.Extentions;
using RegistrationSystem.Service.Layer.Abstractions;
using RegistrationSystem.Service.Layer.HelperClass;
using RegistrationSystem.Service.Layer.Implementation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
                Email = "giorginikoleishvili115@gmail.com",
                FirstName = "luka",
                Language = "kartuli",
                LastName = "nikoleishvili",
                Mobile = "+995577607123",
                Password = "Gop143",
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

            var repo = Repository.Layer.Repository.GetRepositoryInstance;
            var val = Validation.GetRepositoryInstance;

            var regi = new RegistreUserService();
            await regi.RegistrationUserAsync(repo,user1,val);

            var ageVl = val.IsAgeValid(53);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("FirstName", "jora");
            dict.Add("Mobile", "+995593724547");

            EditUserInformatinService editUserInformatinService = new EditUserInformatinService();
            editUserInformatinService.EditInformationService(1,dict,repo);


            LoginUserService loginUserService = new LoginUserService();
            var logined = await loginUserService.LoginUserInSystemAsync("Gop143", "giorginikoleishvili115@gmail.com",repo);

            //Repository.Layer.Repository.GetRepositoryInstance.EditUserInformation(1, dict);

        }
    }
}
