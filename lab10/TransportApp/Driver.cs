using System;

namespace TransportLibrary
{
    public class Driver : IInit
    {
        private static Random rnd = new Random();
        private static string[] names = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов", "Попов" };

        private string name;
        private int experience;   

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrEmpty(value)) name = value;
                else name = "Неизвестен";
            }
        }

        public int Experience
        {
            get { return experience; }
            set
            {
                if (value >= 0 && value <= 60) experience = value;
                else experience = 0;
            }
        }

        public Driver()
        {
            name = "Неизвестен";
            experience = 0;
        }

        public Driver(string name, int experience)
        {
            Name = name;
            Experience = experience;
        }

        public Driver(Driver other)
        {
            name = other.name;
            experience = other.experience;
        }

        public void Show()
        {
            Console.WriteLine("Водитель: " + name + ", стаж: " + experience + " лет");
        }

        public void Init()
        {
            Console.WriteLine("Фамилия водителя:");
            Name = Console.ReadLine();
            Console.WriteLine("Стаж:");
            int e;
            int.TryParse(Console.ReadLine(), out e);   
            Experience = e;
        }

        public void RandomInit()
        {
            Name = names[rnd.Next(names.Length)];
            Experience = rnd.Next(0, 41);
        }

        public override bool Equals(object obj)
        {
            Driver other = obj as Driver;
            if (other == null) return false;
            return name == other.name && experience == other.experience;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ experience;
        }
    }
}