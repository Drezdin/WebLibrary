namespace Web.Contracts
{
    public record BooksRequest(string Title, string Author, string Genre, string Publisher, bool IsBooked);

}