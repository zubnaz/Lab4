using Lab4.Classes;

namespace Lab4.Interfaces;

public interface INotifier
{
    public void NotifyObservers(Book book, Librarian librarian);
    public void RegisterObserver(IObserver observer);
    public void RemoveObserver(IObserver observer);
}
