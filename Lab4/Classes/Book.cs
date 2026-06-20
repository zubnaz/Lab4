using System.Diagnostics.CodeAnalysis;

namespace Lab4.Classes;

public class Book
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required Writer Author { get; set; }
    public int Year { get; set; }

    [SetsRequiredMembers]
    public Book(string Name, Writer Author, int Year)
    {
        this.Id = Guid.NewGuid();
        this.Name = Name;
        this.Author = Author;
        this.Year = Year;
    }
    public Guid GetId()
    {
        return Id;
    }
    public string GetName()
    {
        return Name;
    }
    public Writer GetAuthor()
    {
        return Author;
    }
    public int GetYear()
    {
        return Year;
    }
    public string GetFullInfo()
    {
        return $"Name: {Name}, Author: {Author.GetFullname()}, Year: {Year}";
    }
}
