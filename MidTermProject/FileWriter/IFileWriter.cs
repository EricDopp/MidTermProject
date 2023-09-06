using MidTermProject.Model;

namespace MidTermProject.FileWriter;

public interface IFileWriter
{
    void WriteFile(List<Book> book);
    List<Book> ReadFile();
}