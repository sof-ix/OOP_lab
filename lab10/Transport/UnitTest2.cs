using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;
using TransportLibrary;

namespace TransportTests
{
    [TestClass]
    public class VehicleTests
    {
        private static string GetOutput(Action action)
        {
            TextWriter oldOut = Console.Out;
            StringWriter sw = new StringWriter();
            try
            {
                Console.SetOut(sw);
                action();
            }
            finally
            {
                Console.SetOut(oldOut);
            }
            return sw.ToString();
        }

        private static void RunInit(IInit obj, string input)
        {
            TextReader oldIn = Console.In;
            try
            {
                Console.SetIn(new StringReader(input));
                GetOutput(() => obj.Init());
            }
            finally
            {
                Console.SetIn(oldIn);
            }
        }

        [TestMethod]
        public void Vehicle_DefaultConstructor_SetsDefaults()
        {
            TransportVehicle v = new TransportVehicle();
            Assert.AreEqual("Без названия", v.Name);
            Assert.AreEqual(1, v.Power);
            Assert.IsNotNull(v.MainDriver);
        }

        [TestMethod]
        public void Vehicle_ParamConstructor_SetsValues()
        {
            TransportVehicle v = new TransportVehicle("Транспорт", 200);
            Assert.AreEqual("Транспорт", v.Name);
            Assert.AreEqual(200, v.Power);
        }

        [TestMethod]
        public void Vehicle_Name_Null_SetsDefault()
        {
            TransportVehicle v = new TransportVehicle("Транспорт", 200);
            v.Name = null;
            Assert.AreEqual("Без названия", v.Name);
        }

        [TestMethod]
        public void Vehicle_Power_Negative_SetsOne()
        {
            TransportVehicle v = new TransportVehicle("Транспорт", -5);
            Assert.AreEqual(1, v.Power);
        }

        [TestMethod]
        public void Vehicle_Power_TooBig_SetsOne()
        {
            TransportVehicle v = new TransportVehicle();
            v.Power = 50000;
            Assert.AreEqual(1, v.Power);
        }

        [TestMethod]
        public void Vehicle_MainDriver_Null_CreatesNewDriver()
        {
            TransportVehicle v = new TransportVehicle();
            v.MainDriver = null;
            Assert.IsNotNull(v.MainDriver);
        }

        [TestMethod]
        public void Vehicle_CopyConstructor_CopiesDriverSeparately()
        {
            TransportVehicle v1 = new TransportVehicle("Транспорт", 200);
            v1.MainDriver = new Driver("Иванов", 5);
            TransportVehicle v2 = new TransportVehicle(v1);
            v1.MainDriver.Name = "Другой";
            Assert.AreEqual("Иванов", v2.MainDriver.Name);
            Assert.AreEqual(200, v2.Power);
        }

        [TestMethod]
        public void Vehicle_Equals_SameData_True()
        {
            Assert.IsTrue(new TransportVehicle("А", 100).Equals(new TransportVehicle("А", 100)));
        }

        [TestMethod]
        public void Vehicle_Equals_DifferentPower_False()
        {
            Assert.IsFalse(new TransportVehicle("А", 100).Equals(new TransportVehicle("А", 200)));
        }

        [TestMethod]
        public void Vehicle_Equals_Null_False()
        {
            Assert.IsFalse(new TransportVehicle("А", 100).Equals(null));
        }

        [TestMethod]
        public void Vehicle_Equals_DifferentTypes_False()
        {
            Assert.IsFalse(new Car("А", 100, 4).Equals(new Train("А", 100, 4)));
        }

        [TestMethod]
        public void Vehicle_GetHashCode_EqualObjects_SameHash()
        {
            Assert.AreEqual(new TransportVehicle("А", 100).GetHashCode(),
                            new TransportVehicle("А", 100).GetHashCode());
        }

        [TestMethod]
        public void Vehicle_CompareTo_ByPower()
        {
            TransportVehicle weak = new TransportVehicle("А", 100);
            TransportVehicle strong = new TransportVehicle("Б", 200);
            Assert.IsTrue(weak.CompareTo(strong) < 0);
            Assert.IsTrue(strong.CompareTo(weak) > 0);
            Assert.AreEqual(0, weak.CompareTo(new TransportVehicle("В", 100)));
        }

        [TestMethod]
        public void Vehicle_RandomInit_SetsValidValues()
        {
            TransportVehicle v = new TransportVehicle();
            v.RandomInit();
            Assert.IsFalse(string.IsNullOrEmpty(v.Name));
            Assert.IsTrue(v.Power >= 50 && v.Power <= 500);
        }

        [TestMethod]
        public void Vehicle_Init_ReadsValues()
        {
            TransportVehicle v = new TransportVehicle();
            RunInit(v, "Транспорт\n250\nПетров\n7\n");
            Assert.AreEqual("Транспорт", v.Name);
            Assert.AreEqual(250, v.Power);
            Assert.AreEqual("Петров", v.MainDriver.Name);
            Assert.AreEqual(7, v.MainDriver.Experience);
        }

        [TestMethod]
        public void Vehicle_Init_WrongNumber_SetsDefaultPower()
        {
            TransportVehicle v = new TransportVehicle();
            RunInit(v, "Транспорт\nabc\nПетров\n7\n");
            Assert.AreEqual(1, v.Power);
        }

        [TestMethod]
        public void Show_IsVirtual_CallsDerivedVersion()
        {
            TransportVehicle v = new Car("Lada", 100, 5);   
            string text = GetOutput(() => v.Show());
            Assert.IsTrue(text.Contains("Это автомобиль"));
        }

        [TestMethod]
        public void ShowSimple_NotVirtual_CallsBaseVersion()
        {
            TransportVehicle v = new Car("Lada", 100, 5);
            string text = GetOutput(() => v.ShowSimple());
            Assert.IsTrue(text.Contains("Транспортное средство"));
            Assert.IsFalse(text.Contains("Автомобиль"));
        }

        [TestMethod]
        public void ShowSimple_CalledThroughDerivedReference_CallsDerivedVersion()
        {
            Car c = new Car("Lada", 100, 5);
            string text = GetOutput(() => c.ShowSimple());
            Assert.IsTrue(text.Contains("Автомобиль"));
        }

        [TestMethod]
        public void Show_Vehicle_PrintsNameAndPower()
        {
            string text = GetOutput(() => new TransportVehicle("Транспорт", 123).Show());
            Assert.IsTrue(text.Contains("Транспорт"));
            Assert.IsTrue(text.Contains("123"));
        }

        [TestMethod]
        public void ShowSimple_Vehicle_PrintsName()
        {
            string text = GetOutput(() => new TransportVehicle("Транспорт", 123).ShowSimple());
            Assert.IsTrue(text.Contains("Транспорт"));
        }

        [TestMethod]
        public void ShallowCopy_SharesDriver()
        {
            Car original = new Car("Lada", 100, 5);
            TransportVehicle shallow = original.ShallowCopy();
            Assert.AreNotSame(original, shallow);
            Assert.AreSame(original.MainDriver, shallow.MainDriver);
        }

        [TestMethod]
        public void ShallowCopy_ValueFieldsAreIndependent()
        {
            Car original = new Car("Lada", 100, 5);
            TransportVehicle shallow = original.ShallowCopy();
            original.Name = "BMW";
            Assert.AreEqual("Lada", shallow.Name);
        }

        [TestMethod]
        public void Clone_CreatesIndependentDriver()
        {
            Car original = new Car("Lada", 100, 5);
            original.MainDriver = new Driver("Иванов", 10);
            TransportVehicle clone = (TransportVehicle)original.Clone();
            original.MainDriver.Name = "Другой";
            Assert.AreNotSame(original.MainDriver, clone.MainDriver);
            Assert.AreEqual("Иванов", clone.MainDriver.Name);
        }

        [TestMethod]
        public void Clone_KeepsRealType()
        {
            TransportVehicle original = new Express("Сапсан", 8000, 10, 50);
            object clone = original.Clone();
            Assert.AreEqual(typeof(Express), clone.GetType());
            Assert.IsTrue(original.Equals(clone));
        }

        [TestMethod]
        public void Car_DefaultConstructor_SeatsIsOne()
        {
            Assert.AreEqual(1, new Car().Seats);
        }

        [TestMethod]
        public void Car_ParamConstructor_SetsValues()
        {
            Car c = new Car("Lada", 100, 5);
            Assert.AreEqual("Lada", c.Name);
            Assert.AreEqual(100, c.Power);
            Assert.AreEqual(5, c.Seats);
        }

        [TestMethod]
        public void Car_Seats_Zero_SetsOne()
        {
            Car c = new Car("Lada", 100, 0);
            Assert.AreEqual(1, c.Seats);
        }

        [TestMethod]
        public void Car_Seats_TooMany_SetsOne()
        {
            Car c = new Car("Lada", 100, 10);
            Assert.AreEqual(1, c.Seats);
        }

        [TestMethod]
        public void Car_CopyConstructor_CopiesAllFields()
        {
            Car c1 = new Car("Lada", 100, 5);
            Car c2 = new Car(c1);
            Assert.AreEqual("Lada", c2.Name);
            Assert.AreEqual(100, c2.Power);
            Assert.AreEqual(5, c2.Seats);
            Assert.IsTrue(c1.Equals(c2));
        }

        [TestMethod]
        public void Car_Equals_DifferentSeats_False()
        {
            Assert.IsFalse(new Car("Lada", 100, 5).Equals(new Car("Lada", 100, 4)));
        }

        [TestMethod]
        public void Car_Equals_SameData_True()
        {
            Assert.IsTrue(new Car("Lada", 100, 5).Equals(new Car("Lada", 100, 5)));
        }

        [TestMethod]
        public void Car_GetHashCode_EqualObjects_SameHash()
        {
            Assert.AreEqual(new Car("Lada", 100, 5).GetHashCode(), new Car("Lada", 100, 5).GetHashCode());
        }

        [TestMethod]
        public void Car_RandomInit_SetsValidValues()
        {
            Car c = new Car();
            c.RandomInit();
            Assert.IsTrue(c.Power >= 70 && c.Power <= 400);
            Assert.IsTrue(c.Seats >= 2 && c.Seats <= 7);
            Assert.IsFalse(string.IsNullOrEmpty(c.Name));
        }

        [TestMethod]
        public void Car_Init_ReadsValues()
        {
            Car c = new Car();
            RunInit(c, "Lada\n150\nИванов\n10\n5\n");
            Assert.AreEqual("Lada", c.Name);
            Assert.AreEqual(150, c.Power);
            Assert.AreEqual(5, c.Seats);
        }

        [TestMethod]
        public void Car_Show_PrintsSeats()
        {
            string text = GetOutput(() => new Car("Lada", 100, 5).Show());
            Assert.IsTrue(text.Contains("Lada"));
            Assert.IsTrue(text.Contains("мест: 5"));
        }

        [TestMethod]
        public void Train_DefaultConstructor_WagonCountIsOne()
        {
            Assert.AreEqual(1, new Train().WagonCount);
        }

        [TestMethod]
        public void Train_ParamConstructor_SetsValues()
        {
            Train t = new Train("Грузовой", 3000, 10);
            Assert.AreEqual("Грузовой", t.Name);
            Assert.AreEqual(3000, t.Power);
            Assert.AreEqual(10, t.WagonCount);
        }

        [TestMethod]
        public void Train_WagonCount_Zero_SetsOne()
        {
            Assert.AreEqual(1, new Train("Грузовой", 3000, 0).WagonCount);
        }

        [TestMethod]
        public void Train_WagonCount_TooMany_SetsOne()
        {
            Assert.AreEqual(1, new Train("Грузовой", 3000, 31).WagonCount);
        }

        [TestMethod]
        public void Train_CopyConstructor_CopiesAllFields()
        {
            Train t1 = new Train("Грузовой", 3000, 10);
            Train t2 = new Train(t1);
            Assert.AreEqual(10, t2.WagonCount);
            Assert.IsTrue(t1.Equals(t2));
        }

        [TestMethod]
        public void Train_Equals_DifferentWagons_False()
        {
            Assert.IsFalse(new Train("Грузовой", 3000, 10).Equals(new Train("Грузовой", 3000, 9)));
        }

        [TestMethod]
        public void Train_GetHashCode_EqualObjects_SameHash()
        {
            Assert.AreEqual(new Train("Т", 3000, 10).GetHashCode(), new Train("Т", 3000, 10).GetHashCode());
        }

        [TestMethod]
        public void Train_RandomInit_SetsValidValues()
        {
            Train t = new Train();
            t.RandomInit();
            Assert.IsTrue(t.Power >= 1000 && t.Power <= 5000);
            Assert.IsTrue(t.WagonCount >= 3 && t.WagonCount <= 14);
        }

        [TestMethod]
        public void Train_Init_ReadsValues()
        {
            Train t = new Train();
            RunInit(t, "Грузовой\n3000\nИванов\n10\n12\n");
            Assert.AreEqual(3000, t.Power);
            Assert.AreEqual(12, t.WagonCount);
        }

        [TestMethod]
        public void Train_Show_PrintsWagons()
        {
            string text = GetOutput(() => new Train("Грузовой", 3000, 10).Show());
            Assert.IsTrue(text.Contains("вагонов: 10"));
        }

        [TestMethod]
        public void Train_ShowSimple_PrintsWagons()
        {
            string text = GetOutput(() => new Train("Грузовой", 3000, 10).ShowSimple());
            Assert.IsTrue(text.Contains("Поезд"));
        }

        [TestMethod]
        public void Express_IsTrain()
        {
            object e = new Express();
            Assert.IsTrue(e is Train);
            Assert.IsTrue(e is TransportVehicle);
        }

        [TestMethod]
        public void Express_DefaultConstructor_PassengersIsOne()
        {
            Assert.AreEqual(1, new Express().PassengersPerWagon);
        }

        [TestMethod]
        public void Express_ParamConstructor_SetsValues()
        {
            Express e = new Express("Сапсан", 8000, 10, 50);
            Assert.AreEqual("Сапсан", e.Name);
            Assert.AreEqual(8000, e.Power);
            Assert.AreEqual(10, e.WagonCount);
            Assert.AreEqual(50, e.PassengersPerWagon);
        }

        [TestMethod]
        public void Express_PassengersPerWagon_Zero_SetsOne()
        {
            Assert.AreEqual(1, new Express("Сапсан", 8000, 10, 0).PassengersPerWagon);
        }

        [TestMethod]
        public void Express_PassengersPerWagon_TooMany_SetsOne()
        {
            Assert.AreEqual(1, new Express("Сапсан", 8000, 10, 151).PassengersPerWagon);
        }

        [TestMethod]
        public void Express_TotalPassengers_IsWagonsTimesPassengers()
        {
            Assert.AreEqual(500, new Express("Сапсан", 8000, 10, 50).TotalPassengers);
        }

        [TestMethod]
        public void Express_CopyConstructor_CopiesAllFields()
        {
            Express e1 = new Express("Сапсан", 8000, 10, 50);
            Express e2 = new Express(e1);
            Assert.AreEqual(50, e2.PassengersPerWagon);
            Assert.AreEqual(10, e2.WagonCount);
            Assert.IsTrue(e1.Equals(e2));
        }

        [TestMethod]
        public void Express_Equals_DifferentPassengers_False()
        {
            Assert.IsFalse(new Express("С", 8000, 10, 50).Equals(new Express("С", 8000, 10, 60)));
        }

        [TestMethod]
        public void Express_Equals_ExpressAndTrain_False()
        {
            Assert.IsFalse(new Express("С", 8000, 10, 50).Equals(new Train("С", 8000, 10)));
        }

        [TestMethod]
        public void Express_GetHashCode_EqualObjects_SameHash()
        {
            Assert.AreEqual(new Express("С", 8000, 10, 50).GetHashCode(),
                            new Express("С", 8000, 10, 50).GetHashCode());
        }

        [TestMethod]
        public void Express_RandomInit_SetsValidValues()
        {
            Express e = new Express();
            e.RandomInit();
            Assert.IsTrue(e.Power >= 3000 && e.Power <= 9000);
            Assert.IsTrue(e.PassengersPerWagon >= 40 && e.PassengersPerWagon <= 100);
            Assert.IsTrue(e.WagonCount >= 3 && e.WagonCount <= 14);
        }

        [TestMethod]
        public void Express_Init_ReadsValues()
        {
            Express e = new Express();
            RunInit(e, "Сапсан\n8000\nИванов\n10\n10\n50\n");
            Assert.AreEqual("Сапсан", e.Name);
            Assert.AreEqual(10, e.WagonCount);
            Assert.AreEqual(50, e.PassengersPerWagon);
        }

        [TestMethod]
        public void Express_Show_PrintsTotalPassengers()
        {
            string text = GetOutput(() => new Express("Сапсан", 8000, 10, 50).Show());
            Assert.IsTrue(text.Contains("Это экспресс"));
            Assert.IsTrue(text.Contains("500"));
        }

        [TestMethod]
        public void Express_ShowSimple_PrintsExpress()
        {
            string text = GetOutput(() => new Express("Сапсан", 8000, 10, 50).ShowSimple());
            Assert.IsTrue(text.Contains("Экспресс"));
        }
    }
}