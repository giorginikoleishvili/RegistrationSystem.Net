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
            validationSystem.SetUser(user);
            try
            {
                var userValidParametersDeep = await validationSystem.GetAllValidationForUserAsync();
                var succsessValidation = userValidParametersDeep
                                        .Count(o => o.IsValidate == true);
                
                if (userPartsNulOrEmpty && succsessValidation == 4)
                    repository.RegistreUser(user);

                validationSystem._user = null;
                    
            }
            catch (Exception ex)
            {
                var exeption = ex.Message;
            }

               
        }
    }
}
