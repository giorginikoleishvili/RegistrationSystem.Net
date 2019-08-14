using MySql.Data.MySqlClient;
using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Repository.Layer.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer
{
    public class Repository
    {
        #region Singleton repository
        private static Repository _instance = null;
        private static readonly object _root = new object();

        Repository() { }
        public static Repository GetRepositoryInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_root)
                    {
                        if (_instance == null)
                            _instance = new Repository();
                    }

                }

                return _instance;

            }
        }
        #endregion

        private readonly string ConnectionString = "Data Source = DESKTOP-APJVDMM; Initial Catalog = TestBase; User ID = TestBase; Password = giusha131313";

        public IEnumerable<IUser> GetAllUser()
        {
            var getAllInformationQuery = "SELECT * FROM mystudet";
            var connection = new MySqlConnection(ConnectionString);
            var myCommand = new MySqlCommand(getAllInformationQuery, connection);

            connection.Open();
            var reader = myCommand.ExecuteReader();

            while (reader.Read())
            {
                var someValue = reader[1] as IUser;


            }

            return null;

        }

        public void AddUserInDataBase(IUser user)
        {

            var isUserChecked = this.IsUserInformationValidate(user);

            if(isUserChecked)
            {
                try
                {
                    var insertUserQuery = "INSERT INTO users(FirstName, LastName, Resident, " +
                                                               "PrivateId, RegistrationIp, Language_, " +
                                                               "Email, Password_, Mobile, DateOfBirth," +
                                                               "RegistrationDate, Country, Region," +
                                                               "City, Address1, Address2)" +
                                          "VALUES" +
                                                       $"('{user.FirstName}','{user.LastName}','{user.Resident}'," +
                                                       $"'{user.PrivateID}','{user.RegistrarionIP}','{user.Language}'," +
                                                       $"'{user.Email}','{user.Password}','{user.Mobile}','{user.DateOfBirth}'," +
                                                       $"'{user.RegistrationDate}','{user.UserAddress.Country}','{user.UserAddress.Region}'," +
                                                       $"'{user.UserAddress.City}','{user.UserAddress.Addres1}','{user.UserAddress.Address2}');";

                    var connection = new MySqlConnection(ConnectionString);
                    var myCommand = new MySqlCommand(insertUserQuery, connection);

                    connection.Open();
                    var reader = myCommand.ExecuteReader();

                    connection.Close();
                }
                catch (Exception e)
                {
                    var exeption = e.Message;
                    
                }
                
            }
            

        }

    }
}
