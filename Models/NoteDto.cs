namespace ToDoList.Models
{
    public class NoteDto
    {
        public Guid Id { get; set; }
        public string HeadLine { get; set; } // Заголовок
        public string Title { get; set; } 
        public float FileWeight { get; set; } 
        public bool CheckPoint { get; set; } // Поставить подтверждение поинта
    }
}