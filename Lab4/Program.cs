using Lab4.Classes;
using Lab4.Interfaces;
using System.Text.Json;
string configText = File.ReadAllText("D:\\Vodnik\\Python\\Laba4\\Lab4\\config.json");

var config = JsonSerializer.Deserialize<Dictionary<string, string>>(configText);

string notifireType = config!["NotifierType"];
Type type = Type.GetType(notifireType)!;

INotifier notifier = (INotifier)Activator.CreateInstance(type)!;

Library library = Library.Singleton(new StaffManager(),notifier); // Отримання екземпляру класу Library

Writer writer = new Writer("Тарас", "Шевченко", "Григорович", new Date(9, 3, 1814)); // Створення екземпляра класу Writer
writer.WriteBook(new Book("Кобзар", writer, 1840)); // Написання книги

Librarian librarian = new Librarian("Назарiй", "Зубар", "Вiкторович", new Date(13, 3, 2006)); // Створення екземпляра класу Librarian
library.HireStaff(librarian,50000); // Додавання бібліотекаря до списку в library
Librarian librarian1 = new Librarian("Петро", "Бондарчук", "Степанович", new Date(17, 7, 2008)); // Створення екземпляра класу Librarian
library.HireStaff(librarian1,30000); // Додавання бібліотекаря до списку в library

librarian.AddBook(writer.GetAllBooks()[0]); // Додавання книги до бібліотеки

/*Library library = Library.Singleton(new WhistleblowerSMS());
Librarian librarian = new Librarian("Назарiй", "Зубар", "Вiкторович", new Date(13, 3, 2006)); // Створення екземпляра класу Librarian
library.HireStaff(librarian, 50000); // Додавання бібліотекаря до списку в library

var librarianTest = new TestLibrarian();

librarianTest.Init();
librarianTest.AddBookTest();*/