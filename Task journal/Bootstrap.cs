using TaskJournal;

public class Bootstrap
{
    public static void Main(string[] args)
    {
        TaskService taskService = new TaskService();
        TaskManager taskManager = new TaskManager(taskService);
        
        LocalizationService localizationService = new LocalizationService();
        ConsoleUi consoleUi = new ConsoleUi(localizationService,taskManager);
        
        consoleUi.Start();
    }
    
}