namespace TaskJournal;

public class TaskManager (TaskService taskService)
{
    public void CreateNewTask(TaskItem task)
    {
        taskService.AddTask(task);
    }

    public IReadOnlyList<TaskItem> GetAllTaskList()
    {
        return taskService.GetAllTasks();
    }

    public IReadOnlyList<TaskItem> GetTaskListByType<TEnum>() where TEnum : Enum
    {
        return taskService.GetTaskListByType<TEnum>();
    }

    public void CompleteTask(int taskId)
    {
        taskService.ChangeTaskStatusToComplete(taskId);
    }

    public void DeleteTask(int taskId)
    {
        taskService.DeleteTask(taskId);
    }
}