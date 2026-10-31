using System;

namespace Lab9
{
    public class Time
    {
        private int hours;
        private int minutes;
        private static int count = 0;

        public int Hours
        {
            get { return hours; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Часы не могут быть меньше 0");
                hours = value;
            }
        }

        public int Minutes
        {
            get { return minutes; }
            set
            {
                if (value < 0 || value > 59)
                    throw new ArgumentException("Минуты должны быть от 0 до 59");
                minutes = value;
            }
        }

        public static int Count
        {
            get { return count; }
        }

        public Time()
        {
            Hours = 0;
            Minutes = 0;
            count++;
        }

        public Time(int hours, int minutes)
        {
            Hours = hours;
            Minutes = minutes;
            count++;
        }

        public int ToMinutes()
        {
            return hours * 60 + minutes;
        }

        public override string ToString()
        {
            return hours.ToString("D2") + ":" + minutes.ToString("D2");
        }

        //  Часть 1 

        public static Time MinusMinutes(Time t, int minutes)
        {
            int total = t.hours * 60 + t.minutes - minutes;
            if (total < 0)
                total = 0;
            return new Time(total / 60, total % 60);
        }

        public Time MinusMinutes(int minutes)
        {
            int total = hours * 60 + this.minutes - minutes;
            if (total < 0)
                total = 0;
            return new Time(total / 60, total % 60);
        }

        //  Часть 2 
        public static Time operator -(Time t)
        {
            return new Time(0, 0);
        }

        public static Time operator --(Time t)
        {
            int total = t.hours * 60 + t.minutes - 1;
            if (total < 0)
                total = 0;
            return new Time(total / 60, total % 60);
        }

        public static implicit operator int(Time t)
        {
            return t.hours;
        }

        public static explicit operator bool(Time t)
        {
            return t.hours != 0 && t.minutes != 0;
        }


        public static bool operator ==(Time t1, Time t2)
        {
            if (object.ReferenceEquals(t1, null) || object.ReferenceEquals(t2, null))
                return object.ReferenceEquals(t1, t2);
            return t1.hours == t2.hours && t1.minutes == t2.minutes;
        }

        public static bool operator !=(Time t1, Time t2)
        {
            return !(t1 == t2);
        }

        public override bool Equals(object obj)
        {
            if (obj is Time)
                return this == (Time)obj;
            return false;
        }

        public override int GetHashCode()
        {
            return hours * 60 + minutes;
        }

        // Часть 3 

        public static bool operator <(Time t1, Time t2)
        {
            return t1.ToMinutes() < t2.ToMinutes();
        }

        public static bool operator >(Time t1, Time t2)
        {
            return t1.ToMinutes() > t2.ToMinutes();
        }
    }
}