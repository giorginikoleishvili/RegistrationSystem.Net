using MySql.Data.MySqlClient;
using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Data.Layer.Models;
using RegistrationSystem.Repository.Layer.Extentions;
using RegistrationSystem.Repository.Layer.HelperMethods.PasswordHashing;
using RegistrationSystem.Repository.Layer.SerilizeAndDeserilize;
using RegistrationSystem.Repository.Layer.SerilizeAndDeserilize.Abstraction;
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
        private readonly string _path = Environment.CurrentDirectory + "\\LogiedInformation.txt";
        private readonly ISerilizeObject<IDataChangeInformation> _serilizeObject = new SerilizeObject<IDataChangeInformation>();
        #region Singleton repository
        private static Repository _instance = null;
        private static readonly object _root = new object();


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



        private readonly string _connectionString = "Data Source = DESKTOP-APJVDMM; Initial Catalog = TestBase; User ID = TestBase; Password = giusha131313";

        public void RegistreUser(IUser user)
        {
            var isUserChecked = this.IsUserInformationValidate(user);
            CheckAndCreatTableInBase();
            var userHashedPassword = HashingSystem.Hash(user.Password);

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
                                                       $"'{user.Email}','{userHashedPassword}','{user.Mobile}','{user.DateOfBirth}'," +
                                                       $"'{user.RegistrationDate}','{user.UserAddress.Country}','{user.UserAddress.Region}'," +
                                                       $"'{user.UserAddress.City}','{user.UserAddress.Addres1}','{user.UserAddress.Address2}');";
                    Task.Run(() =>
                    {
                        var connection = new MySqlConnection(_connectionString);

                        var myCommand = new MySqlCommand(insertUserQuery, connection);
                        connection.Open();
                        var reader = myCommand.ExecuteReader();

                        connection.Close();

                    });

                }
                catch (Exception e)
                {
                    var exeption = e.Message;
                }
            }
            else
                throw new ArgumentNullException("User is null or user's some parameter is null or empty");
        }



        public void EditUserInformation(int userId, Dictionary<string, string> editValuePears)//shemodis ra unda sheicvalos
                                                                                              //maglitad paroli(key)
                                                                                              //da ritac unda sheicvalos(value)
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

                        using (MySqlConnection connection = new MySqlConnection(_connectionString))
                        {
                            var createUpdateUserInformationComand = 
                                                    new MySqlCommand(updateUserInformationQuery, connection);
                            connection.Open();
                            createUpdateUserInformationComand.ExecuteReaderAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        var exeption = ex.Message;
                    }

                }

                ///logireba
                var userIdAndName = GetNameAndPrivateId(userId);
                SaveEditiedInformationInFile(editValuePears, userIdAndName);

            }
            else
                throw new ArgumentNullException("user id or dictionary is null or empty");
        }

        public async Task<IUser> LoginUserAsync(string email, string password)//informaciis gamomtani esaa ubralod daloginebasac ase gavaketebdi
                                                                              //mibrundeba konkretuli obiekti tavisi monacemebit..
                                                                              //am obiekts shemdeg UI shi gamoikeneb
        {


            var isParametersNullOrEmpty = this.IsStringsNullOrEmpty(email, password);

            if (isParametersNullOrEmpty)
                throw new ArgumentNullException("Email or password is null or empty");

            var findUserInBaseByEmailAndPasswordQuery = "SELECT * FROM users " +
                                                      "WHERE " +
                                                            $"Password_ = '{password}' AND Email = '{email}'";
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {


                    var createfindInformationCommand = new MySqlCommand(findUserInBaseByEmailAndPasswordQuery, connection);
                    connection.Open();
                    var result = await createfindInformationCommand.ExecuteReaderAsync();

                    while (result.Read())
                    {
                        var userAddress = new Address
                        {
                            Country = result.ElementAt(12).ToString(),

                            Region = result.ElementAt(13).ToString(),
                            City = result.ElementAt(14).ToString(),
                            Addres1 = result.ElementAt(15).ToString(),
                            Address2 = result.ElementAt(16).ToString(),
                        };

                        var currentUser = new User
                        {
                            FirstName = result.ElementAt(1).ToString(),
                            LastName = result.ElementAt(2).ToString(),
                            Resident = result.ElementAt(3).ToString(),
                            PrivateID = result.ElementAt(4).ToString(),
                            RegistrarionIP = result.ElementAt(5).ToString(),
                            Language = result.ElementAt(6).ToString(),
                            Email = result.ElementAt(7).ToString(),
                            Mobile = Int64.Parse(result.ElementAt(9).ToString()),
                            DateOfBirth = Convert.ToDateTime(result.ElementAt(10).ToString()),
                            RegistrationDate = Convert.ToDateTime(result.ElementAt(11).ToString()),
                            UserAddress = userAddress

                        };
                        return currentUser;
                    }
                }
            }
            catch (Exception ex)
            {

                var exep = ex.Message;
            }

            return null;


        }




        private void SaveEditiedInformationInFile(Dictionary<string, string> editValuePears,
                                                                            Tuple<string, string> userIdAndName)
        {
            var changeInformationObject = new DataChangeInformation
            {
                editPairs = editValuePears,
                PrivateId = userIdAndName.Item2,
                UserName = userIdAndName.Item1,
                Time = DateTime.Now,
            };

            var editiedDataJson = _serilizeObject.Serilize(changeInformationObject);
            WriteInFile(editiedDataJson);
        }


        private void WriteInFile(string json)
        {
            using (var Stream = new StreamWriter(_path, false))
            {
                Stream.WriteLine(json);
                Stream.Close();
            }
        }
        private Tuple<string, string> GetNameAndPrivateId(int unicId)
        {

            var getUserPrivateIdAndNameQuery = "SELECT FirstName, PrivateId FROM users " +
                                            "WHERE " +
                                                        $"User_Id = {unicId};";

            var connection2 = new MySqlConnection(_connectionString);
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

        private void CheckAndCreatTableInBase()
        {
            var isTableCreated = this.IsCurrentTableCreatedInDataBase("users", "TestBase");
            if (!isTableCreated)
                CreateTableInBase();
        }

        private void CreateTableInBase()
        {

            var _crearteTableQuery = "CREATE TABLE users" +
            "(" +
                                "User_Id INT AUTO_INCREMENT," +
                                "FirstName VARCHAR(20)," +
                                "LastName VARCHAR(20)," +
                                "Resident VARCHAR(20)," +
                                "PrivateId VARCHAR(20)," +
                                "RegistrationIp VARCHAR(20)," +
                                "Language_ VARCHAR(20)," +
                                "Email VARCHAR(50)," +
                                "Password_ LONGTEXT," +
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
            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                var createTableCommand = new MySqlCommand(_crearteTableQuery, connection);
                connection.Open();
                createTableCommand.ExecuteReader();
            }
        }



    }
}
