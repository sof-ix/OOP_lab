using System;

namespace Lab9
{
    public class TimeArray
    {
        private Time[] arr;

        public TimeArray()
        {
            arr = new Time[0];
        }

        public TimeArray(int size)
        {
            if (size < 0)
                throw new ArgumentException("Размер массива не может быть отрицательным");

            arr = new Time[size];
            Random rnd = new Random();
            for (int i = 0; i < size; i++)
            {
                arr[i] = new Time(rnd.Next(0, 24), rnd.Next(0, 60));
            }
        }

        public TimeArray(Time[] items)
        {
            arr = new Time[items.Length];
            for (int i = 0; i < items.Length; i++)
            {
                arr[i] = items[i];
            }
        }

        public int Length
        {
            get { return arr.Length; }
        }

        public Time this[int index]
        {
            get
            {
                if (index < 0 || index >= arr.Length)
                    throw new ArgumentException("Индекс выходит за пределы массива");
                return arr[index];
            }
            set
            {
                if (index < 0 || index >= arr.Length)
                    throw new ArgumentException("Индекс выходит за пределы массива");
                arr[index] = value;
            }
        }

        public string View()
        {
            if (arr.Length == 0)
                return "Массив пуст";

            string result = "";
            for (int i = 0; i < arr.Length; i++)
            {
                result = result + "[" + i + "] " + arr[i].ToString() + "\n";
            }
            return result;
        }
    }
}