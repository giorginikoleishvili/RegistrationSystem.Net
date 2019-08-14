using RegistrationSystem.Data.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer.Extentions
{
    public static class MyExtentions
    {
        public static bool IsUserInformationValidate(this Repository repository, IUser user)
        {
            if (user == null)
                return false;
            var a = string.IsNullOrEmpty(user.Email);
            return !(user.DateOfBirth == default(DateTime) || user.RegistrationDate == default(DateTime) ||
                    string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.FirstName) ||
                    string.IsNullOrEmpty(user.Language) || string.IsNullOrEmpty(user.LastName) ||
                    string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.PrivateID) ||
                    string.IsNullOrEmpty(user.RegistrarionIP) || string.IsNullOrEmpty(user.Resident) ||
                    string.IsNullOrEmpty(user.UserAddress.Addres1) || string.IsNullOrEmpty(user.UserAddress.Address2) ||
                    string.IsNullOrEmpty(user.UserAddress.City) || string.IsNullOrEmpty(user.UserAddress.Country) ||
                    string.IsNullOrEmpty(user.UserAddress.Region));
        }
    }
}
