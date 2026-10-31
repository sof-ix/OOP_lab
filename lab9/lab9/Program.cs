using System;

namespace Lab9
{
    public class Program
    {
        public static Time FindMin(TimeArray array)
        {
            Time min = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                    min = array[i];
            }
            return min;
        }

        static void Part1()
        {
            UserInterface.Print("--- Часть 1 ---");
            Time t1 = new Time();
            Time t2 = new Time(2, 30);
            UserInterface.Print("t1 = " + t1.ToString() + " (конструктор без параметров)");
            UserInterface.Print("t2 = " + t2.ToString() + ", часы: " + t2.Hours + ", минуты: " + t2.Minutes);

            int minutes = UserInterface.ReadInt("Сколько минут вычесть из t2? ");
            Time r1 = Time.MinusMinutes(t2, minutes);
            Time r2 = t2.MinusMinutes(minutes);
            UserInterface.Print("Статическая функция: " + r1.ToString());
            UserInterface.Print("Метод класса: " + r2.ToString());
            UserInterface.Print("Создано объектов Time: " + Time.Count);
        }

        static void Part2()
        {
            UserInterface.Print("--- Часть 2 ---");
            UserInterface.Print("Введите время a:");
            Time a = UserInterface.ReadTime();
            UserInterface.Print("Введите время b:");
            Time b = UserInterface.ReadTime();

            UserInterface.Print("a = " + a.ToString() + ", b = " + b.ToString());

            Time zero = -a;
            UserInterface.Print("-a (обнуление) = " + zero.ToString());

            Time c = a;
            c--;
            UserInterface.Print("a-- (минус одна минута) = " + c.ToString());

            int h = a;
            UserInterface.Print("int(a) - количество часов = " + h);

            bool notZero = (bool)a;
            UserInterface.Print("bool(a) = " + notZero);

            UserInterface.Print("a == b: " + (a == b));
            UserInterface.Print("a != b: " + (a != b));
            UserInterface.Print("Создано объектов Time: " + Time.Count);
        }

        static void Part3()
        {
            UserInterface.Print("--- Часть 3 ---");

            TimeArray arr1 = new TimeArray();
            UserInterface.Print("Массив 1 (без параметров):");
            UserInterface.Print(arr1.View());

            int size = UserInterface.ReadInt("Сколько элементов в случайном массиве? ");
            TimeArray arr2 = new TimeArray(size);
            UserInterface.Print("Массив 2 (случайные значения):");
            UserInterface.Print(arr2.View());

            int n = UserInterface.ReadInt("Сколько элементов ввести с клавиатуры? ");
            TimeArray arr3 = new TimeArray(UserInterface.ReadTimes(n));
            UserInterface.Print("Массив 3 (ввод с клавиатуры):");
            UserInterface.Print(arr3.View());

            if (arr3.Length > 0)
            {
                Time min = FindMin(arr3);
                UserInterface.Print("Минимальное время в массиве 3: " + min.ToString());
            }
            UserInterface.Print("Создано объектов Time: " + Time.Count);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int choice;
            do
            {
                UserInterface.PrintMenu();
                choice = UserInterface.ReadInt("Ваш выбор: ");

                if (choice == 1)
                    Part1();
                else if (choice == 2)
                    Part2();
                else if (choice == 3)
                    Part3();
            }
            while (choice != 0);
        }
    }
}