using WebLibrary.Core.Models;

namespace WebLibrary.DataAccess.Repositories
{
    public interface IBooksRepository
    {
        Task<Guid> Create(Book book);
        Task<Guid> Delete(Guid id);
        Task<List<Book>> Get();
        Task<Guid> Update(Guid id, string title, string author, string genre, string publisher, bool isBooked);
    }
}