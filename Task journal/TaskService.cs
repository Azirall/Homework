namespace TaskJournal;

public class TaskService
{
    private List<TaskItem> _taskList = new();

    public TaskService()
    {
        GenerateSampleTasks();
    }
    
    public void AddTask(TaskItem task)
    {
        _taskList.Add(task);
    }

    public IReadOnlyList<TaskItem> GetAllTasks()
    {
        return _taskList;
    }

    public void DeleteTask(int index)
    {
        _taskList.RemoveAt(index-1);
    }

    public void ChangeTaskStatusToComplete(int taskId)
    {
        _taskList[taskId - 1].ChangeTaskStatus(Status.Done);
    }

    public List<TaskItem> GetTaskListByType<TEnum>() where TEnum : Enum
    {
        return typeof(TEnum) switch
        {
            Type t when t == typeof(Category) => _taskList.OrderBy(task => task.Category).ToList(),
            Type t when t == typeof(Status) => _taskList.OrderByDescending(task => task.Status).ToList(),
            Type t when t == typeof(Priority) => _taskList.OrderBy(task => task.Priority).ToList(),
            _ => new List<TaskItem>()
        };
    }
    private void GenerateSampleTasks()
    {
        _taskList.Add(new TaskItem("Изучить C# основы", "Изучить базовые концепции языка C#", Priority.High, Category.Study, Status.New));
        _taskList.Add(new TaskItem("Завершить проект", null, Priority.High, Category.Work, Status.InProgress));
        _taskList.Add(new TaskItem("Купить продукты", "Молоко, хлеб, яйца", Priority.Medium, Category.Home, Status.New));
        _taskList.Add(new TaskItem("Помыть посуду", null, Priority.Low, Category.Home, Status.New));
        _taskList.Add(new TaskItem("Подготовить презентацию", "Создать слайды для завтрашнего выступления", Priority.High, Category.Work, Status.InProgress));
        _taskList.Add(new TaskItem("Прочитать книгу", null, Priority.Medium, Category.Study, Status.InProgress));
        _taskList.Add(new TaskItem("Встретиться с друзьями", "Планируемая встреча на выходных", Priority.Low, Category.Other, Status.New));
        _taskList.Add(new TaskItem("Сдать домашнее задание", null, Priority.High, Category.Study, Status.Done));
        _taskList.Add(new TaskItem("Убраться в комнате", "Привести в порядок рабочее место", Priority.Medium, Category.Home, Status.Done));
        _taskList.Add(new TaskItem("Записаться на курсы", null, Priority.Medium, Category.Other, Status.New));
    }
}
