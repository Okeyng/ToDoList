using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ToDoList.Models;
using ToDoList.Services.Interface;

namespace ToDoList.Controllers
{
    public class NoteService : INoteService
    {
        private readonly LearningDbContext _context;

        public NoteService(LearningDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает все заметки
        /// </summary>
        /// <returns></returns>

        // GET: api/v1/notes
        public async Task<IEnumerable<NoteDto>> GetAllNotesAsync()
        {
           return await _context.Notes
                .Select(n => new NoteDto    
                {
                    Id = n.Id,
                    HeadLine = n.HeadLine,
                    Title = n.Title,
                    FileWeight = n.FileWeight,
                    CheckPoint = n.CheckPoint,
                })
                .ToListAsync();
        }

        /// <summary>
        /// Получает заметку по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/v1/notes/{id}
        public async Task<NoteDto?> GetNoteByIdAsync(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return null;

            return new NoteDto
            {
                Id = note.Id,
                HeadLine = note.HeadLine,
                Title = note.Title,
                FileWeight = note.FileWeight,
                CheckPoint = note.CheckPoint,
            };
        }

        /// <summary>
        /// Создаёт новую заметку
        /// </summary>
        /// <param name="newNote"></param>
        /// <returns></returns>
        // POST: api/v1/notes
        public async Task<NoteDto> PostNoteAsync(NoteCreateDto noteDto)
        {
            var newNote = new NoteEntity
            {
                Id = Guid.NewGuid(),
                HeadLine = noteDto.HeadLine,
                Title = noteDto.Title,
                FileWeight = noteDto.FileWeight,
                CheckPoint = noteDto.CheckPoint,
            };
            await _context.Notes.AddAsync(newNote);
            await _context.SaveChangesAsync();

            return new NoteDto
            {
                Id = newNote.Id,
                HeadLine = newNote.HeadLine,
                Title = newNote.Title,
                FileWeight = newNote.FileWeight,
                CheckPoint = newNote.CheckPoint,
            };
        }

        // DELETE: api/v1/notes
        public async Task<bool> DeleteNoteAsync(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return false;
            

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Выбираем размер файла (Пользователь выбирает размер файла, для теста)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FileWeight"></param>
        /// <returns></returns>
        // PUT: api/v1/notes/{id}
        public async Task<bool> SetWeightWeightAsync(Guid id, float weight)
        {

            var note = await _context.Notes.FindAsync(id);
            if (note == null) return false;

            note.FileWeight = weight;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Прибавляет размер файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FileWeight"></param>
        /// <returns></returns>
        public async Task<bool> PutAddWeightAsync(Guid id, float weight)
        {

            var note = await _context.Notes.FindAsync(id);
            if (note == null) return false;

            var newWeight = note.FileWeight + weight;
            if (newWeight > 100)
            {
                note.FileWeight = 100;

                await _context.SaveChangesAsync();
                return true;
            }

            note.FileWeight = newWeight;

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Забирает размер файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FileWeight"></param>
        /// <returns></returns>
        public async Task<bool> PutTakeWeightAsync(Guid id, float weight)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null) return false;

            var newWeight = note.FileWeight - weight;
            if (newWeight < 0)
            {
                newWeight = 0;

                await _context.SaveChangesAsync();
                return true;
                throw new ValidationException("Вес файла не может быть меньше 0");
            }

            note.FileWeight = newWeight;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
