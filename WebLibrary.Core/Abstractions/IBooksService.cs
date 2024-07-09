using WebLibrary.Core.Models;

namespace WebLibrary.Application.Services
{
    public interface IBooksService
    {
        Task<Guid> CreateBook(Book book);
        Task<Guid> DeleteBook(Guid id);
        Task<List<Book>> GetAllBooks();
        Task<Guid> UpdateBook(Guid id, string title, string author, string genre, string publisher, bool isBooked);
    }
}