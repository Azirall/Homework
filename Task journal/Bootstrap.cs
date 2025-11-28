using TaskJournal;

public class Bootstrap
{
    public static void Main(string[] args)
    {
        TaskService taskService = new TaskService();
        LocalizationService localizationService = new LocalizationService();
        TaskConsoleApp taskConsoleApp = new TaskConsoleApp(localizationService, taskService);
        
        taskConsoleApp.Start();
    }
    
}