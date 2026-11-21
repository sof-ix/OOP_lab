using System.Collections;

namespace TransportLibrary
{
    // Задание 1. Методы для работы с коллекцией Hashtable.
   
    public static class HashtableTasks
    {
        
        public static int NextKey(Hashtable table)
        {
            int max = 0;
            foreach (int key in table.Keys)
            {
                if (key > max) max = key;
            }
            return max + 1;
        }

        public static int Add(Hashtable table, TransportVehicle vehicle)
        {
            int key = NextKey(table);
            table.Add(key, vehicle);
            return key;
        }

        
        public static bool Remove(Hashtable table, int key)
        {
            if (!table.ContainsKey(key)) return false;
            table.Remove(key);
            return true;
        }

        
        public static int CountByKind(Hashtable table, int kind)
        {
            int count = 0;
            foreach (TransportVehicle v in table.Values)
            {
                if (Queries.IsKind(v, kind)) count++;
            }
            return count;
        }


        public static ArrayList GetByKind(Hashtable table, int kind)
        {
            ArrayList result = new ArrayList();
            foreach (TransportVehicle v in table.Values)
            {
                if (Queries.IsKind(v, kind)) result.Add(v);
            }
            return result;
        }

        
        public static double AveragePower(Hashtable table, int kind)
        {
            int count = 0;
            double sum = 0;
            foreach (TransportVehicle v in table.Values)
            {
                if (Queries.IsKind(v, kind))
                {
                    sum += v.Power;
                    count++;
                }
            }
            if (count == 0) return 0;
            return sum / count;
        }

        
        public static Hashtable ShallowClone(Hashtable table)
        {
            return (Hashtable)table.Clone();
        }

        
        public static Hashtable DeepClone(Hashtable table)
        {
            Hashtable copy = new Hashtable();
            foreach (DictionaryEntry entry in table)
            {
                TransportVehicle v = (TransportVehicle)entry.Value;
                copy.Add(entry.Key, v.Clone());
            }
            return copy;
        }

       
        public static ArrayList GetSortedByPower(Hashtable table)
        {
            ArrayList list = new ArrayList(table.Values);
            list.Sort();
            return list;
        }

        
        public static ArrayList GetSortedByName(Hashtable table)
        {
            ArrayList list = new ArrayList(table.Values);
            list.Sort(new SortByName());
            return list;
        }

        
        public static int FindKeyByName(Hashtable table, string name)
        {
            foreach (DictionaryEntry entry in table)
            {
                TransportVehicle v = (TransportVehicle)entry.Value;
                if (v.Name == name) return (int)entry.Key;
            }
            return -1;
        }

        public static bool ContainsVehicle(Hashtable table, TransportVehicle vehicle)
        {
            return table.ContainsValue(vehicle);
        }
    }
}