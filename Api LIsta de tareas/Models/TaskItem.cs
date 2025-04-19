namespace Api_LIsta_de_tareas.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
