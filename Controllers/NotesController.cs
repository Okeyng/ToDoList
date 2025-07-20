using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly LearningDbContext _context;

        public NotesController(LearningDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Получает все заметки
        /// </summary>
        /// <returns></returns>
        
        // GET: api/v1/notes
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _context.Notes.ToListAsync();
            return Ok(notes);
        }

        /// <summary>
        /// Получает заметку по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/v1/notes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);
            return Ok(note);
        }

        /// <summary>
        /// Создаёт новую заметку
        /// </summary>
        /// <param name="newNote"></param>
        /// <returns></returns>
        // POST: api/v1/notes
        [HttpPost]
        public async Task<IActionResult> PostNote(NoteEntity newNote)
        {   
            await _context.Notes.AddAsync(newNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNoteById), new { id = newNote.Id }, newNote);
        }

        // DELETE: api/v1/notes
        [HttpDelete]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateNote(Guid id, NoteUpdateDto dto)
        //{
        //    var note = await _context.Notes.FindAsync(id);
        //    if (note == null) return NotFound();

        //    // Явное обновление полей
        //    note.HeadLine = dto.HeadLine;
        //    note.Title = dto.Title;
        //    note.CheckPoint = dto.CheckPoint;

        //    await _context.SaveChangesAsync();
        //    return NoContent();
        //}

        /// <summary>
        /// Выбираем размер файла (Пользователь выбирает размер файла, для теста)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FileWeight"></param>
        /// <returns></returns>
        // PUT: api/v1/notes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeight(Guid id, float FileWeight)
        {
            var note = await _context.Notes.FindAsync(id);

            if (note == null) return NotFound();
            if (FileWeight <= 0) return BadRequest();

            note.FileWeight = FileWeight;

            await _context.SaveChangesAsync();
            return NoContent();

        }
    }
}
