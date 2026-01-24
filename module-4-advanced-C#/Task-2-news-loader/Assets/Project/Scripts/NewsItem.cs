#nullable enable
using System;

public class NewsItem
{
    public NewsItem(string? content, string? title, DateTime timeTamp)
    {
        Content = content;
        TimeTamp = timeTamp;
        Title = title;
    }

    public string? Title { get; private set; }
    public string? Content { get; private set;}
    public DateTime TimeTamp {get; private set; }
    
}
