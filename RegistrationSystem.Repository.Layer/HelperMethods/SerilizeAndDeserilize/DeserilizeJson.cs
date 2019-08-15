using Newtonsoft.Json;
using RegistrationSystem.Repository.Layer.SerilizeAndDeserilize.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer.SerilizeAndDeserilize
{
    public class DeserilizeJson<TResult> : IDeserializeJson<TResult>
    {
        public TResult Deserialize(string jsonInFile)
        {
            return JsonConvert.DeserializeObject<TResult>(jsonInFile);
        }
    }
}
