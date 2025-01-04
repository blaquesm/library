namespace Backend.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
    public bool IsReserved { get; set; } = false;
    public bool IsIssued { get; set; } = false;
    public bool IsReturnBook { get; set; } = true;
}