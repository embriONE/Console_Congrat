using System;
using System.Linq;

namespace ConsoleCongratulator
{
    class Congratulator
    {
        AppContext db = new AppContext();
        public void ShowData(int numberOfPastDays = 0, int numberOfNextDays=0, string sortingMarker = "0")
        {
            if (numberOfPastDays == 0 & numberOfNextDays == 0)
            {
                Console.WriteLine("Укажите количество прошедших дней, за которые нужно получить данные");
                numberOfPastDays = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Укажите количество следующих дней, за которые нужно получить данные");
                numberOfNextDays = Convert.ToInt32(Console.ReadLine());
            }
            if (sortingMarker == "0")
            {
                Console.WriteLine("Укажите маркер сортировки");
                Console.WriteLine("1 - Сортировка по Id");
                Console.WriteLine("2 - сортировка по алфавиту");
                Console.WriteLine("3 - Сортировка по количеству оставшихся дней до дня рождения");
                sortingMarker = Console.ReadLine();
            }
            int currentDay = DateTime.Now.DayOfYear;

            int numberOfdaysInCurrentYear;
            if (DateTime.Now.Year % 4 == 0)
                numberOfdaysInCurrentYear = 366;
            else
                numberOfdaysInCurrentYear = 365;

            var data = db.People.Where(d => currentDay - d.BirthDate.DayOfYear <= numberOfPastDays &
                                            d.BirthDate.DayOfYear - currentDay <= numberOfNextDays);


            if (sortingMarker == "1")
            {
                data = data.OrderBy(d => d.Id);
            }
            else if (sortingMarker == "2")
            {
                data = data.OrderBy(d => d.Name);
            }
            else if (sortingMarker == "3")
            {
                data = data.OrderBy(d => d.BirthDate.DayOfYear >= currentDay ?
                                          d.BirthDate.DayOfYear - currentDay :
                d.BirthDate.DayOfYear - currentDay + numberOfdaysInCurrentYear);
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Ошибка ввода");
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            foreach (var d in data)
            {
                if (d.BirthDate.DayOfYear >= currentDay)
                {
                    if (d.BirthDate.DayOfYear - currentDay <= 5)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine($"{d.Id}.{d.Name,-20}\t{d.BirthDate.ToString("M"),-16}" +
                        $"\t Осталось дней до дня рождения:{d.BirthDate.DayOfYear - currentDay}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                        Console.WriteLine($"{d.Id}.{d.Name,-20}\t{d.BirthDate.ToString("M"),-16}" +
                        $"\t Осталось дней до дня рождения:{d.BirthDate.DayOfYear - currentDay}");
                }
                else if (d.BirthDate.DayOfYear < currentDay)
                {
                    if (currentDay - d.BirthDate.DayOfYear <= 5)
                    { 
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"{d.Id}.{d.Name,-20}\t{d.BirthDate.ToString("M"),-16}" +
                        $"\t Осталось дней до дня рождения:{d.BirthDate.DayOfYear - currentDay + numberOfdaysInCurrentYear}");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                        Console.WriteLine($"{d.Id}.{d.Name,-20}\t{d.BirthDate.ToString("M"),-16}" +
                        $"\t Осталось дней до дня рождения:{d.BirthDate.DayOfYear - currentDay + numberOfdaysInCurrentYear}");
                }
            } 
        }

        public void AddNewPerson()
        {
            Person newPersonAdded = new Person();

            Console.WriteLine("Введите имя для новой записи");
            newPersonAdded.Name = Console.ReadLine();

            Console.WriteLine("Введите день рождения для новой записи в формате дд.мм");
            newPersonAdded.BirthDate = Convert.ToDateTime(Console.ReadLine());

            db.Add(newPersonAdded);
            db.SaveChanges();

            Console.WriteLine("Новая запись добавлена");
            Console.WriteLine($"Id: {newPersonAdded.Id}\tИмя: {newPersonAdded.Name}" +
                        $"\tДень рождения: {newPersonAdded.BirthDate.ToString("M")}");
        }

        public void EditPerson()
        {
            int idOfEditedPerson;
            Console.WriteLine("Введите Id записи, которую нужно редактировать");
            idOfEditedPerson = Convert.ToInt32(Console.ReadLine());

            Person newPerson = new Person();

            Console.WriteLine("Введите новое имя для записи");
            newPerson.Name = Console.ReadLine();

            Console.WriteLine("Введите день рождения для новой записи в формате дд.мм");
            newPerson.BirthDate = Convert.ToDateTime(Console.ReadLine());

            Person editedPerson = db.People.First(p => p.Id == idOfEditedPerson);

            editedPerson.Name = newPerson.Name;
            editedPerson.BirthDate = newPerson.BirthDate;
            db.SaveChanges();
            Console.WriteLine($"Данные успешно изменены\nId: {editedPerson.Id}\tИмя: {editedPerson.Name}\tДень рождения:" +
                                                                                 $" {editedPerson.BirthDate.ToString("M")}");
        }

        public void DeletePerson()
        {
            int idOfDeletedPerson;
            Console.WriteLine("Введите Id записи, которую нужно удалить");
            idOfDeletedPerson = Convert.ToInt32(Console.ReadLine());

            Person deletedPerson = db.People.First(p => p.Id == idOfDeletedPerson);
            db.Remove(deletedPerson);
            db.SaveChanges();
            Console.WriteLine($"Запись {idOfDeletedPerson}.{deletedPerson.Name} удалена.");
        }
    }
}
