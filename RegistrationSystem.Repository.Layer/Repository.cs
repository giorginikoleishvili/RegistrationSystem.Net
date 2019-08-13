using MySql.Data.MySqlClient;
using RegistrationSystem.Data.Layer.Interfaces;
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
                var someValue = reader[2];

                
            }

            return null;

        }



    }
}
