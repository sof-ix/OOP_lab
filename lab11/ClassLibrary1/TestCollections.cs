using System.Collections.Generic;
using System.Diagnostics;

namespace TransportLibrary
{
    // Задание 3. Четыре коллекции с одинаковыми элементами и измерение времени поиска.
    
    public class TestCollections
    {
        private List<Car> collection1;                          
        private List<string> collection1Str;                    
        private Dictionary<TransportVehicle, Car> collection2;  
        private Dictionary<string, Car> collection2Str;         

        public List<Car> Collection1 { get { return collection1; } }
        public List<string> Collection1Str { get { return collection1Str; } }
        public Dictionary<TransportVehicle, Car> Collection2 { get { return collection2; } }
        public Dictionary<string, Car> Collection2Str { get { return collection2Str; } }

        
        public TestCollections(int count)
        {
            collection1 = new List<Car>();
            collection1Str = new List<string>();
            collection2 = new Dictionary<TransportVehicle, Car>();
            collection2Str = new Dictionary<string, Car>();

            for (int i = 0; i < count; i++)
            {
                Car car = new Car();
                car.RandomInit();
                car.Name = car.Name + " №" + i;   
                Add(car);
            }
        }

        public bool Add(Car car)
        {
            TransportVehicle key = car.BaseVehicle;   
            string keyStr = key.ToString();

            if (collection2.ContainsKey(key) || collection2Str.ContainsKey(keyStr)) return false;

            collection1.Add(car);
            collection1Str.Add(car.ToString());
            collection2.Add(key, car);
            collection2Str.Add(keyStr, car);
            return true;
        }

       
        public bool Remove(Car car)
        {
            TransportVehicle key = car.BaseVehicle;
            if (!collection2.ContainsKey(key)) return false;

            Car stored = collection2[key];   
            collection1.Remove(stored);
            collection1Str.Remove(stored.ToString());
            collection2.Remove(key);
            collection2Str.Remove(key.ToString());
            return true;
        }

        
        public Car GetFirst()
        {
            return new Car(collection1[0]);
        }

        public Car GetMiddle()
        {
            return new Car(collection1[collection1.Count / 2]);
        }

        public Car GetLast()
        {
            return new Car(collection1[collection1.Count - 1]);
        }

       
        public long TimeContainsInList(Car car, out bool found)
        {
            Stopwatch sw = Stopwatch.StartNew();
            found = collection1.Contains(car);
            sw.Stop();
            return sw.ElapsedTicks;
        }

        public long TimeContainsInStringList(string text, out bool found)
        {
            Stopwatch sw = Stopwatch.StartNew();
            found = collection1Str.Contains(text);
            sw.Stop();
            return sw.ElapsedTicks;
        }

        public long TimeContainsKey(TransportVehicle key, out bool found)
        {
            Stopwatch sw = Stopwatch.StartNew();
            found = collection2.ContainsKey(key);
            sw.Stop();
            return sw.ElapsedTicks;
        }

        public long TimeContainsKeyString(string key, out bool found)
        {
            Stopwatch sw = Stopwatch.StartNew();
            found = collection2Str.ContainsKey(key);
            sw.Stop();
            return sw.ElapsedTicks;
        }

        public long TimeContainsValue(Car car, out bool found)
        {
            Stopwatch sw = Stopwatch.StartNew();
            found = collection2.ContainsValue(car);
            sw.Stop();
            return sw.ElapsedTicks;
        }
    }
}