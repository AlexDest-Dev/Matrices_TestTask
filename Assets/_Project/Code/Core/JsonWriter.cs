using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace _Project.Code.Core
{
    public class JsonWriter
    {
        public void Write<T>(T obj, string filename = "result.json")
        {
            var path = Path.Combine(Application.persistentDataPath, filename);
            var jsonString = JsonConvert.SerializeObject(obj);
            File.WriteAllText(path, jsonString);
            Logger.Log($"File written: {path}");
        }
    }
}