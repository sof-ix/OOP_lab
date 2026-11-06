using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportLibrary;

namespace TransportTests
{
    [TestClass]
    public class DriverTests
    {
        [TestMethod]
        public void DefaultConstructor_SetsDefaultValues()
        {
            Driver d = new Driver();
            Assert.AreEqual("Неизвестен", d.Name);
            Assert.AreEqual(0, d.Experience);
        }

        [TestMethod]
        public void ParamConstructor_SetsValues()
        {
            Driver d = new Driver("Иванов", 10);
            Assert.AreEqual("Иванов", d.Name);
            Assert.AreEqual(10, d.Experience);
        }

        [TestMethod]
        public void CopyConstructor_CopiesValues()
        {
            Driver d1 = new Driver("Иванов", 10);
            Driver d2 = new Driver(d1);
            Assert.AreEqual("Иванов", d2.Name);
            Assert.AreEqual(10, d2.Experience);
            Assert.AreNotSame(d1, d2);
        }

        [TestMethod]
        public void Name_Empty_SetsDefault()
        {
            Driver d = new Driver("Иванов", 5);
            d.Name = "";
            Assert.AreEqual("Неизвестен", d.Name);
        }

        [TestMethod]
        public void Experience_Negative_SetsZero()
        {
            Driver d = new Driver("Иванов", 5);
            d.Experience = -3;
            Assert.AreEqual(0, d.Experience);
        }

        [TestMethod]
        public void Experience_TooBig_SetsZero()
        {
            Driver d = new Driver("Иванов", 5);
            d.Experience = 100;
            Assert.AreEqual(0, d.Experience);
        }

        [TestMethod]
        public void Equals_SameData_ReturnsTrue()
        {
            Assert.IsTrue(new Driver("Иванов", 5).Equals(new Driver("Иванов", 5)));
        }

        [TestMethod]
        public void Equals_DifferentData_ReturnsFalse()
        {
            Assert.IsFalse(new Driver("Иванов", 5).Equals(new Driver("Иванов", 6)));
        }

        [TestMethod]
        public void Equals_Null_ReturnsFalse()
        {
            Assert.IsFalse(new Driver("Иванов", 5).Equals(null));
        }

        [TestMethod]
        public void Equals_OtherType_ReturnsFalse()
        {
            Assert.IsFalse(new Driver("Иванов", 5).Equals("Иванов"));
        }

        [TestMethod]
        public void GetHashCode_EqualObjects_SameHash()
        {
            Assert.AreEqual(new Driver("Иванов", 5).GetHashCode(), new Driver("Иванов", 5).GetHashCode());
        }

        [TestMethod]
        public void RandomInit_SetsValidValues()
        {
            Driver d = new Driver();
            d.RandomInit();
            Assert.IsFalse(string.IsNullOrEmpty(d.Name));
            Assert.IsTrue(d.Experience >= 0 && d.Experience <= 40);
        }

        [TestMethod]
        public void Init_ReadsValuesFromKeyboard()
        {
            Driver d = new Driver();
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            try
            {
                Console.SetIn(new StringReader("Петров\n7\n"));
                Console.SetOut(new StringWriter());
                d.Init();
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }
            Assert.AreEqual("Петров", d.Name);
            Assert.AreEqual(7, d.Experience);
        }

        [TestMethod]
        public void Init_WrongNumber_SetsZeroExperience()
        {
            Driver d = new Driver();
            TextReader oldIn = Console.In;
            TextWriter oldOut = Console.Out;
            try
            {
                Console.SetIn(new StringReader("Петров\nabc\n"));
                Console.SetOut(new StringWriter());
                d.Init();
            }
            finally
            {
                Console.SetIn(oldIn);
                Console.SetOut(oldOut);
            }
            Assert.AreEqual(0, d.Experience);
        }

        [TestMethod]
        public void Show_PrintsNameAndExperience()
        {
            Driver d = new Driver("Иванов", 10);
            TextWriter oldOut = Console.Out;
            StringWriter sw = new StringWriter();
            try
            {
                Console.SetOut(sw);
                d.Show();
            }
            finally
            {
                Console.SetOut(oldOut);
            }
            Assert.IsTrue(sw.ToString().Contains("Иванов"));
            Assert.IsTrue(sw.ToString().Contains("10"));
        }
    }
}