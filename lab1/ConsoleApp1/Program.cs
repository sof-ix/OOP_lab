using System;

namespace Lab1Variant12
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1. Задание 1");
                Console.WriteLine("2. Задание 2");
                Console.WriteLine("3. Задание 3");
                Console.WriteLine("0. Выход");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Введите корректное число!");
                    continue;
                }

                if (choice == 1)
                {
                    new FirstTask();
                }
                else if (choice == 2)
                {
                    new SecondTask();
                }
                else if (choice == 3)
                {
                    new ThirdTask();
                }
                else if (choice == 0)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Нет такого значения");
                }
            }
        }
    }

    class FirstTask
    {
        public FirstTask()
        {
            Init();
        }

        void Init()
        {
            while (true)
            {
                Console.Write("Введите значение переменной N: ");
                if (!int.TryParse(Console.ReadLine(), out int n))
                {
                    Console.WriteLine("Ошибка");
                    continue;
                }
                Console.Write("Введите значение переменной M: ");
                if (!int.TryParse(Console.ReadLine(), out int m))
                {
                    Console.WriteLine("Ошибка!");
                    continue;
                }

                Console.WriteLine("n++*m: " + First(n, m));
                Console.WriteLine("m--<n: " + Second(m, n));
                Console.WriteLine("++m>n: " + Third(m, n));

                double[] xValues = { -2, -1, -0.5, 0, 1 };
                foreach (double x in xValues)
                {
                    Console.WriteLine("x=" + x + " -> " + Fourth(x));
                }

                return;
            }
        }

        int First(int n, int m)
        {
            return n++ * m;
        }

        bool Second(int m, int n)
        {
            return m-- < n;
        }

        bool Third(int m, int n)
        {
            return ++m > n;
        }

        string Fourth(double x)
        {
            double podKornem = x + Math.Pow(Math.Abs(x), 0.25);
            if (podKornem < 0)
            {
                return "ошибка";
            }
            return (Math.Sqrt(podKornem) + Math.Abs(x)).ToString();
        }
    }

    class SecondTask
    {
        public SecondTask()
        {
            Init();
        }

        void Init()
        {
            while (true)
            {
                Console.Write("Введите значение переменной X1: ");
                if (!double.TryParse(Console.ReadLine(), out double x1))
                {
                    Console.WriteLine("Ошибка");
                    continue;
                }
                Console.Write("Введите значение переменной Y1: ");
                if (!double.TryParse(Console.ReadLine(), out double y1))
                {
                    Console.WriteLine("Ошибка");
                    continue;
                }

                bool belongs = IsInsideArea(x1, y1);
                if (belongs)
                {
                    Console.WriteLine("Точка принадлежит заштрихованной области");
                }
                else
                {
                    Console.WriteLine("Точка не принадлежит заштрихованной области");
                }
                return;
            }
        }

        bool IsInsideArea(double x, double y)
        {
            bool insideCircle = x * x + y * y <= 1;
            bool rightQuarter = x * y <= 0;
            return insideCircle && rightQuarter;
        }
    }

    class ThirdTask
    {
        public float a = 1000f;
        public float b = 0.0001f;
        public double a2 = 1000;
        public double b2 = 0.0001;

        public ThirdTask()
        {
            Init();
        }

        void Init()
        {
            Console.WriteLine("Значение при типе данных float: " + CalculationFloat(a, b));
            Console.WriteLine("Значение при типе данных double: " + CalculationDouble(a2, b2));
        }

        float CalculationFloat(float a, float b)
        {
            float numerator = (float)Math.Pow(a + b, 4) - (float)Math.Pow(a, 4);
            float denominator = 6 * (float)Math.Pow(a, 2) * (float)Math.Pow(b, 2)
                               + 4 * a * (float)Math.Pow(b, 3)
                               + (float)Math.Pow(b, 4)
                               + 4 * (float)Math.Pow(a, 3) * b;
            return numerator / denominator;
        }

        double CalculationDouble(double a, double b)
        {
            double numerator = Math.Pow(a + b, 4) - Math.Pow(a, 4);
            double denominator = 6 * Math.Pow(a, 2) * Math.Pow(b, 2)
                                + 4 * a * Math.Pow(b, 3)
                                + Math.Pow(b, 4)
                                + 4 * Math.Pow(a, 3) * b;
            return numerator / denominator;
        }
    }
}