using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Lab5Var12
{
    class Program
    {
        public static Random rnd = new Random();

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            int choice;
            do
            {
                Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1. Работа с массивами");
                Console.WriteLine("2. Работа со строками");
                Console.WriteLine("3. Выход");
                choice = ReadInt();
                switch (choice)
                {
                    case 1:
                        ArrayTask.Show();
                        break;
                    case 2:
                        StringTask.Show();
                        break;
                    case 3:
                        Console.WriteLine("До свидания!");
                        break;
                    default:
                        Console.WriteLine("Нет такого значения!");
                        break;
                }
            } while (choice != 3);
        }

        public static int ReadInt(string message = "Ваш выбор: ", int min = int.MinValue, int max = int.MaxValue)
        {
            while (true)
            {
                Console.Write(message);
                try
                {
                    int number = Convert.ToInt32(Console.ReadLine());
                    if (number >= min && number <= max)
                        return number;
                    Console.WriteLine("Ошибка: число должно быть от " + min + " до " + max + "!");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: нужно ввести целое число!");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: число слишком большое!");
                }
            }
        }

        public static void Print(int[] a)
        {
            if (a == null || a.Length == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }
            for (int i = 0; i < a.Length; i++)
                Console.Write(a[i] + "\t");
            Console.WriteLine();
        }

        public static void Print(int[,] a)
        {
            if (a == null || a.Length == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                    Console.Write(a[i, j] + "\t");
                Console.WriteLine();
            }
        }

        public static void Print(int[][] a)
        {
            if (a == null || a.Length == 0)
            {
                Console.WriteLine("Массив пуст.");
                return;
            }
            for (int i = 0; i < a.Length; i++)
                Print(a[i]);
        }

        public static void Print(string s)
        {
            if (s == "")
                Console.WriteLine("Строка пуста.");
            else
                Console.WriteLine(s);
        }
    }

    class ArrayTask
    {
        public static void Show()
        {
            int choice;
            do
            {
                Console.WriteLine("\n=== РАБОТА С МАССИВАМИ ===");
                Console.WriteLine("1. Работа с двумерными массивами");
                Console.WriteLine("2. Работа с рваными массивами");
                Console.WriteLine("3. Назад");
                choice = Program.ReadInt();
                switch (choice)
                {
                    case 1:
                        TwoArray.Show();
                        break;
                    case 2:
                        CutArray.Show();
                        break;
                    case 3:
                        break;
                    default:
                        Console.WriteLine("Нет такого значения!");
                        break;
                }
            } while (choice != 4);
        }
    }
    class TwoArray
    {
        public static int[,] table;

        public static void Show()
        {
            int choice;
            do
            {
                Console.WriteLine("\n--- Двумерный массив ---");
                Console.WriteLine("1. Создать массив");
                Console.WriteLine("2. Напечатать массив");
                Console.WriteLine("3. Удалить столбец с номером К");
                Console.WriteLine("4. Назад");
                choice = Program.ReadInt();
                switch (choice)
                {
                    case 1:
                        CreateArray.Show();
                        break;
                    case 2:
                        Program.Print(table);
                        break;
                    case 3:
                        DeleteK.Show();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Нет такого значения!");
                        break;
                }
            } while (choice != 4);
        }
    }

    class CreateArray
    {
        public static void Show()
        {
            Console.WriteLine("1. Создать массив вручную");
            Console.WriteLine("2. Создать массив с помощью ДСЧ");
            Console.WriteLine("3. Назад");
            int choice = Program.ReadInt();
            switch (choice)
            {
                case 1:
                case 2:
                    int strings = Program.ReadInt("Введите количество строк (1-20): ", 1, 20);
                    int columns = Program.ReadInt("Введите количество столбцов (1-20): ", 1, 20);
                    TwoArray.table = new int[strings, columns];
                    for (int i = 0; i < strings; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            if (choice == 1)
                                TwoArray.table[i, j] = Program.ReadInt("Элемент [" + i + "," + j + "]: ");
                            else
                                TwoArray.table[i, j] = Program.rnd.Next(1, 10);
                        }
                    }
                    Program.Print(TwoArray.table);
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Нет такого значения!");
                    break;
            }
        }
    }

    class DeleteK
    {
        public static void Show()
        {
            if (TwoArray.table == null || TwoArray.table.Length == 0)
            {
                Console.WriteLine("Ошибка: массив пуст, удалять нечего!");
                return;
            }
            int columns = TwoArray.table.GetLength(1);
            int k = Program.ReadInt("Номер удаляемого столбца (1-" + columns + "): ");
            if (k < 1 || k > columns)
            {
                Console.WriteLine("Ошибка: столбца с номером " + k + " нет!");
                return;
            }
            RemoveColumn(ref TwoArray.table, k);
            Console.WriteLine("Результат:");
            Program.Print(TwoArray.table);
        }

        static void RemoveColumn(ref int[,] table, int k)
        {
            int strings = table.GetLength(0);
            int columns = table.GetLength(1);
            int[,] result = new int[strings, columns - 1];
            for (int i = 0; i < strings; i++)
            {
                int newJ = 0;
                for (int j = 0; j < columns; j++)
                {
                    if (j != k - 1)
                    {
                        result[i, newJ] = table[i, j];
                        newJ++;
                    }
                }
            }
            table = result;
        }
    }

    class CutArray
    {
        public static int[][] jag_arr;

        public static void Show()
        {
            int choice;
            do
            {
                Console.WriteLine("\n--- Рваный массив ---");
                Console.WriteLine("1. Создать массив");
                Console.WriteLine("2. Напечатать массив");
                Console.WriteLine("3. Добавить К строк в конец массива");
                Console.WriteLine("4. Назад");
                choice = Program.ReadInt();
                switch (choice)
                {
                    case 1:
                        CreateCutArray.Show();
                        break;
                    case 2:
                        Program.Print(jag_arr);
                        break;
                    case 3:
                        AddK.Show();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Нет такого значения!");
                        break;
                }
            } while (choice != 4);
        }

        public static void FillRow(int i, int way)
        {
            int columns = Program.ReadInt("Количество элементов в строке " + i + " (1-20): ", 1, 20);
            jag_arr[i] = new int[columns];
            for (int j = 0; j < columns; j++)
            {
                if (way == 1)
                    jag_arr[i][j] = Program.ReadInt("Элемент [" + i + "][" + j + "]: ");
                else
                    jag_arr[i][j] = Program.rnd.Next(0, 10);
            }
        }
    }

    class CreateCutArray
    {
        public static void Show()
        {
            Console.WriteLine("1. Создать массив вручную");
            Console.WriteLine("2. Создать массив с помощью ДСЧ");
            Console.WriteLine("3. Назад");
            int choice = Program.ReadInt();
            switch (choice)
            {
                case 1:
                case 2:
                    int strings = Program.ReadInt("Введите количество строк (1-20): ", 1, 20);
                    CutArray.jag_arr = new int[strings][];
                    for (int i = 0; i < strings; i++)
                        CutArray.FillRow(i, choice);
                    Program.Print(CutArray.jag_arr);
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Нет такого значения!");
                    break;
            }
        }
    }

    class AddK
    {
        public static void Show()
        {
            if (CutArray.jag_arr == null || CutArray.jag_arr.Length == 0)
            {
                Console.WriteLine("Ошибка: сначала создайте массив!");
                return;
            }
            int k = Program.ReadInt("Сколько строк добавить (1-10): ", 1, 10);
            Console.WriteLine("Как заполнить новые строки?");
            Console.WriteLine("1. Вручную");
            Console.WriteLine("2. С помощью ДСЧ");
            int way = Program.ReadInt("Ваш выбор: ", 1, 2);

            int oldLength = CutArray.jag_arr.Length;
            Array.Resize(ref CutArray.jag_arr, oldLength + k);
            for (int i = oldLength; i < CutArray.jag_arr.Length; i++)
                CutArray.FillRow(i, way);

            Console.WriteLine("Результат:");
            Program.Print(CutArray.jag_arr);
        }
    }

    class StringTask
    {
        public static string text = "";

        public static void Show()
        {
            int choice;
            do
            {
                Console.WriteLine("\n=== РАБОТА СО СТРОКАМИ ===");
                Console.WriteLine("1. Ввести строку");
                Console.WriteLine("2. Напечатать строку");
                Console.WriteLine("3. Удалить первое и последнее слово в каждом предложении");
                Console.WriteLine("4. Назад");
                choice = Program.ReadInt();
                switch (choice)
                {
                    case 1:
                        InputString.Show();
                        break;
                    case 2:
                        Program.Print(text);
                        break;
                    case 3:
                        DeleteWords.Show();
                        break;
                    case 4:
                        break;
                    default:
                        Console.WriteLine("Нет такого значения!");
                        break;
                }
            } while (choice != 4);
        }
    }

    class InputString
    {
        static string[] tests =
        {
            "В лесу родилась елочка! В лесу она росла. Зимой и летом стройная, зеленая была!",
            "Раз два три? Один два три четыре! Пять шесть семь.",
            "Привет мир! Мама мыла раму.",
            "Да. Нет!"
        };

        public static void Show()
        {
            Console.WriteLine("1. Ввести строку с клавиатуры");
            Console.WriteLine("2. Взять строку из тестового массива");
            Console.WriteLine("3. Назад");
            int choice = Program.ReadInt();
            switch (choice)
            {
                case 1:
                    Console.WriteLine("Введите строку (предложения заканчиваются на . ! ?):");
                    string s = Console.ReadLine();
                    if (s == null)
                        s = "";
                    s = s.Trim();
                    if (s == "")
                        Console.WriteLine("Ошибка: строка пуста!");
                    else if (!Regex.IsMatch(s, @"[.!?]$"))
                        Console.WriteLine("Ошибка: строка должна заканчиваться знаком . ! или ?");
                    else
                        StringTask.text = s;
                    break;
                case 2:
                    for (int i = 0; i < tests.Length; i++)
                        Console.WriteLine((i + 1) + ". " + tests[i]);
                    int number = Program.ReadInt("Номер строки: ", 1, tests.Length);
                    StringTask.text = tests[number - 1];
                    break;
                case 3:
                    break;
                default:
                    Console.WriteLine("Нет такого значения!");
                    break;
            }
        }
    }

    class DeleteWords
    {
        public static void Show()
        {
            if (StringTask.text == "")
            {
                Console.WriteLine("Ошибка: строка пуста!");
                return;
            }
            string result = "";
            foreach (Match sentence in Regex.Matches(StringTask.text, @"[^.!?]+[.!?]"))
            {
                string part = CutSentence(sentence.Value);
                if (part != "")
                    result = result + part + " ";
            }
            result = result.Trim();
            if (result == "")
                Console.WriteLine("После обработки строка пуста.");
            else
                Console.WriteLine("Результат: " + result);
        }

        static string CutSentence(string sentence)
        {
            sentence = sentence.Trim();
            string end = sentence.Substring(sentence.Length - 1);
            string body = sentence.Substring(0, sentence.Length - 1);

            MatchCollection words = Regex.Matches(body, @"\w+");
            if (words.Count <= 2)
                return "";

            Match first = words[0];
            Match last = words[words.Count - 1];
            int start = first.Index + first.Length;

            string middle = body.Substring(start, last.Index - start);
            middle = middle.Trim(' ', ',', ';', ':');
            middle = Regex.Replace(middle, @"\s+", " ");

            return char.ToUpper(middle[0]) + middle.Substring(1) + end;
        }
    }
}