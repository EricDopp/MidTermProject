using MidTermProject.FileWriter;
using MidTermProject.Repository;
using MidTermProject.Services;


var writer = new JsonFileWriter();
var bookRepository = new BookRepository(writer);
var libraryService = new LibraryService(bookRepository);
AppDomain.CurrentDomain.ProcessExit += BookRepository.OnProcessExit;

libraryService.MainMenu();