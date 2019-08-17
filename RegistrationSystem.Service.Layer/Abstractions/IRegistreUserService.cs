using RegistrationSystem.Data.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Service.Layer.Abstractions
{
    public interface IRegistreUserService
    {
        void RegistrationUser(Repository.Layer.Repository repository, IUser user);
        
    }
}
