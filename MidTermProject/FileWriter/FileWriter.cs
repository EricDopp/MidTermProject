namespace MidTermProject.FileWriter;
public abstract class FileWriter
{
    public string GetFilePath()
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;

        string solutionDirectory = Path.GetFullPath(Path.Combine(basePath, "..", "..", ".."));

        string fileOutputPath = Path.Combine(solutionDirectory, "OutputFile");

        Directory.CreateDirectory(fileOutputPath);

        string filePath = Path.Combine(fileOutputPath, "libraryCatalog.json");

        return filePath;
    }
}
