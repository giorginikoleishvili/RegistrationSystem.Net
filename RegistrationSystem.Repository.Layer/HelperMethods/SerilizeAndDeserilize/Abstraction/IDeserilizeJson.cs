using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer.SerilizeAndDeserilize.Abstraction
{
    public interface IDeserializeJson<TResult>
    {
        TResult Deserialize(string jsonInFile);
    }
}
