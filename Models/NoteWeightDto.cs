using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class NoteWeightDto
    {
        [Range(1, 100, ErrorMessage = "Вес файла должен быть от 1 до 100")]
        public float FileWeight { get; set; } = 100;
    }
}