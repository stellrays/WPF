using System.Reflection.PortableExecutable;

using ConsoleAppEntityFrameworkCore.Model;

using static ConsoleAppEntityFrameworkCore.Model.User;
using (UserContext db = new UserContext())
{
    // получаем объекты из бд и выводим на консоль
    var users = db.Users.ToList();
    Console.WriteLine("Вывод данных:");
    foreach (User u in users)
    {
        Console.WriteLine($"ID\t Name\t Age\n" +
            $"{u.Id}\t {u.Name}\t {u.Age}");
    }
}