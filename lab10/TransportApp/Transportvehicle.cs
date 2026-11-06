using System;

namespace TransportLibrary
{
   
    public class TransportVehicle : IInit, IComparable, ICloneable
    {
        protected static Random rnd = new Random();
        private static string[] names = { "Транспорт А", "Транспорт Б", "Транспорт В" };

        protected string name;     
        protected int power;       
        protected Driver driver;   

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrEmpty(value)) name = value;
                else name = "Без названия";
            }
        }

        public int Power
        {
            get { return power; }
            set
            {
                if (value > 0 && value <= 20000) power = value;
                else power = 1;
            }
        }

        public Driver MainDriver
        {
            get { return driver; }
            set
            {
                if (value != null) driver = value;
                else driver = new Driver();
            }
        }

        public TransportVehicle()
        {
            name = "Без названия";
            power = 1;
            driver = new Driver();
        }

        public TransportVehicle(string name, int power)
        {
            Name = name;
            Power = power;
            driver = new Driver();
        }

        public TransportVehicle(TransportVehicle other)
        {
            name = other.name;
            power = other.power;
            driver = new Driver(other.driver);   
        }
        public virtual void Show()
        {
            Console.WriteLine("Транспортное средство: " + name + ", мощность: " + power
                + " л.с., водитель: " + driver.Name + " (стаж " + driver.Experience + " лет)");
        }

        public void ShowSimple()
        {
            Console.WriteLine("[обычный метод] Транспортное средство: " + name);
        }

        public virtual void Init()
        {
            Console.WriteLine("Название:");
            Name = Console.ReadLine();
            Console.WriteLine("Мощность (л.с.):");
            int p;
            int.TryParse(Console.ReadLine(), out p);   
            Power = p;
            Console.WriteLine("Данные водителя:");
            driver.Init();
        }

        public virtual void RandomInit()
        {
            Name = names[rnd.Next(names.Length)];
            Power = rnd.Next(50, 501);
            driver.RandomInit();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType()) return false;
            TransportVehicle other = (TransportVehicle)obj;
            return name == other.name && power == other.power;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ power;
        }
        public int CompareTo(object obj)
        {
            TransportVehicle other = (TransportVehicle)obj; 
            return power.CompareTo(other.power);
        }

        public TransportVehicle ShallowCopy()
        {
            return (TransportVehicle)this.MemberwiseClone();
        }

        public object Clone()
        {
            TransportVehicle copy = (TransportVehicle)this.MemberwiseClone();
            copy.driver = new Driver(this.driver);   // водителя копируем отдельно
            return copy;
        }
    }
}