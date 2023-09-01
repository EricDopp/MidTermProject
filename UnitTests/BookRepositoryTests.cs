using MidTermProject.FileWriter;
using MidTermProject.Model;
using MidTermProject.Repository;

namespace UnitTests
{
    public class BookRepositoryTests
    {
        [Fact]
        public void CollectionIsAccurate()
        {
            JsonFileWriter writer = new JsonFileWriter();

            BookRepository bookRepository = new BookRepository(writer);
            var book = bookRepository.GetAllBooks();

            Assert.Equal(12, book.Count());
            Assert.True(book.Any(x => x.Title == "Literally 1984"));
        }
        [Fact]
        public void AddBook_Succeeds()
        {
            JsonFileWriter writer = new JsonFileWriter();
            BookRepository bookRepository = new BookRepository(writer);
            var newBook = bookRepository.GetAllBooks();

            Book book = new Book();
            book.Title = "Title";

            book.CheckoutTime = new DateTime();
            book.ReturnTime = new DateTime();
            book.DueDate = new DateTime();
            book.IsAvailable = false;
            book.Title = "Title";
            book.Author = null;
            book.Genre = null;

            bookRepository.SaveBook(book);
            newBook = bookRepository.GetAllBooks();
            Assert.True(newBook.Any(b => b.Title == "Title"));

            for (int i = 0; i < 3; i++)
            {
                newBook.Add(book);
            }
            writer.WriteFile(newBook);

            var itemToRemove = newBook.FindAll(item => item.Title == "Title");
            foreach (Book item in itemToRemove)
            {
                newBook.Remove(item);
            }


            writer.WriteFile(newBook);
            Assert.False(newBook.Any(x => x.Title == "Title"));
        }
        [Fact]
        public void RemoveBook_Succeeds()
        {
            JsonFileWriter writer = new JsonFileWriter();
            BookRepository bookRepository = new BookRepository(writer);
            var newBook = bookRepository.GetAllBooks();

            Book book = new Book();
            book.IsAvailable = true;
            book.Author = "John Smith";
            book.Genre = "Nonfiction";
            book.Title = "Title";

            bookRepository.DeleteBook(book);
            newBook = bookRepository.GetAllBooks();
            Assert.DoesNotContain(book, newBook);
        }
    }
}

//AppDomain.CurrentDomain.ProcessExit += BookRepository.OnProcessExit;