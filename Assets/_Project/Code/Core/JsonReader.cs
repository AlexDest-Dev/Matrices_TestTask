using System;
using System.IO;
using Newtonsoft.Json;

namespace _Project.Code.Core
{
    public class JsonReader
    {
        public T Read<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new Exception($"Can't load config {path}");
            }

            string jsonText = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(jsonText);
        }
    }
}