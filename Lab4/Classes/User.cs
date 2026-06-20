using Lab4.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Lab4.Classes;

public class Person
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Middlename { get; set; }
    public required Date Birthdate { get; set; }

    public Person(string Name, string Surname, string Middlename, Date Birthdate)
    {
        this.Id = Guid.NewGuid();
        this.Name = Name;
        this.Surname = Surname;
        this.Middlename = Middlename;
        this.Birthdate = Birthdate;
    }
    public Guid GetId()
    {
        return Id;
    }
    public string GetFullname()
    {
        return $"{Surname} {Name} {Middlename}";
    }
    public Date GetBirthdate()
    {
        return Birthdate;
    }
}

public class Writer : Person
{
    public string? Nickname { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();

    [SetsRequiredMembers]
    public Writer(string Name, string Surname, string Middlename, Date Birthdate) : base(Name, Surname, Middlename, Birthdate)
    {
    }
    [SetsRequiredMembers]
    public Writer(string Name, string Surname, string Middlename, Date Birthdate, string Nickname) : base(Name, Surname, Middlename, Birthdate)
    {
        this.Nickname = Nickname;
    }
    public string? GetNickname()
    {
        return Nickname;
    }
    public Book[] GetAllBooks()
    {
        return Books.ToArray();
    }
    public Book? GetBooksById(Guid Id)
    {
        return Books.FirstOrDefault(book => book.Id == Id);
    }
    public void WriteBook(Book book)
    {
        Books.Add(book);
    }

}

public class Librarian : Person, IObserver
{
    public int Salary { get; set; }
    public Library? Library { get; set; }

    [SetsRequiredMembers]
    public Librarian(string Name, string Surname, string Middlename, Date Birthdate) : base(Name, Surname, Middlename, Birthdate) { }
    public int GetSalary()
    {
        return Salary;
    }
    public void AddBook(Book book)
    {
        if(Library != null)
        {
            Library.Books.Add(book);
            Library.Whistleblower.NotifyObservers(book,this);
        }
           
    }
    public void Update(Book book, Librarian librarian)
    {
        if(librarian.Id != this.Id)
            Console.WriteLine($"Librarian {librarian.Name} {librarian.Surname} has added a new book: {book.Name} by {book.Author.Surname} {book.Author.Name} {book.Author.Middlename} ");
    }
}

