using MidTermProject.Services;

namespace UnitTests // Renamed namespace to UnitTests
{
    public class BookServiceTests
    {
        [Fact]
        public void CannotCheckoutNonexistentBook()
        {
            var bookService = new BookService();
            var title = "Nonexistent Book";

            bookService.CheckoutBook(title);
        }
        [Fact]
        public void CanCheckoutAvailableBook()
        {
            var bookService = new BookService();
            var title = "The Great Gatsby"; 

            // Act
            bookService.CheckoutBook(title);

            
        }
        [Fact]
        public void CanCheckoutAndReturnBookWithCorrectDueDate()
        {
            var bookService = new BookService();
            var title = "The Great Gatsby"; 

            bookService.CheckoutBook(title);
            bookService.ReturnBook(title);
        }

        [Fact]
        public void BookReturnedOnTime()
        {
            var bookService = new BookService();
            var title = "The Great Gatsby"; 

            bookService.CheckoutBook(title);

            DateTime dueDate = DateTime.Now.AddDays(14);
            bookService.ReturnBook(title);
        }
        [Fact]
        public void BookReturnedWithin14Days()
        {
            var bookService = new BookService();
            var title = "The Great Gatsby"; 

            bookService.CheckoutBook(title);

            DateTime returnDate = DateTime.Now.AddDays(10);

            bookService.ReturnBook(title);
        }
    }
}

