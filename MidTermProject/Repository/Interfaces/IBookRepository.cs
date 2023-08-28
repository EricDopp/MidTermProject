using MidTermProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermProject.Repository.Interfaces;

public interface IBookRepository
{
    List<Book> GetAllBooks();
    void SaveBook(Book book);
}
