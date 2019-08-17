using RegistrationSystem.Data.Layer.Interfaces;
using RegistrationSystem.Service.Layer.HelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Service.Layer.Abstractions
{
    public interface IRegistreUserService
    {
        Task RegistrationUserAsync(Repository.Layer.Repository repository, IUser user, ValidationSystem validationSystem);
        
    }
}
