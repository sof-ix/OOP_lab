using System;

namespace TransportLibrary
{
    public class Car : TransportVehicle
    {
        private static string[] carNames = { "Lada", "Toyota", "BMW", "Audi", "Kia" };

        private int seats;   

        public int Seats
        {
            get { return seats; }
            set
            {
                if (value >= 1 && value <= 9) seats = value;
                else seats = 1;
            }
        }

        public Car() : base()
        {
            seats = 1;
        }

        public Car(string name, int power, int seats) : base(name, power)
        {
            Seats = seats;
        }

        public Car(Car other) : base(other)
        {
            seats = other.seats;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("   Это автомобиль, мест: " + seats);
        }

        public new void ShowSimple()
        {
            Console.WriteLine("[обычный метод] Автомобиль: " + name + ", мест: " + seats);
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Количество мест:");
            int s;
            int.TryParse(Console.ReadLine(), out s);
            Seats = s;
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Name = carNames[rnd.Next(carNames.Length)];
            Power = rnd.Next(70, 401);
            Seats = rnd.Next(2, 8);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            Car other = (Car)obj;
            return seats == other.seats;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ seats;
        }
    }
}