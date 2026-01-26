using System;
using System.Collections.Generic;
using UnityEngine;

public static class NewsMapper
{
    public static List<NewsItem> ToNewsItems(this List<NewsItemDto> newsItems)
    {
        List<NewsItem> items = new();

        foreach (var newsData in newsItems)
        {
            string content = string.IsNullOrWhiteSpace(newsData.content) ? null : newsData.content;
            string title = string.IsNullOrEmpty(newsData.title) ? null : newsData.title;
            DateTime.TryParse(newsData.timestamp, out var time);

            NewsItem newsItem = new NewsItem(content, title, time);
            
            items.Add(newsItem);
        }
        return items;
    }
}
