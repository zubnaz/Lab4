using Lab4.Classes;

namespace Lab4.Interfaces;

public interface IObserver
{
    public void Update(Book book, Librarian librarian);
}

