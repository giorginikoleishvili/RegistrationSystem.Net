using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Repository.Layer;
using RegistrationSystem.Repository.Layer.Extentions;
using RegistrationSystem.Service.Layer.HelperClass;

namespace RegistrationSystem.Service.Layer.Abstractions
{
    public class RegistreUserService : IRegistreUserService
    {
        public async Task RegistrationUserAsync(Repository.Layer.Repository repository, IUser user, ValidationSystem validationSystem)
        {
            var userPartsNulOrEmpty = repository.IsUserInformationValidate(user);
            try
            {
                var userValidParametersDeep = await validationSystem.RunAllValidationForUser();
                if (userPartsNulOrEmpty && userValidParametersDeep)
                {
                    repository.RegistreUser(user);
                    
                }

            }
            catch (Exception ex)
            {
                var exeption = ex.Message;
            }

               
        }
    }
}
