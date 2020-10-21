using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

public interface IJsonLoader
{
    void LoadJson(string path, Action<string> callback);
}

public class JsonLoaderAsync : IJsonLoader
{
    public void LoadJson(string path, Action<string> callback)
    {
        LoadFileAsync(path, callback);
    }

    private async Task LoadFileAsync(string path, Action<string> OnLoaded)
    {
        using (FileStream SourceStream = File.Open(path, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(SourceStream, Encoding.UTF8))
            {
                string result = await reader.ReadToEndAsync();
                OnLoaded(result);
            }
        }
    }
}


public class JsonLoader : IJsonLoader
{
    public void LoadJson(string path, Action<string> callback)
    {
        string json = string.Empty;
        if (File.Exists(path))
        {
            json = File.ReadAllText(path);
        }
        callback(json);
    }
}