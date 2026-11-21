namespace TransportLibrary
{
   
    public static class Queries
    {
        public static bool IsKind(TransportVehicle v, int kind)
        {
            if (kind == 1) return v is Car;
            if (kind == 2) return v is Train;
            if (kind == 3) return v is Express;
            return false;
        }
        public static int CountByKind(TransportVehicle[] array, int kind)
        {
            int count = 0;
            foreach (TransportVehicle v in array)
            {
                if (IsKind(v, kind)) count++;
            }
            return count;
        }
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