using Microsoft.EntityFrameworkCore;
using WebLibrary.Core.Models;
using WebLibrary.DataAccess.Entites;

namespace WebLibrary.DataAccess.Repositories
{
    public class BooksRepository : IBooksRepository
    {
        private readonly WebLibraryDbContext _context;
        public BooksRepository(WebLibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> Get()
        {
            var bookeEntities = await _context.Books.AsNoTracking().ToListAsync();

            var books = bookeEntities.Select(b => Book.Create(b.Id, b.Title, b.Author, b.Genre, b.Publisher, b.IsBooked).Book).ToList();

            return books;
        }

        public async Task<Guid> Create(Book book)
        {
            var bookEntity = new BookEntity
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Publisher = book.Publisher,
                IsBooked = book.IsBooked,
            };

            await _context.Books.AddAsync(bookEntity);
            await _context.SaveChangesAsync();

            return bookEntity.Id;
        }
        public async Task<Guid> Update(Guid id, string title, string author, string genre, string publisher, bool isBooked)
        {
            await _context.Books.Where(b => b.Id == id).ExecuteUpdateAsync(s => s.SetProperty(b => b.Title, b => title).SetProperty(b => b.Author, b => author).SetProperty(b => b.Genre, b => genre).SetProperty(b => b.Publisher, b => publisher).SetProperty(b => b.IsBooked, b => isBooked));
            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Books.Where(b => b.Id == id).ExecuteDeleteAsync();
            return id;
        }
    }
}
