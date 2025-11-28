using System;

namespace TaskJournal;

public class LocalizationService
{
    public string ToRussian(Enum value)
    {
        return value switch
        {
            Status status => ToRussian(status),
            Priority priority => ToRussian(priority),
            Category category => ToRussian(category),
        };
    }
    private string ToRussian(Category category)
    {
        return category switch
        {
            Category.Study => "Учебные задачи",
            Category.Work => "Рабочие задачи",
            Category.Home => "Домашние задачи",
            Category.Other => "Прочие задачи",
        };
    }

    private string ToRussian(Priority priority)
    {
        return priority switch
        {
            Priority.Low => "Низкий приоритет",
            Priority.Medium => "Средний приоритет",
            Priority.High => "Высокий приоритет",
        };
    }

    private string ToRussian(Status status)
    {
        return status switch
        {
            Status.New => "Новые задачи",
            Status.InProgress => "Задачи в работе",
            Status.Done => "Выполненные задачи",
        };
    }
}