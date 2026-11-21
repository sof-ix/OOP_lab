using System;
using System.Collections.Generic;

namespace TransportLibrary
{
    // Задание 2. Методы для работы с обобщённой коллекцией Queue<TransportVehicle>
    public static class QueueTasks
    {
  
        public static void Add(Queue<TransportVehicle> queue, TransportVehicle vehicle)
        {
            queue.Enqueue(vehicle);
        }

      
        public static TransportVehicle RemoveFirst(Queue<TransportVehicle> queue)
        {
            if (queue.Count == 0) return null;
            return queue.Dequeue();
        }

    
        public static int CountByKind(Queue<TransportVehicle> queue, int kind)
        {
            int count = 0;
            foreach (TransportVehicle v in queue)
            {
                if (Queries.IsKind(v, kind)) count++;
            }
            return count;
        }

        public static List<TransportVehicle> GetByKind(Queue<TransportVehicle> queue, int kind)
        {
            List<TransportVehicle> result = new List<TransportVehicle>();
            foreach (TransportVehicle v in queue)
            {
                if (Queries.IsKind(v, kind)) result.Add(v);
            }
            return result;
        }

     
        public static int TotalExpressPassengers(Queue<TransportVehicle> queue)
        {
            int sum = 0;
            foreach (TransportVehicle v in queue)
            {
                Express e = v as Express;
                if (e != null) sum += e.TotalPassengers;
            }
            return sum;
        }

        public static Queue<TransportVehicle> ShallowCopy(Queue<TransportVehicle> queue)
        {
            return new Queue<TransportVehicle>(queue);
        }

        
        public static Queue<TransportVehicle> DeepCopy(Queue<TransportVehicle> queue)
        {
            Queue<TransportVehicle> copy = new Queue<TransportVehicle>();
            foreach (TransportVehicle v in queue)
            {
                copy.Enqueue((TransportVehicle)v.Clone());
            }
            return copy;
        }

  
        public static Queue<TransportVehicle> GetSortedByPower(Queue<TransportVehicle> queue)
        {
            TransportVehicle[] array = queue.ToArray();
            Array.Sort(array);
            return new Queue<TransportVehicle>(array);
        }

     
        public static Queue<TransportVehicle> GetSortedByName(Queue<TransportVehicle> queue)
        {
            TransportVehicle[] array = queue.ToArray();
            Array.Sort(array, new SortByName());
            return new Queue<TransportVehicle>(array);
        }

   
        public static TransportVehicle FindByName(Queue<TransportVehicle> queue, string name)
        {
            foreach (TransportVehicle v in queue)
            {
                if (v.Name == name) return v;
            }
            return null;
        }

     
        public static int BinarySearchByName(Queue<TransportVehicle> sortedByName, string name)
        {
            return Search.BinarySearchByName(sortedByName.ToArray(), name);
        }

   
        public static bool Contains(Queue<TransportVehicle> queue, TransportVehicle vehicle)
        {
            return queue.Contains(vehicle);
        }
    }
}