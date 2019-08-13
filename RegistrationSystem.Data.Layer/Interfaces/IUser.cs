using RegistrationSystem.Data.Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Data.Layer.Interfaces
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateOfBirth { get; set; }
        string Resident { get; set; }
        string PrivateID { get; set; }
        
        DateTime RegistrationDate { get; set; }
        string RegistrarionIP { get; set; }

        string Language { get; set; }

        string Email { get; set; }

        long Mobile { get; set; }
        string Password { get; set; }
        
        IAddress UserAddress { get; set; }

        
    }
}
