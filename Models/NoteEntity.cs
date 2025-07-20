using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class NoteEntity
    {
        public Guid Id { get; set; }

        [MinLength(1, ErrorMessage = "Заполните поле от 1 символа")]
        public string HeadLine { get; set; } = string.Empty; // Заголовок

        [MinLength(1, ErrorMessage = "Заполните поле от 1 символа")]
        public string Title { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Вес файла от 1 до 100 цифр")]
        public float FileWeight { get; set; } = 100;
        public bool CheckPoint { get; set; } // Поставить подтверждение
    }
}