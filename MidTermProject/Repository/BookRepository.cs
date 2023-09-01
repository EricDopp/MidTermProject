using MidTermProject.FileWriter;
using MidTermProject.Model;
using MidTermProject.Repository.Interfaces;

namespace MidTermProject.Repository;

public class BookRepository : IBookRepository
{
    private IFileWriter _writer;

    public BookRepository(IFileWriter writer)
    {
        _writer = writer;
    }

    public List<Book> GetAllBooks()
    {
        return _writer.ReadFile();
    }

    public void SaveBook(Book book)
    {
        var books = GetAllBooks();

        books.Add(book);

        _writer.WriteFile(books);
    }
}
