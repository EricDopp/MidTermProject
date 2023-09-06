using MidTermProject.FileWriter;
using MidTermProject.Repository;
using MidTermProject.Services;

namespace UnitTests;
public class BookServiceTests
{
    [Fact]
    public void CannotCheckoutNonexistentBook()
    {
        var writer = new JsonFileWriter();
        var bookRepository = new BookRepository(writer);
        var bookService = new BookService(bookRepository);
        var title = "Nonexistent Book";

        bookService.CheckoutBook(title);
    }
    [Fact]
    public void CanCheckoutAvailableBook()
    {
        var writer = new JsonFileWriter();
        var bookRepository = new BookRepository(writer);
        var bookService = new BookService(bookRepository);
        var title = "The Great Gatsby";

        bookService.CheckoutBook(title);
    }
    [Fact]
    public void CanCheckoutAndReturnBookWithCorrectDueDate()
    {
        var writer = new JsonFileWriter();
        var bookRepository = new BookRepository(writer);
        var bookService = new BookService(bookRepository);
        var title = "The Great Gatsby";

        bookService.CheckoutBook(title);
        bookService.ReturnBook(title);
    }
    [Fact]
    public void BookReturnedOnTime()
    {
        var writer = new JsonFileWriter();
        var bookRepository = new BookRepository(writer);
        var bookService = new BookService(bookRepository);
        var title = "The Great Gatsby";

        bookService.CheckoutBook(title);
        bookService.ReturnBook(title);
    }
    [Fact]
    public void BookReturnedWithin14Days()
    {
        var writer = new JsonFileWriter();
        var bookRepository = new BookRepository(writer);
        var bookService = new BookService(bookRepository);
        var title = "The Great Gatsby";

        bookService.CheckoutBook(title);
        bookService.ReturnBook(title);
    }
}