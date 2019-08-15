using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Data.Layer.Interfaces
{
    public interface IDataChangeInformation
    {
        DateTime Time { get; set; } // Dro roca daeditda ragac
        string UserName { get; set; }
        string PrivateId { get; set; }
        Dictionary<string, string> editPairs { get; set; }
    }
}
