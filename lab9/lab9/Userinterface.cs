using System;

namespace Lab9
{
    public class UserInterface
    {
        public static void Print(string text)
        {
            Console.WriteLine(text);
        }
        public static int ReadInt(string message)
        {
            int number;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("Это не целое число. Попробуйте ещё раз: ");
            }
            return number;
        }

        public static Time ReadTime()
        {
            while (true)
            {
                int h = ReadInt("  Часы: ");
                int m = ReadInt("  Минуты: ");
                try
                {
                    return new Time(h, m);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                }
            }
        }

        public static Time[] ReadTimes(int size)
        {
            Time[] items = new Time[size];
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine("Элемент " + i + ":");
                items[i] = ReadTime();
            }
            return items;
        }

        public static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Часть 1 (вычитание минут)");
            Console.WriteLine("2 - Часть 2 (перегруженные операции)");
            Console.WriteLine("3 - Часть 3 (массив Time, минимальное значение)");
            Console.WriteLine("0 - Выход");
        }
    }
}