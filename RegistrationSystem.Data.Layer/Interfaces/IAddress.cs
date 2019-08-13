using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Data.Layer.Enums
{
    public interface IAddress
    {
        string Country { get; set; }
        string Region { get; set; }
        string City { get; set; }
        string Addres1 { get; set; }
        string Address2 { get; set; }
    }
}
