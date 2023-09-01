using MidTermProject.FileWriter;
using MidTermProject.Model;
using MidTermProject.Repository.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Json;

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
    public void DeleteBook(Book book)
    {
        var books = GetAllBooks();

        var itemToRemove = books.FindAll(item => item.Title == "Title");
        foreach (Book item in itemToRemove)
        {
            books.Remove(item);
        }

        _writer.WriteFile(books);
    }
    public static void OnProcessExit(object sender, EventArgs e)
    {
        JsonFileWriter writer = new JsonFileWriter();
        BookRepository bookRepository = new BookRepository(writer);
        var newBook = bookRepository.GetAllBooks();

        bookRepository._writer.WriteFile(newBook);
    }
}
