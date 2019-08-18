using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Data.Layer.Models;
using RegistrationSystem.Repository.Layer.Extentions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RegistrationSystem.Service.Layer.HelperClass
{
    public class Validation : ValidationSystem// am klasis gamokenebit Ui shi chautardeba validacia kvelapers
    {
        #region Singleton Validation
        private static Validation _instance = null;
        private static readonly object _root = new object();

        Validation() { }
        public static Validation GetRepositoryInstance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_root)
                    {
                        if (_instance == null)
                            _instance = new Validation();
                    }

                }

                return _instance;

            }
        }
        #endregion
        public struct ValidationPears
        {
            
            public string validationMessage;
            public bool IsValidate;
            public ValidationPears(string text, bool valid)
            {
                this.IsValidate = valid;
                this.validationMessage = $"{text} + {valid.ToString()}";

            }
            
        }

        public override void SetUser(IUser user)
        {
            this._user = user;
            isSetUser = true;
        }
        public override ValidationPears IsAgeValid(int age)
        {
            var userAge =age;


            return new ValidationPears("User age is", userAge >= 16);
        }

        public override ValidationPears IsLastNameAndFirstNameValid(string lastName,string firstName)
        {
            var userName = firstName;
            var userLastName = lastName;


            var isValid = 
                IsStringContainOnlyLater(userName) && IsStringContainOnlyLater(userLastName) &&
                userName.Count() >= 2 && userLastName.Count() >= 2;

            return new ValidationPears("LastName and Firstname validation is", isValid);
        }

        public override ValidationPears IsMailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Mail is null or empty");
            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return new ValidationPears("Regex TimeOut",false);
            }
            catch (ArgumentException e)
            {
                return new ValidationPears("",false);
            }

            try
            {
                var isValid = Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));

                return new ValidationPears("Mail valid is ", isValid);
            }
            catch (RegexMatchTimeoutException)
            {
                return new ValidationPears("Regex TimeOut", false);
            }
        }

        public override ValidationPears IsPasswordValid(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new Exception("UserPasswordIs null or empty");

            var regex = new Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])");

            var isValid =  regex.IsMatch(password);
            
            return new ValidationPears("Password valid is ",isValid);

        }

        public override async Task<Tuple<IPhoneNumber, bool>> IsPhoneNumberValidAsync(string mobile)
        {

            var getInformationAboutNumber = await Repository.Layer.Repository.
                                            GetRepositoryInstance.
                                            GetCurrentNumberInformationAsync(mobile);
            if (getInformationAboutNumber == null)
                throw new Exception("Current number not found");

            return new Tuple<IPhoneNumber, bool>(getInformationAboutNumber, getInformationAboutNumber.Valid);
        }
        private bool IsStringContainOnlyLater(string word)
        {
            return word.All(Char.IsLetter);
        }

        
    }
}
