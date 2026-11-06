namespace TransportLibrary
{
    // Часть 2. Запросы к массиву объектов иерархии.
    // Для определения типа объекта используются операторы is и as.
    public static class Queries
    {
        // Вид транспорта: 1 - автомобиль, 2 - поезд (экспресс тоже поезд!), 3 - экспресс
        private static bool IsKind(TransportVehicle v, int kind)
        {
            if (kind == 1) return v is Car;
            if (kind == 2) return v is Train;
            if (kind == 3) return v is Express;
            return false;
        }

        // Запрос 1: количество транспортных средств указанного вида (оператор is)
        public static int CountByKind(TransportVehicle[] array, int kind)
        {
            int count = 0;
            foreach (TransportVehicle v in array)
            {
                if (IsKind(v, kind)) count++;
            }
            return count;
        }

        // Запрос 2: количество пассажиров во всех вагонах всех экспрессов (оператор as)
        public static int TotalExpressPassengers(TransportVehicle[] array)
        {
            int sum = 0;
            foreach (TransportVehicle v in array)
            {
                Express e = v as Express;
                if (e != null) sum += e.TotalPassengers;
            }
            return sum;
        }

        // Запрос 3: самый мощный автомобиль (оператор as). Если автомобилей нет - null
        public static Car MostPowerfulCar(TransportVehicle[] array)
        {
            Car best = null;
            foreach (TransportVehicle v in array)
            {
                Car c = v as Car;
                if (c != null && (best == null || c.Power > best.Power)) best = c;
            }
            return best;
        }

        // Запрос 4: средняя мощность транспортных средств указанного вида. Если таких нет - 0
        public static double AveragePower(TransportVehicle[] array, int kind)
        {
            int count = 0;
            double sum = 0;
            foreach (TransportVehicle v in array)
            {
                if (IsKind(v, kind))
                {
                    sum += v.Power;
                    count++;
                }
            }
            if (count == 0) return 0;
            return sum / count;
        }
    }
}