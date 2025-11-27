namespace TaskJournal;

public class ConsoleUi(LocalizationService localizationService, TaskManager taskManager)
{
    public void Start()
    {
        while (true)
        {
            ConsoleLoop();
        }
        
    }
    private void ConsoleLoop()
    {
        ShowMenu();
        int input = ReadIntInput("Сделайте выбор: ");
        
        switch (input)
        {
            case 1:
                CreateNewTask();
                break;
            case 2:
                ShowAllTasks();
                break;
            case 3:
                ShowSortedTask();
                break;
        }
    }
    
    private void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("1) Добавить задачу");
        Console.WriteLine("2) Посмотреть все задачи");
        Console.WriteLine("3) Вывести сортировку по типам");
    }

    private int ReadIntInput(string inputText)
    {
        Console.Write(inputText);
        if (int.TryParse(Console.ReadLine(), out int input))
        {
            return input;
        }
        return -1;
    }

    private string? ReadStringInput(string inputText)
    {
        Console.WriteLine(inputText);
        return Console.ReadLine();
    }
    private void CreateNewTask()
    {
        Console.WriteLine("\n");
        string? name = ReadStringInput("Введите имя");
        EnsureNotEmpty(ref name);
        
        string? description = ReadStringInput("Введите описание ");
        
        Console.WriteLine("Приоритет задачи:");
        Priority priority = HandleEnumListChoice<Priority>();
        
        Console.Write("Категория задачи: \n");
        Category category = HandleEnumListChoice<Category>();
        
        Console.Write("Статус задачи: ");
        Status status = HandleEnumListChoice<Status>();
        
        TaskItem task = new TaskItem(name, description, priority, category, status);
        taskManager.CreateNewTask(task);
    }
    private void ShowAllTasks()
    {
        IReadOnlyList<TaskItem> allTasks = taskManager.GetAllTaskList();
        ShowTaskList(allTasks);
    }

    private void ShowSortedTask()
    {
        Console.WriteLine();
        Console.WriteLine("Показать задания сортированные по: ");
        Console.WriteLine("1) Статусу");
        Console.WriteLine("2) Приоритету");
        Console.WriteLine("3) Категории");

        int choice = ReadIntInput("Выберете категорию: ");

        switch (choice)
        {
            case 1: ShowSortedTaskByType<Status>();
                break;
            case 2: ShowSortedTaskByType<Priority>();
                break;
            case 3: ShowSortedTaskByType<Category>();
                break;
        }
    }

    private void ShowSortedTaskByType<TEnum>() where TEnum : Enum
    {
      var tasksByType =  taskManager.GetTaskListByType<TEnum>();
      ShowTaskList(tasksByType);
    }

    private void ShowTaskList(IReadOnlyList<TaskItem> tasks)
    {
        Console.WriteLine();
        if (tasks.Count == 0)
        {
            Console.WriteLine("Список задач пуст.");
            return;
        }
        Console.WriteLine();

        for (int index = 0; index < tasks.Count; index++)
        {
            TaskItem task = tasks[index];
            string description = string.IsNullOrWhiteSpace(task.Description)
                ? "описание отсутствует"
                : task.Description!;

            Console.WriteLine(
                $"{index + 1}) {task.Name} | {description} | {localizationService.ToRussian(task.Priority)} | {localizationService.ToRussian(task.Category)} | {localizationService.ToRussian(task.Status)}");
        }
    }
    private void EnsureNotEmpty(ref string? obj)
    {
        while (string.IsNullOrWhiteSpace(obj))
        {
            Console.WriteLine("Поле не может быть пустым. Повторите ввод:");
            obj =  Console.ReadLine();
        }
    }
    private TEnum HandleEnumListChoice<TEnum>() where TEnum :  Enum
    {
        var values = (TEnum[])Enum.GetValues(typeof(TEnum));

        for (int i = 0; i < values.Length; i++)
        {
            var enumValue = values[i];
            Console.WriteLine($"{i + 1}) {localizationService.ToRussian(enumValue)}");
        }
        int choice = ReadIntInput("Сделайте выбор ");
        
        Console.WriteLine("\n");
        
        while (choice < 1 || choice > values.Length)
        {
            choice = ReadIntInput("Введенное значение выходит за рамки выбора, повторите ввод");
        }
        return  values[choice-1];
    }

    private void ChangeTaskStatus()
    {
        
    }

}