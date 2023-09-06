namespace MidTermProject.Model;

public class Book
{
    public DateTime CheckoutTime { get; set; }
    public DateTime ReturnTime { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsAvailable { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
}