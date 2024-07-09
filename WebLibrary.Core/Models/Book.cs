namespace WebLibrary.Core.Models
{
    public class Book
    {
        public const int MaxLenght = 250;
        private Book (Guid id, string title, string author, string genre, string publisher, bool isBooked)
        {
            Id = id;
            Title = title;
            Author = author;
            Genre = genre;
            Publisher = publisher;
            IsBooked = isBooked;
        }

        public Guid Id { get; }

        public string Title { get; } = string.Empty;

        public string Author { get; } = string.Empty;

        public string Genre { get; } = string.Empty;

        public string Publisher { get; } = string.Empty;

        public bool IsBooked { get; } = false;

        public static (Book Book, string Error) Create(Guid id, string title, string author, string genre, string publisher, bool isBooked)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MaxLenght)
            {
                error = "Field Title can not be empty or longer than 250 symbols";
            }

            if (string.IsNullOrEmpty(author) || author.Length > MaxLenght)
            {
                error = "Field Author can not be empty or longer than 250 symbols";
            }

            if (string.IsNullOrEmpty(genre) || genre.Length > MaxLenght)
            {
                error = "Field Genre can not be empty or longer than 250 symbols";
            }

            if (string.IsNullOrEmpty(publisher) || publisher.Length > MaxLenght)
            {
                error = "Field Publisher can not be empty or longer than 250 symbols";
            }
            var book = new Book(id, title, author, genre, publisher, isBooked);

            return (book, error);
        }
    }
}
