using System;

namespace TransportLibrary
{
    public class Train : TransportVehicle
    {
        private static string[] trainNames = { "Пассажирский", "Грузовой", "Пригородный", "Почтовый" };

        protected int wagonCount;   

        public int WagonCount
        {
            get { return wagonCount; }
            set
            {
                if (value >= 1 && value <= 30) wagonCount = value;
                else wagonCount = 1;
            }
        }

        public Train() : base()
        {
            wagonCount = 1;
        }

        public Train(string name, int power, int wagonCount) : base(name, power)
        {
            WagonCount = wagonCount;
        }

        public Train(Train other) : base(other)
        {
            wagonCount = other.wagonCount;
        }

        public override void Show()
        {
            base.Show();
            Console.WriteLine("   Это поезд, вагонов: " + wagonCount);
        }

        public new void ShowSimple()
        {
            Console.WriteLine("[обычный метод] Поезд: " + name + ", вагонов: " + wagonCount);
        }

        public override void Init()
        {
            base.Init();
            Console.WriteLine("Количество вагонов:");
            int w;
            int.TryParse(Console.ReadLine(), out w);
            WagonCount = w;
        }

        public override void RandomInit()
        {
            base.RandomInit();
            Name = trainNames[rnd.Next(trainNames.Length)];
            Power = rnd.Next(1000, 5001);
            WagonCount = rnd.Next(3, 15);
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            Train other = (Train)obj;
            return wagonCount == other.wagonCount;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^ wagonCount;
        }
    }
}