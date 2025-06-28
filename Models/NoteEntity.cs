namespace ToDoList.Models
{
    public class NoteEntity
    {
        public Guid Id { get; set; }
        public string HeadLine { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public bool CheckPoint { get; set; }
    }
}