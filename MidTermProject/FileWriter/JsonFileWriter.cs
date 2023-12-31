﻿using MidTermProject.Model;
using Newtonsoft.Json;

namespace MidTermProject.FileWriter;

public class JsonFileWriter : FileWriter, IFileWriter
{
    public void WriteFile(List<Book> book)
    {
        var jsonData = JsonConvert.SerializeObject(book, Formatting.Indented);

        File.WriteAllText(GetFilePath(), jsonData);
    }

    public List<Book> ReadFile()
    {
        var jsonData = File.ReadAllText(GetFilePath());

        try
        {
            var books = JsonConvert.DeserializeObject<List<Book>>(jsonData);

            if (books == null)
            {
                return new List<Book>();
            }

            return books;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return new List<Book>();
        }
    }
}