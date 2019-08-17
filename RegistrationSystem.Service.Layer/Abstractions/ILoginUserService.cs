using RegistrationSystem.Data.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Service.Layer.Abstractions
{
    public interface ILoginUserService
    {
        Task<IUser> LoginUserInSystemAsync(string password, string mail, Repository.Layer.Repository repository);
    }
}
