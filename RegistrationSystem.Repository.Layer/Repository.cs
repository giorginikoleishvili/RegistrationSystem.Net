using MySql.Data.MySqlClient;
using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Data.Layer.Models;
using RegistrationSystem.Repository.Layer.Extentions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer
{
    public class Repository
    {
        private bool _isTableCreatedInDataBase = false;
        public EventHandler<IDataChangeInformation> onEditInformation;


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

        private readonly string _crearteTableQuery = "CREATE TABLE users" +
            "(" +
                                "User_Id INT AUTO_INCREMENT," + 
                                "FirstName VARCHAR(20)," +                                  
                                "LastName VARCHAR(20)," +
                                "Resident VARCHAR(20)," +
                                "PrivateId VARCHAR(20)," +
                                "RegistrationIp VARCHAR(20)," +
                                "Language_ VARCHAR(20)," +
                                "Email VARCHAR(50)," +
                                "Password_ VARCHAR(20)," +
                                "Mobile BIGINT," +
                                "DateOfBirth LONGTEXT," +
                                "RegistrationDate LONGTEXT," +
                                "Country VARCHAR(20)," +
                                "Region VARCHAR(20)," +
                                "City VARCHAR(15)," +
                                "Address1 VARCHAR(200)," +
                                "Address2 VARCHAR(200)," +
                                "PRIMARY KEY (User_Id)" +


            ");";

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

            if (isUserChecked)
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

                    //CreateTableInBase(connection);

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
            else
                throw new ArgumentNullException("User is null or user's some parameter is null or empty");
        }

        public void EditUserInformation(int userId, Dictionary<string,string> editValuePears)
        {
            
            if (userId >= 0 && editValuePears != null)
            {
                foreach (var item in editValuePears)
                {
                    try
                    {
                        var updateUserInformationQuery = "UPDATE users " +
                                            "SET " +
                                                    $"{item.Key} = '{item.Value} '" +
                                            "WHERE " +
                                                    $"User_Id = {userId};";

                        var connection1 = new MySqlConnection(ConnectionString);
                        connection1.Open();
                        var editUserComand = new MySqlCommand(updateUserInformationQuery, connection1);

                        editUserComand.ExecuteReader();

                        connection1.Close();
                    }
                    catch (Exception ex)
                    {
                        var exeption = ex.Message;

                    }

                }

                ///logireba
                var userIdAndName = GetNameAndPrivateId(userId);
                var changeInformation = new DataChangeInformation
                {
                    editPairs = editValuePears,
                    PrivateId = userIdAndName.Item2,
                    UserName = userIdAndName.Item1,
                    Time = DateTime.Now,
                };

                onEditInformation?.Invoke(this, changeInformation);

            }
            else
                throw new ArgumentNullException("user id or dictionary is null or empty");
            

        }

        private void WriteInFileChangeInformation(IDataChangeInformation dataChangeInformation, int userId)
        {

        }

        private Tuple<string,string> GetNameAndPrivateId(int unicId)
        {

            var getUserPrivateIdAndNameQuery = "SELECT FirstName, PrivateId FROM users " +
                                            "WHERE " +
                                                        $"User_Id = {unicId};";
            var connection2 = new MySqlConnection(ConnectionString);
            connection2.Open();

            var getCurrentUserIdAndNameCommand = new MySqlCommand(getUserPrivateIdAndNameQuery, connection2);


            var readerForNameAndId = getCurrentUserIdAndNameCommand.ExecuteReader();

            string name = null;
            string privateId = null;

            while (readerForNameAndId.Read())
            {
                name = readerForNameAndId[0].ToString();
                privateId = readerForNameAndId[1].ToString();
            }

            
            connection2.Close();

            return new Tuple<string, string>(name, privateId);
        }

        private MySqlDataReader ExecuteQueryInBase(string someQuery,MySqlConnection connection)
        {
            if (string.IsNullOrEmpty(someQuery))
                return null;


            
            var myCommand = new MySqlCommand(someQuery, connection);

            var reader = myCommand.ExecuteReader();
            return reader;
        }

        private void CreateTableInBase(MySqlConnection connection)
        {
            if (!_isTableCreatedInDataBase)
            {
                var createTableCommand = new MySqlCommand(_crearteTableQuery, connection);
                connection.Open();
                createTableCommand.ExecuteReader();
                connection.Close();

                _isTableCreatedInDataBase = true;
            }
        }

       
    }
}
