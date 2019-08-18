using DomainValidation.Interfaces.Specification;
using DomainValidation.Validation;
using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Data.Layer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static RegistrationSystem.Service.Layer.HelperClass.Validation;

namespace RegistrationSystem.Service.Layer.HelperClass
{
    public abstract class ValidationSystem
    {
        public IUser _user;
        protected bool isSetUser = false;
        public abstract void SetUser(IUser user);

        public abstract ValidationPears IsMailValid(string mail);
        public abstract Task<Tuple<IPhoneNumber,bool>> IsPhoneNumberValidAsync(string number);
        public abstract ValidationPears IsLastNameAndFirstNameValid(string lastName, string firstName);
        public abstract ValidationPears IsAgeValid(int age);
        public abstract ValidationPears IsPasswordValid(string password);

        public async Task<List<ValidationPears>> GetAllValidationForUserAsync()// es motodi gamoikeneba mxolod obiektis validaciistvis
        {
            var isNumberValid = await IsPhoneNumberValidAsync(_user.Password);


            return new List<ValidationPears>
            {
                IsMailValid(_user.Email),IsLastNameAndFirstNameValid(_user.LastName,_user.FirstName),
                IsAgeValid(DateTime.Now.Year - _user.DateOfBirth.Year),IsPasswordValid(_user.Password),
                

            };
            
        }

        


    }


}
