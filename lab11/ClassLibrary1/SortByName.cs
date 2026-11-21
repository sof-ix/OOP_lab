using System.Collections;

namespace TransportLibrary
{
    public class SortByName : IComparer
    {
        public int Compare(object ob1, object ob2)
        {
            TransportVehicle v1 = (TransportVehicle)ob1;
            TransportVehicle v2 = (TransportVehicle)ob2;
            return string.Compare(v1.Name, v2.Name);
        }
    }
}