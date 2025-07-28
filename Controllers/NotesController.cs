using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;
using ToDoList.Services.Interface;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }
        /// <summary>
        /// Возвращает все заметки
        /// </summary>
        /// <returns></returns>
        
        // GET: api/v1/notes
        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _noteService.GetAllNotesAsync();

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
            var note = await _noteService.GetNoteByIdAsync(id);
            if (note == null) return NotFound();

            return Ok(note);
        }

        /// <summary>
        /// Создаёт новую заметку
        /// </summary>
        /// <param name="newNote"></param>
        /// <returns></returns>
        // POST: api/v1/notes
        [HttpPost]
        public async Task<IActionResult> PostNote(NoteCreateDto note)
        {   
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _noteService.PostNoteAsync(note);

            return CreatedAtAction(nameof(GetNoteById), new { id = result.Id }, result);
        }

        
        // DELETE: api/v1/notes
        [HttpDelete]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var deleted = await _noteService.DeleteNoteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateNote(Guid id, NoteUpdateDto dto) // Не в контроллер
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
        public async Task<IActionResult> SetWeight(Guid id,[FromBody] float Weight)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var set = await _noteService.SetWeightWeightAsync(id, Weight);
            if (!set) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Прибавляет размер файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FileWeight"></param>
        /// <returns></returns>
        [HttpPut("{id}/add-weight")]
        public async Task<IActionResult> PutAddWeight(Guid id,[FromBody] float Weight)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var Add = await _noteService.PutAddWeightAsync(id, Weight);
            if (!Add) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Забирает размер файла
        /// </summary>
        /// <param name="id"></param>
        /// <param name="FileWeight"></param>
        /// <returns></returns>
        [HttpPut("{id}/take-weight")]
        public async Task<IActionResult> PutTakeWeight(Guid id,[FromBody] float Weight)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var note = await _noteService.PutTakeWeightAsync(id, Weight);
            if (!note) return NotFound();

            return NoContent();
        }
    }
}
