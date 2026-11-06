namespace TransportLibrary
{
    public static class Search
    {
        public static int FindByName(TransportVehicle[] array, string name)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i].Name == name) return i;
            }
            return -1;
        }

        public static int BinarySearchByName(TransportVehicle[] array, string name)
        {
            int left = 0;
            int right = array.Length - 1;

            while (left <= right)
            {
                int middle = (left + right) / 2;
                int result = string.Compare(array[middle].Name, name);

                if (result == 0) return middle;          
                if (result < 0) left = middle + 1;       
                else right = middle - 1;                 
            }
            return -1;
        }
    }
}