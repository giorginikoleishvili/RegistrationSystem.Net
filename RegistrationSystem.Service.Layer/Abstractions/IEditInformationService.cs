using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Service.Layer.Abstractions
{
    public interface IEditInformationService
    {
        public editUserInformation(int userId, Dictionary<string, string> editValuePears, Repository.Layer.Repository repository);
        { }
    }
}
