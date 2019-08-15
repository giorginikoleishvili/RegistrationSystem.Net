using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistrationSystem.Repository.Layer
{
    public class InformationManager
    {
        private readonly string _path = Environment.CurrentDirectory + "\\LogiedInformation.txt";

        private void WriteInFile(string json)
        {
            using (var Stream = new StreamWriter(_path, false))
            {
                Stream.WriteLine(json);
                Stream.Close();
            }
        }

        private string ReadFromFile()
        {
            using (StreamReader Stream = new StreamReader(_path, false))
            {
                string ReadText = Stream.ReadToEnd();
                return ReadText;
            }
        }

        
    }
}
