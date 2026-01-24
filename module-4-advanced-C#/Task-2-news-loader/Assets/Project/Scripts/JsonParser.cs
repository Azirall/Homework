using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonParser
{
        public static List<NewsItemDto> DeserializeNewsItems(this string json)
        {
                List<NewsItemDto> newsItems = new();
                
                string wrappedJson = "{\"items\":" + json + "}";
                
                NewsWrapperDto wrapper = JsonUtility.FromJson<NewsWrapperDto>(wrappedJson);
                
                
                if (wrapper == null || wrapper.items == null)
                        throw new InvalidDataException("Некорректный JSON");
                
                newsItems.AddRange(wrapper.items);
                
                return newsItems;
                
        }
}       