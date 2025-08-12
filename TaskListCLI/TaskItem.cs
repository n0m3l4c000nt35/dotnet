public class TaskItem
{
    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public override string ToString()
    {
        return $"{Description} (Due: {DueDate:dd/MM/yyyy})";
    }
}