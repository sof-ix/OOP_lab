using System;
using System.Text;
using TransportLibrary;

namespace Lab10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            TransportVehicle[] vehicles = CreateArray(6);

            Part1(vehicles);
            Part2(vehicles);
            Part3(vehicles);

            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();
        }
        static TransportVehicle[] CreateArray(int count)
        {
            TransportVehicle[] array = new TransportVehicle[count];
            for (int i = 0; i < count; i++)
            {
                TransportVehicle v;
                if (i % 3 == 0) v = new Car();
                else if (i % 3 == 1) v = new Train();
                else v = new Express();

                v.RandomInit();
                array[i] = v;
            }
            return array;
        }

        static void ShowArray(TransportVehicle[] array)
        {
            foreach (TransportVehicle v in array)
            {
                v.Show();
            }
            Console.WriteLine();
        }

        static void Part1(TransportVehicle[] vehicles)
        {
            Console.WriteLine("===== ЧАСТЬ 1. Иерархия классов =====");
            Console.WriteLine();

            Console.WriteLine("--- Конструкторы и метод эквивалентости ---");
            Car c1 = new Car("Lada", 100, 5);
            Car c2 = new Car(c1);                 
            Car c3 = new Car("BMW", 300, 4);
            Console.WriteLine("c1 равен c2 (копия): " + c1.Equals(c2));
            Console.WriteLine("c1 равен c3: " + c1.Equals(c3));
            Console.WriteLine();

            Console.WriteLine("--- Просмотр массива обычным методом ---");
            foreach (TransportVehicle v in vehicles)
            {
                v.ShowSimple();   
            }
            Console.WriteLine();

            Console.WriteLine("--- Просмотр массива виртуальным методом ---");
            ShowArray(vehicles);   
        }
        static void Part2(TransportVehicle[] vehicles)
        {
            Console.WriteLine("===== ЧАСТЬ 2. Запросы =====");
            Console.WriteLine();
            Console.WriteLine("Массив для запросов:");
            ShowArray(vehicles);

            Console.WriteLine("Введите вид транспорта (1 - автомобиль, 2 - поезд, 3 - экспресс):");
            int kind;
            int.TryParse(Console.ReadLine(), out kind);

            Console.WriteLine("Запрос 1. Количество транспортных средств: "
                + Queries.CountByKind(vehicles, kind));
            Console.WriteLine("Запрос 2. Средняя мощность выбранного вида, л.с.: "
                + Queries.AveragePower(vehicles, kind));
            Console.WriteLine("Запрос 3. Количество пассажиров во всех вагонах всех экспрессов: "
                + Queries.TotalExpressPassengers(vehicles));

            Car best = Queries.MostPowerfulCar(vehicles);
            if (best != null)
            {
                Console.WriteLine("Запрос 4. Самый мощный автомобиль: " + best.Name + ", " + best.Power + " л.с.");
            }
            else
            {
                Console.WriteLine("Запрос 4. Автомобилей в массиве нет");
            }
            Console.WriteLine();
        }

        static void Part3(TransportVehicle[] vehicles)
        {
            Console.WriteLine("===== ЧАСТЬ 3. Интерфейсы =====");
            Console.WriteLine();

            Console.WriteLine("--- Сортировка по мощности ---");
            Array.Sort(vehicles);
            ShowArray(vehicles);

            Console.WriteLine("--- Сортировка по названию ---");
            Array.Sort(vehicles, new SortByName());
            ShowArray(vehicles);

            Console.WriteLine("--- Поиск по названию ---");
            Console.WriteLine("Введите название для поиска (Enter - взять название среднего элемента):");
            string searchName = Console.ReadLine();
            if (string.IsNullOrEmpty(searchName))
            {
                searchName = vehicles[vehicles.Length / 2].Name;
                Console.WriteLine("Ищем: " + searchName);
            }
            Console.WriteLine("Линейный поиск, индекс: " + Search.FindByName(vehicles, searchName));
            Console.WriteLine("Бинарный поиск, индекс: " + Search.BinarySearchByName(vehicles, searchName));
            Console.WriteLine("(-1 означает, что элемент не найден; при одинаковых названиях индексы могут отличаться)");
            Console.WriteLine();

            Console.WriteLine("--- Массив элементов типа ---");
            IInit[] items = new IInit[4];
            items[0] = new Car();
            items[1] = new Train();
            items[2] = new Express();
            items[3] = new Driver();    

            foreach (IInit item in items)
            {
                item.RandomInit();
                ShowItem(item);
            }
            Console.WriteLine();

            Console.WriteLine("Ввести данные водителя с клавиатуры (y - да, иначе - нет):");
            string answer = Console.ReadLine();
            if (answer == "y")
            {
                items[3].Init();
                ShowItem(items[3]);
            }
            Console.WriteLine();

            Console.WriteLine("--- Присваивание, ShallowCopy() и Clone() ---");
            Car original = new Car("Lada", 100, 5);
            original.MainDriver.Name = "Иванов";
            original.MainDriver.Experience = 10;

            TransportVehicle assigned = original;                           
            TransportVehicle shallow = original.ShallowCopy();              
            TransportVehicle clone = (TransportVehicle)original.Clone();    

            original.Name = "Изменённое название";
            original.MainDriver.Name = "Изменённый водитель";

            Console.WriteLine("Оригинал:");
            original.Show();
            Console.WriteLine("Присваивание (та же ссылка - изменилось всё):");
            assigned.Show();
            Console.WriteLine("ShallowCopy (название осталось старым, но водитель общий - изменился):");
            shallow.Show();
            Console.WriteLine("Clone (полностью независимая копия - ничего не изменилось):");
            clone.Show();
            Console.WriteLine();
        }

        static void ShowItem(IInit item)
        {
            TransportVehicle v = item as TransportVehicle;
            if (v != null)
            {
                v.Show();
                return;
            }

            Driver d = item as Driver;
            if (d != null)
            {
                d.Show();
            }
        }
    }
}