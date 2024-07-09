namespace Web.Contracts
{
    public record BooksResponse(Guid Id, string Title, string Author, string Genre, string Publisher, bool IsBooked);
}
