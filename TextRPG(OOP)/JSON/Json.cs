using System;
using System.IO;
using Newtonsoft.Json;

public class JsonFile<T> where T : class, new()
{
    private readonly string _filePath;
    public JsonFile(string filePath, bool createIfDoesntExist)
    {
        _filePath = filePath;

        if (createIfDoesntExist && !File.Exists(_filePath))
        {
            File.WriteAllText(_filePath, "");
            Write(new T());
        }
    }
    public T Read()
    {
        try
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("[JsonFile.Read] File does not exist [" + _filePath + "]");

            string jsonContent = File.ReadAllText(_filePath);
            T data = JsonConvert.DeserializeObject<T>(jsonContent);
            return data ?? new T();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[JsonFile] Error reading [{ex.Message}]" );
            return new T(); 
        }
    }

    public void Write(T data)
    {
        try
        {
            if (!File.Exists(_filePath))
                throw new FileNotFoundException("[JsonFile.Write] File does not exist [" + _filePath + "]");

            string jsonContent = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(_filePath, jsonContent);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[JsonFile.Write] Error writing [{ex.Message}]");
        }
    }
}