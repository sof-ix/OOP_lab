using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using TransportLibrary;

namespace TransportApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine();
                Console.WriteLine("===== ГЛАВНОЕ МЕНЮ =====");
                Console.WriteLine("1 - Задание 1 (коллекция Hashtable)");
                Console.WriteLine("2 - Задание 2 (обобщённая коллекция Queue<T>)");
                Console.WriteLine("3 - Задание 3 (время поиска в коллекциях)");
                Console.WriteLine("0 - Выход");
                int choice = ReadInt("Ваш выбор: ");

                if (choice == 1) Task1();
                else if (choice == 2) Task2();
                else if (choice == 3) Task3();
                else if (choice == 0) exit = true;
                else Console.WriteLine("Нет такого пункта меню");
            }
        }


        static int ReadInt(string text)
        {
            Console.Write(text);
            int value;
            if (!int.TryParse(Console.ReadLine(), out value)) value = -1;
            return value;
        }

        static TransportVehicle CreateVehicle(int kind)
        {
            if (kind == 1) return new Car();
            if (kind == 2) return new Train();
            if (kind == 3) return new Express();
            return null;
        }


        static TransportVehicle ReadNewVehicle()
        {
            int kind = ReadInt("Вид (1 - автомобиль, 2 - поезд, 3 - экспресс): ");
            TransportVehicle v = CreateVehicle(kind);
            if (v == null)
            {
                Console.WriteLine("Неверный вид");
                return null;
            }

            int how = ReadInt("Заполнение (1 - случайно, 2 - вручную): ");
            if (how == 2) v.Init();
            else v.RandomInit();
            return v;
        }


        static void Task1()
        {

            Hashtable table = new Hashtable();
            for (int i = 0; i < 6; i++)
            {
                TransportVehicle v = CreateVehicle(i % 3 + 1);
                v.RandomInit();
                HashtableTasks.Add(table, v);
            }

            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("--- Задание 1: Hashtable ---");
                Console.WriteLine("1 - Показать коллекцию (foreach)");
                Console.WriteLine("2 - Добавить объект");
                Console.WriteLine("3 - Удалить объект по ключу");
                Console.WriteLine("4 - Запросы");
                Console.WriteLine("5 - Клонирование коллекции");
                Console.WriteLine("6 - Сортировка и поиск");
                Console.WriteLine("0 - Назад");
                int choice = ReadInt("Ваш выбор: ");

                if (choice == 1)
                {
                    ShowHashtable(table);
                }
                else if (choice == 2)
                {
                    TransportVehicle v = ReadNewVehicle();
                    if (v != null)
                    {
                        int key = HashtableTasks.Add(table, v);
                        Console.WriteLine("Добавлено, ключ: " + key);
                    }
                }
                else if (choice == 3)
                {
                    int key = ReadInt("Ключ удаляемого объекта: ");
                    if (HashtableTasks.Remove(table, key)) Console.WriteLine("Удалено");
                    else Console.WriteLine("Объекта с таким ключом нет");
                }
                else if (choice == 4) Task1Queries(table);
                else if (choice == 5) Task1Clone(table);
                else if (choice == 6) Task1SortSearch(table);
                else if (choice == 0) back = true;
                else Console.WriteLine("Нет такого пункта меню");
            }
        }

        static void ShowHashtable(Hashtable table)
        {
            Console.WriteLine("Элементов в коллекции: " + table.Count);
            foreach (DictionaryEntry entry in table)
            {
                Console.WriteLine("Ключ " + entry.Key + ":");
                ((TransportVehicle)entry.Value).Show();
            }
        }

        static void Task1Queries(Hashtable table)
        {
            int kind = ReadInt("Вид (1 - автомобиль, 2 - поезд (включая экспрессы), 3 - экспресс): ");

            Console.WriteLine("Запрос 1. Количество элементов этого вида: " + HashtableTasks.CountByKind(table, kind));

            Console.WriteLine("Запрос 2. Элементы этого вида:");
            foreach (TransportVehicle v in HashtableTasks.GetByKind(table, kind))
            {
                v.Show();
            }

            Console.WriteLine("Запрос 3. Средняя мощность элементов этого вида: " + HashtableTasks.AveragePower(table, kind));
        }

        static void Task1Clone(Hashtable table)
        {
            if (table.Count == 0)
            {
                Console.WriteLine("Коллекция пуста");
                return;
            }

            Hashtable shallow = HashtableTasks.ShallowClone(table);
            Hashtable deep = HashtableTasks.DeepClone(table);
            Console.WriteLine("Элементов: оригинал " + table.Count + ", поверхностный клон " + shallow.Count
                + ", глубокий клон " + deep.Count);


            int key = 0;
            foreach (int k in table.Keys)
            {
                key = k;
                break;
            }
            TransportVehicle original = (TransportVehicle)table[key];
            string oldName = original.Name;
            original.Name = "ИЗМЕНЕНО";

            Console.WriteLine("Название в оригинале: " + ((TransportVehicle)table[key]).Name);
            Console.WriteLine("Название в поверхностном клоне: " + ((TransportVehicle)shallow[key]).Name
                + "  (объект общий - изменился)");
            Console.WriteLine("Название в глубоком клоне: " + ((TransportVehicle)deep[key]).Name
                + "  (независимая копия - не изменился)");

            original.Name = oldName;
        }

        static void Task1SortSearch(Hashtable table)
        {
            Console.WriteLine("Сортировка по мощности (IComparable):");
            foreach (TransportVehicle v in HashtableTasks.GetSortedByPower(table))
            {
                Console.WriteLine("  " + v.Name + " - " + v.Power + " л.с.");
            }

            ArrayList byName = HashtableTasks.GetSortedByName(table);
            Console.WriteLine("Сортировка по названию (IComparer):");
            foreach (TransportVehicle v in byName)
            {
                Console.WriteLine("  " + v.Name);
            }

            Console.Write("Введите название для поиска: ");
            string name = Console.ReadLine();

            int key = HashtableTasks.FindKeyByName(table, name);
            if (key == -1) Console.WriteLine("Линейный поиск: объект не найден");
            else Console.WriteLine("Линейный поиск: найден, ключ " + key);


            int index = byName.BinarySearch(new TransportVehicle(name, 1), new SortByName());
            if (index >= 0) Console.WriteLine("Бинарный поиск: найден, позиция " + index);
            else Console.WriteLine("Бинарный поиск: объект не найден");
        }


        static void Task2()
        {
            Queue<TransportVehicle> queue = new Queue<TransportVehicle>();
            for (int i = 0; i < 6; i++)
            {
                TransportVehicle v = CreateVehicle(i % 3 + 1);
                v.RandomInit();
                QueueTasks.Add(queue, v);
            }

            bool back = false;
            while (!back)
            {
                Console.WriteLine();
                Console.WriteLine("--- Задание 2: Queue<T> ---");
                Console.WriteLine("1 - Показать очередь (foreach)");
                Console.WriteLine("2 - Добавить объект в конец очереди");
                Console.WriteLine("3 - Удалить объект из начала очереди");
                Console.WriteLine("4 - Запросы");
                Console.WriteLine("5 - Клонирование очереди");
                Console.WriteLine("6 - Сортировка и поиск");
                Console.WriteLine("0 - Назад");
                int choice = ReadInt("Ваш выбор: ");

                if (choice == 1)
                {
                    ShowQueue(queue);
                }
                else if (choice == 2)
                {
                    TransportVehicle v = ReadNewVehicle();
                    if (v != null)
                    {
                        QueueTasks.Add(queue, v);
                        Console.WriteLine("Добавлено в конец очереди");
                    }
                }
                else if (choice == 3)
                {
                    TransportVehicle removed = QueueTasks.RemoveFirst(queue);
                    if (removed == null)
                    {
                        Console.WriteLine("Очередь пуста");
                    }
                    else
                    {
                        Console.WriteLine("Удалён объект из начала очереди:");
                        removed.Show();
                    }
                }
                else if (choice == 4) Task2Queries(queue);
                else if (choice == 5) Task2Clone(queue);
                else if (choice == 6) Task2SortSearch(queue);
                else if (choice == 0) back = true;
                else Console.WriteLine("Нет такого пункта меню");
            }
        }

        static void ShowQueue(Queue<TransportVehicle> queue)
        {
            Console.WriteLine("Элементов в очереди: " + queue.Count + " (первый в списке - начало очереди)");
            foreach (TransportVehicle v in queue)
            {
                v.Show();
            }
        }

        static void Task2Queries(Queue<TransportVehicle> queue)
        {
            int kind = ReadInt("Вид (1 - автомобиль, 2 - поезд (включая экспрессы), 3 - экспресс): ");

            Console.WriteLine("Запрос 1. Количество элементов этого вида: " + QueueTasks.CountByKind(queue, kind));

            Console.WriteLine("Запрос 2. Элементы этого вида:");
            foreach (TransportVehicle v in QueueTasks.GetByKind(queue, kind))
            {
                v.Show();
            }

            Console.WriteLine("Запрос 3. Пассажиров во всех вагонах всех экспрессов: " + QueueTasks.TotalExpressPassengers(queue));
        }

        static void Task2Clone(Queue<TransportVehicle> queue)
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("Очередь пуста");
                return;
            }

            Queue<TransportVehicle> shallow = QueueTasks.ShallowCopy(queue);
            Queue<TransportVehicle> deep = QueueTasks.DeepCopy(queue);
            Console.WriteLine("Элементов: оригинал " + queue.Count + ", поверхностная копия " + shallow.Count
                + ", глубокая копия " + deep.Count);


            TransportVehicle first = queue.Peek();
            string oldName = first.Name;
            first.Name = "ИЗМЕНЕНО";

            Console.WriteLine("Название первого в оригинале: " + queue.Peek().Name);
            Console.WriteLine("Название первого в поверхностной копии: " + shallow.Peek().Name
                + "  (объект общий - изменился)");
            Console.WriteLine("Название первого в глубокой копии: " + deep.Peek().Name
                + "  (независимая копия - не изменился)");

            first.Name = oldName;
        }

        static void Task2SortSearch(Queue<TransportVehicle> queue)
        {
            Console.WriteLine("Сортировка по мощности (IComparable):");
            foreach (TransportVehicle v in QueueTasks.GetSortedByPower(queue))
            {
                Console.WriteLine("  " + v.Name + " - " + v.Power + " л.с.");
            }

            Queue<TransportVehicle> byName = QueueTasks.GetSortedByName(queue);
            Console.WriteLine("Сортировка по названию (IComparer):");
            foreach (TransportVehicle v in byName)
            {
                Console.WriteLine("  " + v.Name);
            }

            Console.Write("Введите название для поиска: ");
            string name = Console.ReadLine();

            TransportVehicle found = QueueTasks.FindByName(queue, name);
            if (found == null) Console.WriteLine("Линейный поиск: объект не найден");
            else Console.WriteLine("Линейный поиск: найден, мощность " + found.Power + " л.с.");

            int index = QueueTasks.BinarySearchByName(byName, name);
            if (index >= 0) Console.WriteLine("Бинарный поиск: найден, позиция " + index);
            else Console.WriteLine("Бинарный поиск: объект не найден");
        }

        static void Task3()
        {
            TestCollections test = new TestCollections(1000);
            Console.WriteLine("Создано элементов в каждой коллекции:");
            Console.WriteLine("  List<Car>: " + test.Collection1.Count);
            Console.WriteLine("  List<string>: " + test.Collection1Str.Count);
            Console.WriteLine("  Dictionary<TransportVehicle, Car>: " + test.Collection2.Count);
            Console.WriteLine("  Dictionary<string, Car>: " + test.Collection2Str.Count);
            Console.WriteLine();

            Car first = test.GetFirst();
            Car middle = test.GetMiddle();
            Car last = test.GetLast();
            Car missing = new Car("Нет в коллекции", 1, 1);


            MeasureAll(test, "", first, false);

            Console.WriteLine("Частота таймера: " + Stopwatch.Frequency + " тиков в секунду");
            Console.WriteLine();
            MeasureAll(test, "Первый элемент", first, true);
            MeasureAll(test, "Центральный элемент", middle, true);
            MeasureAll(test, "Последний элемент", last, true);
            MeasureAll(test, "Элемент, которого нет в коллекции", missing, true);

            Console.WriteLine("--- Добавление и удаление ---");
            Car extra = new Car("Новый автомобиль", 150, 4);
            Console.WriteLine("Добавление: " + test.Add(extra) + ", элементов: " + test.Collection1.Count);
            Console.WriteLine("Повторное добавление: " + test.Add(extra) + " (такой ключ уже есть)");
            Console.WriteLine("Удаление: " + test.Remove(extra) + ", элементов: " + test.Collection1.Count);
            Console.WriteLine("Повторное удаление: " + test.Remove(extra) + " (элемента уже нет)");
        }


        static void MeasureAll(TestCollections test, string title, Car car, bool show)
        {
            string text = car.ToString();
            TransportVehicle key = car.BaseVehicle;
            string keyText = key.ToString();

            bool f1, f2, f3, f4, f5;
            long t1 = test.TimeContainsInList(car, out f1);
            long t2 = test.TimeContainsInStringList(text, out f2);
            long t3 = test.TimeContainsKey(key, out f3);
            long t4 = test.TimeContainsKeyString(keyText, out f4);
            long t5 = test.TimeContainsValue(car, out f5);

            if (!show) return;

            Console.WriteLine(title + ":");
            PrintResult("List<Car>.Contains", t1, f1);
            PrintResult("List<string>.Contains", t2, f2);
            PrintResult("Dictionary<TKey,Car>.ContainsKey", t3, f3);
            PrintResult("Dictionary<string,Car>.ContainsKey", t4, f4);
            PrintResult("Dictionary<TKey,Car>.ContainsValue", t5, f5);
            Console.WriteLine();
        }

        static void PrintResult(string what, long ticks, bool found)
        {
            Console.WriteLine("  " + what.PadRight(38) + ticks + " тиков, найден: " + found);
        }
    }
}