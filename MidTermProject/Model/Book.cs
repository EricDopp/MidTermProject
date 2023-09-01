using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MidTermProject.Model;

public class Book
{
    internal string? CheckedOutBy;

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
    public DateTime CheckoutTime { get; set; } 
    public DateTime ReturnTime { get; set; } 
    public DateTime DueDate { get; set; } 
    public bool IsAvailable { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Genre { get; set; }
}
