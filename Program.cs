using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using System.Collections.Generic;

namespace Homework_7._8
{
    class Program
    {

        /// <summary>
        /// Метод геренрации ID. Считывает последний из файла базы данных и увеличивает на 1.
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns></returns>
        static public int IdGenetrate(string path)
        {

            string[] str = File.ReadAllLines(path);
            if (str.Length == 0)
            {
                return 1;
            }
            else
            {
                String last = File.ReadLines(path).Last();
                string[] lastArr = last.Split(new char[] { '#' });
                int id = int.Parse(lastArr[0]);
                id++;
                return id;
            }
        }

        /// <summary>
        /// Метод получения списка обьектов структуры из файла
        /// </summary>
        /// <param name="FileName"></param>
        /// <returns></returns>
        public static List<Employee> GetStructList(string FileName)
        {
            int i = default;
            int RowsNumber = 8; //количество полей
            int count = File.ReadAllLines(FileName).Length; //количество строк в файле
            string[] arr = new string[count]; //массив строк из файла
            string[] words = new string[count];

            List<Employee> db = new();
            Employee emp = new();

            using (StreamReader sr = new(FileName, System.Text.Encoding.Default))
            {
                string line;
                //читаем файл до конца
                while ((line = sr.ReadLine()) != null)
                {
                    //заполнили массив строк из файла
                    arr[i] = line;
                    i++;
                }

                for (i = 0; i < count; i++)
                {
                    int j;
                    for (j = 0; j < RowsNumber; j++)
                    {
                        //разделяем строку по #
                        words = arr[i].Split(new char[] { '#' });
                        //присваиваем елементу структуры emp разделённые значения
                        emp.SetField(j, words[j]);
                    }
                    //добавляем новый элемент списка 
                    db.Add(emp);
                }
                sr.Close();
            }
            return db;
        }
        
        /// <summary>
        /// Метод вывода списка структур
        /// </summary>
        /// <param name="db"></param>
        /// <param name="count"></param>
        public static void DisplayList(List<Employee> db, int count)
        {
            for (int i = 0; i < count - 1; i++)
            {
                Console.Write($"{db[i].Print()}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Метод перезаписи файла с изменениями. 
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="data">Список</param>
        static public void WriteListToFile(string filepath, List<Employee> data)
        {
            using (StreamWriter sw = new StreamWriter(filepath, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < data.Count; i++)
                {
                    sw.WriteLine(data[i].ReturnValues());
                }
                sw.Close();
            }
        }


        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            bool flag = false;
            bool flag1 = false;
            int Choice = 0;
            char key = 'y';
            string FileName = @"db.txt";

            List<Employee> db = new ();
            Employee emp = new();

            int id = 1;
            string input;

            DateTime MinDate;
            DateTime MaxDate;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("База данных «Сотрудники»");
            Console.ResetColor();
            Console.WriteLine($"Загружаем файл {FileName}:");

            //загружаем данные из файла, если его нет - создаём
            if (!File.Exists(FileName)) //проверка наличия файла
            {
                Console.WriteLine($"Файл {FileName} не существует.\nСоздаём файл {FileName}");
                //File.Create(FileName);

                Console.WriteLine($"Отсутствуют записи в файле {FileName}" +
                    $"Необходимо создать хотябы одну запись");
                Console.WriteLine($"Введите ФИО: ");
                string fio = $"{Console.ReadLine()}";

                //проверка возраста
                flag = default;
                int age = default;
                while (flag == false || age < 1 || age > 120)
                {
                    Console.WriteLine("Введите возраст (1 - 120): ");
                    input = Console.ReadLine();
                    flag = int.TryParse(input, out age);
                }

                //проверка роста
                flag = default;
                int height = default;
                while (flag == false || height < 10 || height > 230)
                {
                    Console.WriteLine("Введите рост (10 - 230): ");
                    input = Console.ReadLine();
                    flag = int.TryParse(input, out height);
                }

                //проверка даты рождения
                DateTime dob;
                string date;
                do
                {
                    Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):");
                    date = Console.ReadLine();
                }
                while (!DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out dob));

                Console.WriteLine($"Введите место рождения: ");
                string hometown = $"{Console.ReadLine()}";

                emp.Id = 1;
                emp.RecDate = DateTime.Now;
                emp.RecTime = DateTime.Now;
                emp.Fio = fio;
                emp.Age = age;
                emp.Height = height;
                emp.DoB = dob;
                emp.HomeTown = hometown;

                db.Add(emp);
                WriteListToFile(FileName, db);
                flag = false;
                Console.WriteLine();
            }
            else //считываем файл, записываем данные в структуру
            {
                int LinesCount1 = File.ReadAllLines(FileName).Length; //количество строк в файле
                db = GetStructList(FileName);
                Console.WriteLine($"Файл {FileName} загружен.\nКоличество записей {LinesCount1}");
                Console.ResetColor();
                Console.WriteLine();
            }

            int LinesCount = File.ReadAllLines(FileName).Length; //количество строк в файле
            
            do
            {
                while (flag == false || Choice < 1 || Choice > 7)
                {

                    Console.WriteLine("Выберите действие 1 - 7:");
                    Console.WriteLine("1) Просмотр записи");
                    Console.WriteLine("2) Создание записи");
                    Console.WriteLine("3) Удаление записи");
                    Console.WriteLine("4) Редактирование записи");
                    Console.WriteLine("5) Загрузка записей в выбранном диапазоне дат");
                    Console.WriteLine("6) Сортировка по возрастанию и убыванию даты");
                    Console.WriteLine("7) Выход из программы");

                    flag = int.TryParse(Console.ReadLine(), out Choice);

                    //Просмотр записи
                    if (Choice == 1)
                    {
                        while (flag1 == false)
                        {
                            Console.WriteLine($"Ведите номер записи для отображения от 1 до {LinesCount}:");
                            flag1 = int.TryParse(Console.ReadLine(), out id);
                        }
                        
                        Console.WriteLine(db[id - 1].Print());
                        flag = false;
                        flag1 = false;
                    }

                    //Создание записи
                    if (Choice == 2)
                    {
                        
                            Console.WriteLine($"Введите ФИО: ");
                            string fio = $"{Console.ReadLine()}";

                            //проверка возраста
                            flag = default;
                            int age = default;
                            while (flag == false || age < 1 || age > 120)
                            {
                                Console.WriteLine("Введите возраст (1 - 120): ");
                                input = Console.ReadLine();
                                flag = int.TryParse(input, out age);
                            }

                            //проверка роста
                            flag = default;
                            int height = default;
                            while (flag == false || height < 10 || height > 230)
                            {
                                Console.WriteLine("Введите рост (10 - 230): ");
                                input = Console.ReadLine();
                                flag = int.TryParse(input, out height);
                            }

                            //проверка даты рождения
                            DateTime dob;
                            string date;
                            do
                            {
                                Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):");
                                date = Console.ReadLine();
                            }
                            while (!DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out dob));

                            Console.WriteLine($"Введите место рождения: ");
                            string hometown = $"{Console.ReadLine()}";
                        
                        emp.Id = IdGenetrate(FileName);
                        emp.RecDate = DateTime.Now;
                        emp.RecTime = DateTime.Now;
                        emp.Fio = fio;
                        emp.Age = age;
                        emp.Height = height;
                        emp.DoB = dob;
                        emp.HomeTown = hometown;

                        db.Add(emp);
                        
                        flag = false;
                    }

                    //Удаление записи
                    if (Choice == 3)
                    {
                        while (flag1 == false)
                        {
                            Console.WriteLine($"Ведите номер записи для удаления от 1 до {LinesCount}:");
                            flag1 = int.TryParse(Console.ReadLine(), out id);
                        }
                        db.RemoveAt(id);
                        flag = false;
                        flag1 = false;
                    }

                    //Редактирование записи
                    if (Choice == 4)
                    {
                        while (flag1 == false)
                        {
                            Console.WriteLine($"Ведите номер записи для редактирования от 1 до {LinesCount}:");
                            flag1 = int.TryParse(Console.ReadLine(), out id);
                        }

                        Console.WriteLine($"Введите ФИО: ");
                        string fio = $"{Console.ReadLine()}";

                        //проверка возраста
                        flag = default;
                        int age = default;
                        while (flag == false || age < 1 || age > 120)
                        {
                            Console.WriteLine("Введите возраст (1 - 120): ");
                            input = Console.ReadLine();
                            flag = int.TryParse(input, out age);
                        }

                        //проверка роста
                        flag = default;
                        int height = default;
                        while (flag == false || height < 10 || height > 230)
                        {
                            Console.WriteLine("Введите рост (10 - 230): ");
                            input = Console.ReadLine();
                            flag = int.TryParse(input, out height);
                        }

                        //проверка даты рождения
                        DateTime dob;
                        string date;
                        do
                        {
                            Console.WriteLine("Введите дату рождения в формате дд.ММ.гггг (день.месяц.год):");
                            date = Console.ReadLine();
                        }
                        while (!DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out dob));

                        Console.WriteLine($"Введите место рождения: ");
                        string hometown = $"{Console.ReadLine()}";

                        emp.Id = id;
                        emp.RecDate = DateTime.Now;
                        emp.RecTime = DateTime.Now;
                        emp.Fio = fio;
                        emp.Age = age;
                        emp.Height = height;
                        emp.DoB = dob;
                        emp.HomeTown = hometown;

                        db[id - 1] = emp;
                        flag = false;
                    }

                    //Загрузка записей в выбранном диапазоне дат
                    if (Choice == 5)
                    {
                        //проверка минимальной даты записи
                        string date;
                        do
                        {
                            Console.WriteLine("Введите минимальную дату рождения в формате дд.ММ.гггг (день.месяц.год):");
                            date = Console.ReadLine();
                        }
                        while (!DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out MinDate));
                        flag = false;

                        //проверка максимальной даты записи
                        do
                        {
                            Console.WriteLine("Введите максимальную дату рождения в формате дд.ММ.гггг (день.месяц.год):");
                            date = Console.ReadLine();
                        }
                        while (!DateTime.TryParseExact(date, "dd.MM.yyyy", null, DateTimeStyles.None, out MaxDate));
                        flag = false;

                        for (int i = 0; i < LinesCount; i++)
                        {
                            if (db[i].DoB >= MinDate && db[i].DoB <= MaxDate)
                            {
                                Console.WriteLine(db[i].Print());
                            }
                        }
                    }

                    //Сортировка по возрастанию и убыванию даты
                    if (Choice == 6)
                    {
                        Console.WriteLine($"Сортировать по возрастанию дат - 1, по убыванию - 2");
                        while (flag == false || Choice < 1 || Choice > 2)
                        {
                            flag = int.TryParse(Console.ReadLine(), out Choice);
                        }
                        if (Choice == 1)
                        {
                            db.Sort((x, y) => x.RecDate.CompareTo(y.RecDate));
                            DisplayList(db, LinesCount);
                        } else
                        {
                            db.Sort((x, y) => y.RecDate.CompareTo(x.RecDate));
                            DisplayList(db, LinesCount);
                        }
                        flag = false;
                    }

                    //Выход из программы
                    if (Choice == 7)
                    {
                        Process.GetCurrentProcess().Kill();
                    }

                    Console.WriteLine($"Хотите записать изменения в файл {FileName}? y/n");
                    key = Char.ToLower(Console.ReadKey().KeyChar);
                    if (key == 'y')
                    {
                        WriteListToFile(FileName, db);
                    }

                    Console.WriteLine("Хотите продолжить? y/n");
                    key = Char.ToLower(Console.ReadKey().KeyChar);
                    if (key == 'n')
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                    Console.Clear();
                }
            } while (key != 'n');
        }
    }
}

