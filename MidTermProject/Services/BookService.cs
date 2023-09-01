using MidTermProject.Model;
using MidTermProject.Services.Interfaces;

namespace MidTermProject.Services;

public class BookService : IBookService
{
    public DateTime DueDate { get; set; }

    private List<Book> _books = new List<Book>();

    public void CheckoutBook(string title)
    {
        foreach (var book in _books)
        {
            if (book.Title == title && book.IsAvailable)
            {
                DateTime dueDate = DateTime.Now.AddDays(14);            
                book.DueDate = dueDate;
                book.IsAvailable = false;

                Console.WriteLine($"{book.Title} has been checked out. Please return by {dueDate.ToString("yyyy-MM-dd")}.");    
            }
        }
        Console.WriteLine($"Sorry, {title} is not available for checkout.");
    }

    public void ReturnBook(string title)
    {
        foreach (var book in _books)
        {
            if (book.Title == title && book.IsAvailable != true)
            {
                int daysLate = (DateTime.Now - book.DueDate).Days;
                if (daysLate > 0)
                {                   
                    Console.WriteLine($"Book '{book.Title}' is {daysLate} days late.");
                }
                book.DueDate = DateTime.MinValue;
                book.IsAvailable = true;

                Console.WriteLine($"Book '{book.Title}' has been returned.");
                break;
            }
        }
        Console.WriteLine($"Book '{title}' was not found or is not checked out.");
    }
}