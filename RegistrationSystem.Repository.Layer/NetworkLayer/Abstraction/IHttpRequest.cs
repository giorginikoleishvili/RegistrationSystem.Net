using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer.NetworkLayer.Abstraction
{
    public interface IHttpRequest
    {
        Task<string> GetRequestAsync(string uri);
    }
}
