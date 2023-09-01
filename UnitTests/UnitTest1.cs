using MidTermProject.FileWriter;
using MidTermProject.Repository;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Literally1984()
        {
            JsonFileWriter writer = new JsonFileWriter();

            BookRepository bookRepository = new BookRepository(writer);
            var book = bookRepository.GetAllBooks();

            Assert.Equal(12, book.Count());
            Assert.True(book.Any(x => x.Title == "Literally 1984"));
        }
    }
}