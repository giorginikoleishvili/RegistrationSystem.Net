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

namespace RegistrationSystem.Service.Layer.HelperClass
{
    public abstract class ValidationSystem
    {
        protected IUser _user;
        public abstract bool IsMailValid();
        public abstract Task<Tuple<IPhoneNumber,bool>> IsPhoneNumberValidAsync();
        public abstract bool IsLastNameAndFirstNameValid();
        public abstract bool IsAgeValid();
        public abstract bool IsPasswordValid();

        public async Task<bool> RunAllValidationForUser()
        {
            var isNumberValid = await IsPhoneNumberValidAsync();


            return
            IsMailValid() && IsMailValid() &&
            IsAgeValid() && IsPasswordValid() &&
            IsLastNameAndFirstNameValid() &&
            isNumberValid.Item2;
            
          

        }


    }


}
