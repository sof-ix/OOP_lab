using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lab9;

namespace Lab9.Tests
{
    [TestClass]
    public class TimeArrayTests
    {
        private bool GetFails(TimeArray a, int index)
        {
            try
            {
                Time t = a[index];
            }
            catch (ArgumentException)
            {
                return true;
            }
            return false;
        }

        private bool SetFails(TimeArray a, int index)
        {
            try
            {
                a[index] = new Time(1, 1);
            }
            catch (ArgumentException)
            {
                return true;
            }
            return false;
        }


        [TestMethod]
        public void Constructor_Default_CreatesEmptyArray()
        {
            TimeArray a = new TimeArray();
            Assert.AreEqual(0, a.Length);
        }

        [TestMethod]
        public void Constructor_Random_CreatesCorrectSize()
        {
            TimeArray a = new TimeArray(10);
            Assert.AreEqual(10, a.Length);
        }

        [TestMethod]
        public void Constructor_Random_ValuesAreValid()
        {
            TimeArray a = new TimeArray(50);
            for (int i = 0; i < a.Length; i++)
            {
                Assert.IsTrue(a[i].Hours >= 0 && a[i].Hours < 24);
                Assert.IsTrue(a[i].Minutes >= 0 && a[i].Minutes <= 59);
            }
        }

        [TestMethod]
        public void Constructor_Random_ZeroSize_GivesEmptyArray()
        {
            TimeArray a = new TimeArray(0);
            Assert.AreEqual(0, a.Length);
        }

        [TestMethod]
        public void Constructor_Random_NegativeSize_ThrowsException()
        {
            bool error = false;
            try
            {
                TimeArray a = new TimeArray(-1);
            }
            catch (ArgumentException)
            {
                error = true;
            }
            Assert.IsTrue(error);
        }

        [TestMethod]
        public void Constructor_FromItems_CopiesElements()
        {
            Time[] items = { new Time(1, 0), new Time(2, 30), new Time(3, 45) };
            TimeArray a = new TimeArray(items);
            Assert.AreEqual(3, a.Length);
            Assert.AreEqual("01:00", a[0].ToString());
            Assert.AreEqual("02:30", a[1].ToString());
            Assert.AreEqual("03:45", a[2].ToString());
        }

        [TestMethod]
        public void Constructor_FromItems_ArrayIsCopied()
        {
            Time[] items = { new Time(1, 0), new Time(2, 0) };
            TimeArray a = new TimeArray(items);
            items[0] = new Time(9, 9);
            Assert.AreEqual("01:00", a[0].ToString());
        }

        [TestMethod]
        public void Indexer_Set_ChangesElement()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 0), new Time(2, 0) });
            a[1] = new Time(7, 7);
            Assert.AreEqual("07:07", a[1].ToString());
        }

        [TestMethod]
        public void Indexer_Get_OutOfRange_ThrowsException()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 0), new Time(2, 0) });
            Assert.IsTrue(GetFails(a, -1));
            Assert.IsTrue(GetFails(a, 2));
            Assert.IsTrue(GetFails(a, 100));
        }

        [TestMethod]
        public void Indexer_Get_BorderIndexes_Work()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 0), new Time(2, 0) });
            Assert.IsFalse(GetFails(a, 0));
            Assert.IsFalse(GetFails(a, 1));
        }

        [TestMethod]
        public void Indexer_Set_OutOfRange_ThrowsException()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 0), new Time(2, 0) });
            Assert.IsTrue(SetFails(a, -1));
            Assert.IsTrue(SetFails(a, 2));
        }

        [TestMethod]
        public void Indexer_EmptyArray_ThrowsException()
        {
            TimeArray a = new TimeArray();
            Assert.IsTrue(GetFails(a, 0));
        }

        [TestMethod]
        public void View_ReturnsAllElements()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 30), new Time(2, 45) });
            Assert.AreEqual("[0] 01:30\n[1] 02:45\n", a.View());
        }

        [TestMethod]
        public void View_EmptyArray_ReturnsMessage()
        {
            TimeArray a = new TimeArray();
            Assert.AreEqual("Массив пуст", a.View());
        }
    }
}