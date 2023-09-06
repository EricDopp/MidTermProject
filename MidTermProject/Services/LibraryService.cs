using MidTermProject.Model;
using MidTermProject.Repository;
using Spectre.Console;

namespace MidTermProject.Services;

public class LibraryService
{
    private readonly BookRepository _bookRepository;
    private readonly BookService _bookService;
    public LibraryService(BookRepository bookRepository)
    {
        _bookRepository = bookRepository;
        _bookService = new BookService(bookRepository);
    }

    public void MainMenu()
    {
        while (true)
        {
            AnsiConsole.MarkupLine($"[bold darkorange]Welcome to the Grand Circus Library![/]");
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What would you like to do?")
                    .PageSize(4)
                    .AddChoices(new[]
                    {
                    "Display book list",
                    "Search for a book",
                    "Return a book",
                    "Exit"
                    }));
            switch (choice)
            {
                case "Display book list":
                    _DisplayBookTable();
                    _PromptForCheckout();
                    break;
                case "Search for a book":
                    _SearchBooks();
                    _PromptForCheckout();
                    break;
                case "Return a book":
                    _DisplayCheckedOutBooks();
                    break;
                case "Exit":
                    AnsiConsole.MarkupLine("Goodbye!");
                    return;
                default:
                    AnsiConsole.MarkupLine("Invalid choice.");
                    break;
            }
            if (!_ContinueChecker())
            {
                AnsiConsole.MarkupLine("Goodbye!");
                return;
            }
        }
    }
    private void _DisplayBookTable(List<Book> booksToDisplay = null)
    {
        var table = new Table();
        table.Border(TableBorder.HeavyHead);
        table.AddColumn(new TableColumn("Title").Centered());
        table.AddColumn(new TableColumn("Author").Centered());
        table.AddColumn(new TableColumn("Genre").Centered());
        table.AddColumn(new TableColumn("Availability").Centered());

        List<Book> books = booksToDisplay ?? _bookRepository.GetAllBooks(); // Uses default if no filtered list is provided

        for (int i = 0; i < books.Count; i++)
        {
            var book = books[i];
            var availability = book.IsAvailable ? "Available" : "Checked Out";
            if (i % 2 == 0)
            {
                table.AddRow(
                    new Markup($"[italic deepskyblue2]{book.Title}[/]"),
                    new Markup($"{book.Author}"),
                    new Markup($"{book.Genre}"),
                    new Markup($"[wheat1]{availability}[/]")
                );
            }
            else
            {

                table.AddRow(
                    new Markup($"[italic deepskyblue3]{book.Title}[/]"),
                    new Markup($"[grey62]{book.Author}[/]"),
                    new Markup($"[grey62]{book.Genre}[/]"),
                    new Markup($"[lightgoldenrod2]{availability}[/]")
                    );
            }
        }
        AnsiConsole.Write(table);
    }
    private void _PromptForCheckout()
    {
        var titleToCheckout = AnsiConsole.Prompt(
        new TextPrompt<string>("Enter the title of the book you want to checkout:")
            .Validate(title =>
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    return ValidationResult.Error("[red]Title is required.[/]");
                }
                else if (title.Length > 100)
                {
                    return ValidationResult.Error("[red]Title must be 100 characters or less.[/]");
                }
                return ValidationResult.Success();
            }));

        _bookService.CheckoutBook(titleToCheckout);
    }
    private void _DisplayCheckedOutBooks()
    {
        var books = _bookRepository.GetAllBooks();
        var checkedOutBooks = books.FindAll(book => !book.IsAvailable);
        if (checkedOutBooks.Count > 0)
        {
            _DisplayBookTable(checkedOutBooks);

            var bookToReturn = AnsiConsole.Prompt(
                new TextPrompt<string>("Enter the title of the book you want to return:")
                    .Validate(title =>
                    {
                        if (string.IsNullOrWhiteSpace(title))
                        {
                            return ValidationResult.Error("[red]Title is required.[/]");
                        }
                        else if (title.Length > 100)
                        {
                            return ValidationResult.Error("[red]Title must be 100 characters or less.[/]");
                        }
                        return ValidationResult.Success();
                    }));

            _bookService.ReturnBook(bookToReturn);
        }
        else
        {
            AnsiConsole.MarkupLine("[italic]No checked-out books found.[/]");
        }
    }
    private void _SearchBooks()
    {
        var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Search by:")
                .PageSize(3)
                .AddChoices(new[] { "Author", "Title", "Back" })
        );
        if (choice == "Back")
        {
            MainMenu();
            return;
        }
        var keyword = AnsiConsole.Prompt(new TextPrompt<string>($"Enter the {choice} keyword:")
            .Validate(keyword =>
            {
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    return ValidationResult.Error("[red]Keyword is required.[/]");
                }
                else if (keyword.Length > 100)
                {
                    return ValidationResult.Error("[red]Title must be 100 characters or less.[/]");
                }
                return ValidationResult.Success();
            }));

        if (choice == "Author")
        {
            var books = _bookRepository.GetAllBooks();
            var foundBooks = books.FindAll(book =>
                book.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            _DisplayBookTable(foundBooks);
        }
        else if (choice == "Title")
        {
            var books = _bookRepository.GetAllBooks();
            var foundBooks = books.FindAll(book =>
                book.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase));
            _DisplayBookTable(foundBooks);
        }
    }
    private static bool _ContinueChecker()
    {
        AnsiConsole.Markup("[bold]Would you like to continue? (Y/N): [/]");
        string userInput;

        while (true)
        {
            userInput = Console.ReadLine().Trim().ToLower();

            if (userInput == "y")
            {
                Console.Clear();
                return true;
            }
            else if (userInput == "n")
            {
                return false;
            }
            AnsiConsole.Markup("[red]Invalid input.[/] Please answer with either '[bold]Y[/]' or '[bold]N[/]': ");
        }
    }
}