using RegistrationSystem.Repository.Layer;
using RegistrationSystem.Service.Layer.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Service.Layer.Implementation
{
    public class EditUserInformatinService : IEditUserInformationService
    {
        public void EditInformationService(int userId, Dictionary<string, string> editValuePears, Repository.Layer.Repository repository)
        {
            if (userId <= 0 && editValuePears == null && repository == null)
                throw new ArgumentNullException("User id or editValue pears and repository is null or empty");

            repository.EditUserInformation(userId, editValuePears);
        }
    }
}
