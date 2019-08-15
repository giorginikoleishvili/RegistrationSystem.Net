using RegistrationSystem.Data.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Data.Layer.Models
{
    public class DataChangeInformation : IDataChangeInformation
    {
        public DateTime Time {get; set; }
        public string UserName {get; set; }
        
        public string PrivateId { get; set; }
        public Dictionary<string, string> editPairs { get ; set ; }
    }
}
