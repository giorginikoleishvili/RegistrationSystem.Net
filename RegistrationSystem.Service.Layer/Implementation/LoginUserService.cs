using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Repository.Layer;
using RegistrationSystem.Repository.Layer.Extentions;
using RegistrationSystem.Service.Layer.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Service.Layer.Implementation
{
    public class LoginUserService : ILoginUserService
    {
        public async Task<IUser> LoginUserInSystemAsync(string password, string mail, Repository.Layer.Repository repository)
        {
            var isParamsNulOrEmpty = repository.IsStringsNullOrEmpty(password, mail);
            if (!isParamsNulOrEmpty)
            {
                try
                {
                    var currentUser = await repository.LoginUserAsync(mail, password);

                    return currentUser;
                }
                catch (Exception ex)
                {

                    var mess = ex.Message;
                }

            }

            return null;
        }

        
    }
}

