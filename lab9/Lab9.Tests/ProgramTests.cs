using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Lab9;

namespace Lab9.Tests
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void FindMin_MinInMiddle()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(5, 0), new Time(1, 30), new Time(3, 10) });
            Assert.AreEqual("01:30", Lab9.Program.FindMin(a).ToString());
        }

        [TestMethod]
        public void FindMin_MinAtStart()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(0, 10), new Time(1, 30), new Time(3, 10) });
            Assert.AreEqual("00:10", Lab9.Program.FindMin(a).ToString());
        }

        [TestMethod]
        public void FindMin_MinAtEnd()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(5, 0), new Time(4, 30), new Time(0, 59) });
            Assert.AreEqual("00:59", Lab9.Program.FindMin(a).ToString());
        }

        [TestMethod]
        public void FindMin_SameHours_ComparesMinutes()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(2, 5), new Time(2, 10), new Time(2, 3) });
            Assert.AreEqual("02:03", Lab9.Program.FindMin(a).ToString());
        }

        [TestMethod]
        public void FindMin_OneElement()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(7, 7) });
            Assert.AreEqual("07:07", Lab9.Program.FindMin(a).ToString());
        }

        [TestMethod]
        public void FindMin_AllEqual()
        {
            TimeArray a = new TimeArray(new Time[] { new Time(1, 1), new Time(1, 1) });
            Assert.AreEqual("01:01", Lab9.Program.FindMin(a).ToString());
        }

        [TestMethod]
        public void FindMin_EmptyArray_ThrowsException()
        {
            bool error = false;
            try
            {
                Lab9.Program.FindMin(new TimeArray());
            }
            catch (ArgumentException)
            {
                error = true;
            }
            Assert.IsTrue(error);
        }
    }
}