namespace TaskJournal;

public class TaskItem(string name, string? description, Priority priority, Category category, Status status)
{
    public string Name { get; private set; } = name;
    public string? Description { get; private set; } = description;
    public Priority Priority { get; private set; } = priority;
    public Category Category { get; private set; } = category;
    public Status Status { get; private set; } = status;

    public void ChangeTaskStatus(Status status)
    {
        Status = status;
    }
}