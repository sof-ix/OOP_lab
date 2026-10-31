
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lab9;

namespace Lab9.Tests
{
    [TestClass]
    public class TimeTests
    {
        private bool CreateFails(int h, int m)
        {
            try
            {
                Time t = new Time(h, m);
            }
            catch (ArgumentException)
            {
                return true;
            }
            return false;
        }

        [TestMethod]
        public void Constructor_Default_CreatesZeroTime()
        {
            Time t = new Time();
            Assert.AreEqual(0, t.Hours);
            Assert.AreEqual(0, t.Minutes);
        }

        [TestMethod]
        public void Constructor_WithParameters_SetsFields()
        {
            Time t = new Time(2, 30);
            Assert.AreEqual(2, t.Hours);
            Assert.AreEqual(30, t.Minutes);
        }

        [TestMethod]
        public void Constructor_InvalidValues_ThrowsException()
        {
            Assert.IsTrue(CreateFails(-1, 0));
            Assert.IsTrue(CreateFails(0, -1));
            Assert.IsTrue(CreateFails(0, 60));
        }

        [TestMethod]
        public void Constructor_BorderValues_AreAllowed()
        {
            Assert.IsFalse(CreateFails(0, 0));
            Assert.IsFalse(CreateFails(0, 59));
            Assert.IsFalse(CreateFails(100, 0));
        }

        [TestMethod]
        public void Properties_SetValidValues_ChangeFields()
        {
            Time t = new Time();
            t.Hours = 5;
            t.Minutes = 45;
            Assert.AreEqual(5, t.Hours);
            Assert.AreEqual(45, t.Minutes);
        }

        [TestMethod]
        public void ToString_ReturnsTwoDigitFormat()
        {
            Assert.AreEqual("03:07", new Time(3, 7).ToString());
            Assert.AreEqual("00:00", new Time().ToString());
        }

        [TestMethod]
        public void Count_IncreasesAfterCreatingObject()
        {
            int before = Time.Count;
            Time t = new Time(1, 1);
            Assert.AreEqual(before + 1, Time.Count);
        }

        [TestMethod]
        public void Count_DoesNotChangeIfConstructorFails()
        {
            int before = Time.Count;
            CreateFails(-5, 0);
            Assert.AreEqual(before, Time.Count);
        }

        [TestMethod]
        public void StaticMinusMinutes_SimpleCase()
        {
            Time r = Time.MinusMinutes(new Time(2, 30), 10);
            Assert.AreEqual("02:20", r.ToString());
        }

        [TestMethod]
        public void StaticMinusMinutes_BorrowsHour()
        {
            Time r = Time.MinusMinutes(new Time(2, 10), 20);
            Assert.AreEqual("01:50", r.ToString());
        }

        [TestMethod]
        public void StaticMinusMinutes_MoreThanTotal_GivesZero()
        {
            Time r = Time.MinusMinutes(new Time(0, 30), 100);
            Assert.AreEqual("00:00", r.ToString());
        }

        [TestMethod]
        public void StaticMinusMinutes_ExactlyAllTime_GivesZero()
        {
            Time r = Time.MinusMinutes(new Time(1, 15), 75);
            Assert.AreEqual("00:00", r.ToString());
        }

        [TestMethod]
        public void StaticMinusMinutes_DoesNotChangeSource()
        {
            Time t = new Time(2, 30);
            Time r = Time.MinusMinutes(t, 10);
            Assert.AreEqual("02:30", t.ToString());
        }

        [TestMethod]
        public void MethodMinusMinutes_SimpleCase()
        {
            Time t = new Time(2, 30);
            Assert.AreEqual("02:20", t.MinusMinutes(10).ToString());
        }

        [TestMethod]
        public void MethodMinusMinutes_BorrowsHour()
        {
            Time t = new Time(2, 10);
            Assert.AreEqual("01:50", t.MinusMinutes(20).ToString());
        }

        [TestMethod]
        public void MethodMinusMinutes_MoreThanTotal_GivesZero()
        {
            Time t = new Time(0, 30);
            Assert.AreEqual("00:00", t.MinusMinutes(100).ToString());
        }

        [TestMethod]
        public void MethodMinusMinutes_DoesNotChangeSource()
        {
            Time t = new Time(2, 30);
            Time r = t.MinusMinutes(10);
            Assert.AreEqual("02:30", t.ToString());
        }

        [TestMethod]
        public void UnaryMinus_ResetsTimeToZero()
        {
            Time t = new Time(3, 15);
            Time z = -t;
            Assert.AreEqual(0, z.Hours);
            Assert.AreEqual(0, z.Minutes);
        }

        [TestMethod]
        public void Decrement_SubtractsOneMinute()
        {
            Time t = new Time(1, 30);
            t--;
            Assert.AreEqual("01:29", t.ToString());
        }

        [TestMethod]
        public void Decrement_BorrowsHour()
        {
            Time t = new Time(1, 0);
            t--;
            Assert.AreEqual("00:59", t.ToString());
        }

        [TestMethod]
        public void Decrement_OfZero_StaysZero()
        {
            Time t = new Time(0, 0);
            t--;
            Assert.AreEqual("00:00", t.ToString());
        }

        [TestMethod]
        public void ImplicitInt_ReturnsHours()
        {
            int h = new Time(5, 40);
            Assert.AreEqual(5, h);
        }

        [TestMethod]
        public void ImplicitInt_MinutesAreDiscarded()
        {
            int h = new Time(0, 59);
            Assert.AreEqual(0, h);
        }

        [TestMethod]
        public void ExplicitBool_HoursAndMinutesNotZero_True()
        {
            Time t = new Time(1, 30);
            Assert.IsTrue((bool)t);
        }

        [TestMethod]
        public void ExplicitBool_ZeroTime_False()
        {
            Time t = new Time(0, 0);
            Assert.IsFalse((bool)t);
        }

        [TestMethod]
        public void ExplicitBool_OneOfValuesIsZero_False()
        {
            Assert.IsFalse((bool)new Time(1, 0));
            Assert.IsFalse((bool)new Time(0, 5));
        }

        [TestMethod]
        public void Equal_SameValues_True()
        {
            Time a = new Time(2, 15);
            Time b = new Time(2, 15);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [TestMethod]
        public void Equal_DifferentMinutes_False()
        {
            Time a = new Time(2, 15);
            Time b = new Time(2, 16);
            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);
        }

        [TestMethod]
        public void Equal_DifferentHours_False()
        {
            Time a = new Time(2, 15);
            Time b = new Time(3, 15);
            Assert.IsFalse(a == b);
            Assert.IsTrue(a != b);
        }

        [TestMethod]
        public void Equal_WithNull_WorksCorrectly()
        {
            Time a = new Time(1, 1);
            Time n = null;
            Assert.IsFalse(a == n);
            Assert.IsFalse(n == a);
            Assert.IsTrue(n == null);
            Assert.IsTrue(a != n);
        }

        [TestMethod]
        public void Equals_WorksWithObjects()
        {
            Time a = new Time(4, 20);
            Time b = new Time(4, 20);
            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(new Time(4, 21)));
            Assert.IsFalse(a.Equals("строка"));
            Assert.IsFalse(a.Equals(null));
        }

        [TestMethod]
        public void GetHashCode_EqualObjects_EqualHash()
        {
            Time a = new Time(4, 20);
            Time b = new Time(4, 20);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void LessAndGreater_CompareByTotalMinutes()
        {
            Time small = new Time(2, 5);
            Time big = new Time(2, 10);
            Assert.IsTrue(small < big);
            Assert.IsFalse(big < small);
            Assert.IsTrue(big > small);
            Assert.IsFalse(small > big);
        }

        [TestMethod]
        public void LessAndGreater_EqualTimes_BothFalse()
        {
            Time a = new Time(1, 1);
            Time b = new Time(1, 1);
            Assert.IsFalse(a < b);
            Assert.IsFalse(a > b);
        }

        [TestMethod]
        public void ToMinutes_ConvertsCorrectly()
        {
            Assert.AreEqual(150, new Time(2, 30).ToMinutes());
        }
    }
}
