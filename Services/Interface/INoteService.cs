using ToDoList.Models;

namespace ToDoList.Services.Interface
{
    public interface INoteService
    {
        Task<IEnumerable<NoteDto>> GetAllNotesAsync();
        Task<NoteDto?> GetNoteByIdAsync(Guid id);
        Task<NoteDto> PostNoteAsync(NoteCreateDto noteDto);
        Task<bool> DeleteNoteAsync(Guid id);
        Task<bool> SetWeightWeightAsync(Guid id, float Weight);
        Task<bool> PutAddWeightAsync(Guid id, float Weight);
        Task<bool> PutTakeWeightAsync(Guid id, float Weight);
    }
}
