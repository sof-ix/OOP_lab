using System;

namespace TransportLibrary
{
    public class Express : Train
    {
        private static string[] expressNames = { "Сапсан", "Ласточка", "Стриж", "Аллегро" };

        private int passengersPerWagon;   

        public int PassengersPerWagon
        {
            get { return passengersPerWagon; }
            set
            {
                if (value >= 1 && value <= 150) passengersPerWagon = value;
                else passengersPerWagon = 1;
            }
        }

        public int TotalPassengers
        {
            get { return wagonCount * passengersPerWagon; }
        }

        public Express() : base()
        {
            passengersPerWagon = 1;
        }

        public Express(string name, int power, int wagonCount, int passengersPerWagon)
            : base(name, power, wagonCount)
        {
            PassengersPerWagon = passengersPerWagon;
        }

        public Express(Express other) : base(other)
        {
            passengersPerWagon = other.passengersPerWagon;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("   Это экспресс, пассажиров в вагоне: " + passengersPerWagon
                + ", всего пассажиров: " + TotalPassengers);
        }

        public new void ShowSimple()
        {
            Console.WriteLine("[обычный метод] Экспресс: " + name + ", вагонов: " + wagonCount
                + ", пассажиров всего: " + TotalPassengers);
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Пассажиров в одном вагоне:");
            int p;
            int.TryParse(Console.ReadLine(), out p);
            PassengersPerWagon = p;
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Name = expressNames[rnd.Next(expressNames.Length)];
            Power = rnd.Next(3000, 9001);
            PassengersPerWagon = rnd.Next(40, 101);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            Express other = (Express)obj;
            return passengersPerWagon == other.passengersPerWagon;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ passengersPerWagon;
        }
    }
}