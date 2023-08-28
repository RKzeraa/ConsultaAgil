using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Clinica.Helper;

internal class JsonFileHelper
{
    public static bool SerializeAndSave<T>(List<T> list, string path)
    {
        try
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(path, json);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    public static List<T> DeserializeFromFile<T>(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new List<T>();
        }
    }
}