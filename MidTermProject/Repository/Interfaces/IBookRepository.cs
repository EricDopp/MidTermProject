using MidTermProject.Model;

namespace MidTermProject.Repository.Interfaces;

public interface IBookRepository
{
    List<Book> GetAllBooks();
    void SaveBook(Book book);
}