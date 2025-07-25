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
            var notes = await _context.Notes
                .Select(n => new NoteDto
                {
                    Id = n.Id,
                    HeadLine = n.HeadLine,
                    Title = n.Title,
                    FileWeight = n.FileWeight,
                    CheckPoint = n.CheckPoint,
                })
                .ToListAsync();

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
            if (note == null) return NotFound();

            var dto = new NoteDto
            {
                Id = note.Id,
                HeadLine = note.HeadLine,
                Title = note.Title,
                FileWeight = note.FileWeight,
                CheckPoint = note.CheckPoint,
            };                
            return Ok(dto);
        }

        /// <summary>
        /// Создаёт новую заметку
        /// </summary>
        /// <param name="newNote"></param>
        /// <returns></returns>
        // POST: api/v1/notes
        [HttpPost]
        public async Task<IActionResult> PostNote([FromBody] NoteCreateDto noteDto)
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);

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

            var result = new NoteDto
            {
                Id = newNote.Id,
                HeadLine = newNote.HeadLine,
                Title = newNote.Title,
                FileWeight = newNote.FileWeight,
                CheckPoint = newNote.CheckPoint,
            };

            return CreatedAtAction(nameof(GetNoteById), new { id = newNote.Id }, result);
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
        [HttpPut("{id}/set-weight")]
        public async Task<IActionResult> PutWeight(Guid id, [FromBody] NoteWeightDto WeightDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();

            note.FileWeight = WeightDto.FileWeight;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Прибавляет размер файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FileWeight"></param>
        /// <returns></returns>
        [HttpPut("{id}/add-weight")]
        public async Task<IActionResult> PutAddWeight(Guid id, [FromBody] NoteWeightDto WeightDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();


            var newWeight = note.FileWeight += WeightDto.FileWeight;
            if (newWeight > 100)
            {
                return BadRequest(new
                {
                    error = "Вес файла должен быть от 1 до 100",
                    result = newWeight // Результат сложения
                });
            }
            note.FileWeight = newWeight;

            await _context.SaveChangesAsync();
            return NoContent();

        }

        /// <summary>
        /// Забирает размер файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FileWeight"></param>
        /// <returns></returns>
        [HttpPut("{id}/take-weight")]
        public async Task<IActionResult> PutTakeWeight(Guid id, [FromBody] NoteWeightDto WeightDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var note = await _context.Notes.FindAsync(id);
            if (note == null) return NotFound();


            var newWeight = note.FileWeight -= WeightDto.FileWeight;
            if (newWeight < 0)
            {
                return BadRequest(new
                {
                    error = "Вес файла должен быть от 1 до 100",
                    result = newWeight // Результат сложения
                });
            }

            note.FileWeight = newWeight;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
