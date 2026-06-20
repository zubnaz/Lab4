using Lab4.Interfaces;

namespace Lab4.Classes;

public class Library
{
    public Guid Id { get; set; }
    public ICollection<Book> Books { get; set; } = new List<Book>();
    public IStaffManager StaffManager { get; set; } 
    public ICollection<Librarian> Staff { get; set; } = new List<Librarian>();
    public INotifier Whistleblower { get; set; }

    private static Library? instance = null;
    private Library(IStaffManager staffManager, INotifier notifier)
    {
        Id = Guid.NewGuid();
        StaffManager = staffManager;
        Whistleblower = notifier;
    }
   
    public static Library Singleton(IStaffManager staffManager,INotifier whistleblower)
    {
        if(instance == null)
        {
            instance = new Library(staffManager, whistleblower);
        }
        return instance;
    }
    public Librarian[] GetStaff()
    {
       return StaffManager.GetAllStaffers(Staff);
    }
    public Librarian? GetStaffById(Guid Id)
    {
        return StaffManager.GetStafferById(Id, Staff);
    }
    public void HireStaff(Librarian librarian, int salary)
    {
        librarian.Library = this;
        Whistleblower.RegisterObserver(librarian);
        StaffManager.HireStaff(librarian,Staff,salary);
    }
    public void FireAnStaff(Librarian librarian)
    {
        librarian.Library = null;
        Whistleblower.RemoveObserver(librarian);
        StaffManager.FireAnStaff(librarian, Staff);
    }
}
public class StaffManager : IStaffManager
{
    public void HireStaff(Librarian librarian, ICollection<Librarian> Staff, int salary)
    {
        Staff.Add(librarian);
        librarian.Salary = salary;
    }
    public Librarian? GetStafferById(Guid Id, ICollection<Librarian> Staff)
    {
        return Staff.FirstOrDefault(librarian => librarian.Id == Id);
    }
    public Librarian[] GetAllStaffers(ICollection<Librarian> Staff)
    {
        return Staff.ToArray();
    }

    public void FireAnStaff(Librarian librarian, ICollection<Librarian> Staff)
    {
        Staff.Remove(librarian);
        librarian.Library = null;
    }
}
public abstract class Whistleblower : INotifier
{
    protected ICollection<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }
    public abstract void NotifyObservers(Book book, Librarian librarian);
}
public class WhistleblowerSMS : Whistleblower
{
    public override void NotifyObservers(Book book, Librarian librarian)
    {
        Console.WriteLine("SMS");
        foreach (var observer in observers)
        {
            observer.Update(book, librarian);
        }
    }
}
public class WhistleblowerEmail : Whistleblower
{
    public override void NotifyObservers(Book book, Librarian librarian)
    {
        Console.WriteLine("Email");
        foreach (var observer in observers)
        {
            observer.Update(book, librarian);
        }
    }
}