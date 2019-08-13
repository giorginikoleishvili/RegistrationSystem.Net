using RegistrationSystem.Data.Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Data.Layer.Models
{
    public class Address : IAddress
    {
        public string Country {get; set; }
        public string Region {get; set; }
        public string City {get; set; }
        public string Addres1 {get; set; }
        public string Address2 {get; set; }
    }
}
