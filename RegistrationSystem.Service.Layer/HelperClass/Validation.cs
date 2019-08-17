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
    public class Validation : ValidationSystem
    {
        public Validation(IUser user)
        {
            this._user = user;
        }

        public override bool IsAgeValid()
        {
            var userAge = DateTime.Now.Year - _user.DateOfBirth.Year;

            return userAge >= 16;
        }

        public override bool IsLastNameAndFirstNameValid()
        {
            var userName = this._user.FirstName;
            var userLastName = this._user.LastName;

            return
                IsStringContainOnlyLater(userName) && IsStringContainOnlyLater(userLastName) && 
                userName.Count() >= 2 && userLastName.Count() >= 2; 

        }

        public override bool IsMailValid()
        {
            if (string.IsNullOrWhiteSpace(_user.Email))
                return false;
            try
            {
                _user.Email = Regex.Replace(_user.Email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
                string DomainMapper(Match match)
                {
                    var idn = new      IdnMapping();

                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(_user.Email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public override bool IsPasswordValid()
        {
            if (!string.IsNullOrEmpty(_user.Password))
                return false;

            var regex = new Regex(@"(?=.*\d)(?=.*[a-z])(?=.*[A-Z])");

            return regex.IsMatch(_user.Password);

        }

        public override async Task<Tuple<IPhoneNumber,bool>> IsPhoneNumberValidAsync()
        {
            var getInformationAboutNumber = await Repository.Layer.Repository.
                                            GetRepositoryInstance.
                                            GetCurrentNumberInformationAsync(_user.Mobile.ToString());


            return new Tuple<IPhoneNumber, bool>(getInformationAboutNumber, getInformationAboutNumber.Valid);
        }
        private bool IsStringContainOnlyLater(string word)
        {
            return word.All(Char.IsLetter);
        }
    }
}
