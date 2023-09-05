using MidTermProject.Model;
using MidTermProject.Repository;
using MidTermProject.Services.Interfaces;

namespace MidTermProject.Services;

public class BookService : IBookService
{
    private BookRepository _bookRepository;
    public BookService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public void CheckoutBook(string title)
    {
        bool checkedOut = false;
        var books = _bookRepository.GetAllBooks();
        foreach (var book in books)
        {
            if (book.Title == title && book.IsAvailable)
            {
                DateTime dueDate = DateTime.Now.AddDays(14);            
                book.DueDate = dueDate;
                book.IsAvailable = false;

                Console.WriteLine($"{book.Title} has been checked out. Please return by {dueDate.ToString("yyyy-MM-dd")}.");
                checkedOut = true;
                break;
            }
        }
        if (!checkedOut)
        {
            Console.WriteLine($"Sorry, {title} is not available for checkout.");
        }
        _bookRepository.WriteFile(books);
    }
    public void ReturnBook(string title)
    {
        bool returned = false;
        var books = _bookRepository.GetAllBooks();
        foreach (var book in books)
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
                returned = true;
                break;
            }
        }
        if (!returned)
        {
            Console.WriteLine($"Book '{title}' was not found or is not checked out.");
        }
        _bookRepository.WriteFile(books);
    }
}