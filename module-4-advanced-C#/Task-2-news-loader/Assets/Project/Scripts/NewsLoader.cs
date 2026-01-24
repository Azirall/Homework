using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class NewsLoader
{
    private readonly string _fileName = "news.json";
    
    public async Task<List<NewsItem>> LoadNewsAsync()
    {
        List<NewsItem> newsList = new();
        try
        {
            var path = Path.Combine(Application.streamingAssetsPath, _fileName);
            
            var json = await File.ReadAllTextAsync(path);
            
            newsList = json.DeserializeNewsItems().ToNewsItems();
            
        }
        catch (FileNotFoundException e)
        {
           Debug.LogWarning("Файл не найден");
        }
        catch (ArgumentException e)
        {
            throw;
        }
        return newsList;
    }
}
