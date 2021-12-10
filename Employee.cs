using System;
using System.Collections;


namespace Homework_7._8
{
    public struct Employee : IEnumerable
    {
        /// <summary>
        /// инкапсулированные данные
        /// </summary>
        private int id;
        private DateTime recdate;
        private DateTime rectime;
        private string fio;
        private int age;
        private int height;
        private DateTime dob;
        private string homeTown;

        public DateTime DoB { get => dob; set => dob = value; }
        public string HomeTown { get => homeTown; set => homeTown = value; }
        public int Height { get => height; set => height = value; }
        public int Age { get => age; set => age = value; }
        public string Fio { get => fio; set => fio = value; }
        public DateTime RecDate { get => recdate; set => recdate = value; }
        public DateTime RecTime { get => rectime; set => rectime = value; }
        public int Id { get => id; set => id = value; }

        /// <summary>
        /// Метод создания
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recdate"></param>
        /// <param name="fio"></param>
        /// <param name="age"></param>
        /// <param name="height"></param>
        /// <param name="dob"></param>
        /// <param name="homeTown"></param>
        public Employee(int id, DateTime recdate, DateTime rectime, string fio, int age, int height, DateTime dob, string homeTown)
        {
            this.id = id;
            this.recdate = recdate;
            this.rectime = rectime;
            this.fio = fio;
            this.age = age;
            this.height = height;
            this.dob = dob;
            this.homeTown = homeTown;
        }

        /// <summary>
        /// Метод чтения данных
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetField(int i)//Извлекаем данные
        {
            if (i == 0) return id.ToString();
            if (i == 1) return recdate.ToString();
            if (i == 2) return rectime.ToString();
            if (i == 3) return fio;
            if (i == 4) return age.ToString();
            if (i == 5) return height.ToString();
            if (i == 6) return dob.ToString();
            if (i == 7) return homeTown;
            return "";
        }
        /// <summary>
        /// Метод записи
        /// </summary>
        /// <param name="i"></param>
        /// <param name="str"></param>
        public void SetField(int i, string str)//Помещаем данные
        {
            if (i == 0) id = int.Parse(str);
            else if (i == 1) recdate = DateTime.Parse(str);
            else if (i == 2) rectime = DateTime.Parse(str);
            else if (i == 3) fio = str;
            else if (i == 4) age = int.Parse(str);
            else if (i == 5) height = int.Parse(str);
            else if (i == 6) dob = DateTime.Parse(str);
            else if (i == 7) homeTown = str;
        }

        /// <summary>
        /// Метод вывода данных на экран
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            return $"id: {id} Дата записи: " +
                $"{recdate.ToShortDateString(), -6}" +
                $" Время записи: {rectime.ToShortTimeString(),-6}" +
                $" ФИО: {fio, -30}" +
                $" Возраст: {age, -6}" +
                $" Дата рождения {dob.ToShortDateString(), -6}" +
                $" Место рождения {homeTown, -6}";
        }
        /// <summary>
        /// Метод возврата данных для записи в файл
        /// </summary>
        /// <returns></returns>
        public string ReturnValues()
        {
            return $"{id}#{recdate.ToShortDateString()}#{rectime.ToShortTimeString()}#{fio}#{age}#{height}#{dob.ToShortDateString()}#{homeTown}";
        }
        /// <summary>
        /// хз зачем, в процессе игрался
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
