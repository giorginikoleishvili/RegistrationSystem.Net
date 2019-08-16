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
    public static class Cipher
    {
        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public static string base64Decode2(string sData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(sData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

    }
        
    public partial class TestUI : Form
    {
        public TestUI()
        {
            InitializeComponent();
        }


        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            var str = "String";
            var password = "pali";

            var enc = Cipher.base64Encode("giorgi");
            var dec = Cipher.base64Decode2(enc);

            var enc1 = Cipher.base64Encode("giorgi");
            var dec2 = Cipher.base64Decode2(enc);
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

            Repository.Layer.Repository.GetRepositoryInstance.RegistreUser(user1);

            //Repository.Layer.Repository.GetRepositoryInstance.EditUserInformation(1, dict);

        }
    }
}
