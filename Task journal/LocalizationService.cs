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
    private string ToRussian(Status status)
    {
        return status switch
        {
            Status.Study => "Учебные задачи",
            Status.Work => "Рабочие задачи",
            Status.Home => "Домашние задачи",
            Status.Other => "Прочие задачи",
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

    private string ToRussian(Category category)
    {
        return category switch
        {
            Category.New => "Новые задачи",
            Category.InProgress => "Задачи в работе",
            Category.Done => "Выполненные задачи",
        };
    }
}