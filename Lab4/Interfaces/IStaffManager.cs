using Lab4.Classes;

namespace Lab4.Interfaces;

public interface IStaffManager
{
    public void HireStaff(Librarian librarian, ICollection<Librarian> Staff,int Salary);
    public Librarian? GetStafferById(Guid Id, ICollection<Librarian> Staff);
    public Librarian[] GetAllStaffers(ICollection<Librarian> Staff);
    public void FireAnStaff(Librarian librarian, ICollection<Librarian> Staff);
}
