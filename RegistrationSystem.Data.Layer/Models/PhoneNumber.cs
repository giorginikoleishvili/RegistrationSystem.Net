using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Data.Layer.Models
{
    public class PhoneNumber : IPhoneNumber
    {
        public bool Valid { get; set; }
        public string Number { get; set; }
        public string LocalFormat { get; set; }
        public string InternationalFormat { get; set; }
        public string CountryPrefix { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string Location { get; set; }
        public string Carrier { get; set; }
        public string LineType { get; set; }
    }
}
