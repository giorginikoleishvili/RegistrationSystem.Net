using MySql.Data.MySqlClient;
using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Data.Layer.Models;
using RegistrationSystem.Repository.Layer.NetworkLayer.Abstraction;
using RegistrationSystem.Repository.Layer.SerilizeAndDeserilize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer.Extentions
{
    public static class MyExtentions
    {
        private static readonly string ConnectionString = "Data Source = DESKTOP-APJVDMM; Initial Catalog = TestBase; User ID = TestBase; Password = giusha131313";
        
        public static bool IsUserInformationValidate(this Repository repository, IUser user)
        {
            if (user == null)
                return false;
      
            return !(user.DateOfBirth == default(DateTime) && user.RegistrationDate == default(DateTime) &&
                    string.IsNullOrEmpty(user.Email) && string.IsNullOrEmpty(user.FirstName) &&
                    string.IsNullOrEmpty(user.Language) && string.IsNullOrEmpty(user.LastName) &&
                    string.IsNullOrEmpty(user.Password) && string.IsNullOrEmpty(user.PrivateID) &&
                    string.IsNullOrEmpty(user.RegistrarionIP) && string.IsNullOrEmpty(user.Resident) &&
                    string.IsNullOrEmpty(user.UserAddress.Addres1) && string.IsNullOrEmpty(user.UserAddress.Address2) &&
                    string.IsNullOrEmpty(user.UserAddress.City) && string.IsNullOrEmpty(user.UserAddress.Country) &&
                    string.IsNullOrEmpty(user.UserAddress.Region));
        }

        public static bool IsStringsNullOrEmpty(this Repository repository, params string[] wordList)
        {
            foreach (var word in wordList)
            {
                if (string.IsNullOrEmpty(word))
                    return true;
            }

            return false;
        }
        public static Object ElementAt(this System.Data.Common.DbDataReader reader, int index)
        {
            if (reader != null && index >= 0)
                return reader[index];
            else
                throw new ArgumentNullException("Reader on index is null < zero");
        }

        public static async Task<IPhoneNumber> GetCurrentNumberInformationAsync(this Repository repository, string mobileNumber)
        {
            if (repository == null && string.IsNullOrEmpty(mobileNumber))
                throw new ArgumentNullException("Repository or mobile number instance is null or empty");

            var url = @"http://apilayer.net/api/validate?access_key=34f04f59c137a3366a1569691e40dd22&number=+"+$"{mobileNumber}";
            var deserializeJson = new DeserilizeJson<PhoneNumber>();
            var request = new HttpRequest();

            var numberJson = await request.GetRequestAsync(url);

            var currentNumberInformation = deserializeJson.Deserialize(numberJson);

            return currentNumberInformation;
        }

        public static bool IsCurrentTableCreatedInDataBase(this Repository repository, string tableName,
                                                                                        string dataBaseName)
        {
            string cmdStr = $"SELECT COUNT(*) FROM information_schema.tables WHERE " +
                $"table_schema = '{dataBaseName}' " +
                        $"AND " +
                $"table_name = '{tableName}'";


            using (MySqlConnection conn = new MySqlConnection(ConnectionString))

            {
                try
                {
                    MySqlCommand cmd = new MySqlCommand(cmdStr, conn);

                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {

                        int count = reader.GetInt32(0);

                        return count == 1;
                    }
                }
                catch (Exception ex)
                {
                    var exep = ex.Message;
                }
            }

            return false;
        }


    }
}
