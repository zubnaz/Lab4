using Lab4.Classes;
using Lab4.Interfaces;
using Moq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tests
{
    public class Tests
    {
        private Library library;

        private Librarian librarian;
        private Mock<INotifier> mockNotifier;
        private Mock<IStaffManager> mockStaffManager;

        [SetUp]
        public void Init()
        {
            mockNotifier = new Mock<INotifier>();
            mockStaffManager = new Mock<IStaffManager>();
        }

        [Test]
        public void HireStaffTest()
        {
            if (library == null) return;

            library = Library.Singleton(new StaffManager(), new WhistleblowerEmail());
            librarian = new Librarian("Петро", "Коваль", "Олексійович", new Lab4.Classes.Date(13, 7, 2003));

            var countOfStaffBeforeHire = library.Staff.Count;
            library.HireStaff(librarian, 10000);
            var countOfStaffAfterHire = library.Staff.Count;

            Assert.That(countOfStaffBeforeHire < countOfStaffAfterHire, Is.True);
            Assert.That(library.Staff.Contains(librarian), Is.True);
        }
        [Test]
        public void TestNotifier()
        {
            library = Library.Singleton(mockStaffManager.Object, mockNotifier.Object);
            librarian = new Librarian("Ігор", "Стрілець", "Борисович", new Lab4.Classes.Date(25, 1, 2001));
            library.HireStaff(librarian, 11000);

            var book = new Book("Пригоди капiтана Блада",
                new Writer("Рафаель", "Сабатiнi", "", new Lab4.Classes.Date(29, 4, 1875)), 1922);


            librarian.AddBook(book);


            mockNotifier.Verify(n => n.NotifyObservers(book, librarian), Times.Once);
        }

        [Test]
        public void TestStaffManager()
        {
            library = Library.Singleton(mockStaffManager.Object, mockNotifier.Object);
            librarian = new Librarian("Олександр", "Овсієнко", "Захарович", new Lab4.Classes.Date(7, 3, 1999));

            library.HireStaff(librarian, 15000);

            mockStaffManager.Verify(sm => sm.HireStaff(librarian, library.Staff, 15000), Times.Once);
        }

        [Test]
        public void TestSingleton()
        {
            var library1 = Library.Singleton(new StaffManager(), new WhistleblowerSMS());
            var library2 = Library.Singleton(new StaffManager(), new WhistleblowerSMS());
            Assert.That(library1, Is.SameAs(library2));
        }

        [Test]
        public void TestRegisterObserverAndRemovingObserver()
        {
            library = Library.Singleton(mockStaffManager.Object, mockNotifier.Object);
            librarian = new Librarian("Гордієнко", "Євгеній", "Тарасович", new Lab4.Classes.Date(11, 9, 2005));
            library.HireStaff(librarian, 13000);
            mockStaffManager.Verify(sm => sm.HireStaff(librarian, library.Staff, 13000), Times.Once);
            library.FireAnStaff(librarian);
            mockStaffManager.Verify(sm => sm.FireAnStaff(librarian, library.Staff), Times.Once);
        }
        [TearDown]
        public void ResetSingleton()
        {
            typeof(Library)
                .GetField("instance", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(null, null);
        }
    }
}