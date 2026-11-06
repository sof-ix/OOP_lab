using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportLibrary;

namespace TransportTests
{
    [TestClass]
    public class QueriesAndSearchTests
    {

        private static TransportVehicle[] CreateArray()
        {
            return new TransportVehicle[]
            {
                new Car("Lada", 100, 5),
                new Car("BMW", 300, 4),
                new Train("Грузовой", 3000, 10),
                new Express("Сапсан", 8000, 10, 50),
                new Express("Ласточка", 6000, 5, 100)
            };
        }

        [TestMethod]
        public void CountByKind_Cars()
        {
            Assert.AreEqual(2, Queries.CountByKind(CreateArray(), 1));
        }

        [TestMethod]
        public void CountByKind_Trains_IncludesExpresses()
        {
            Assert.AreEqual(3, Queries.CountByKind(CreateArray(), 2));
        }

        [TestMethod]
        public void CountByKind_Expresses()
        {
            Assert.AreEqual(2, Queries.CountByKind(CreateArray(), 3));
        }

        [TestMethod]
        public void CountByKind_UnknownKind_ReturnsZero()
        {
            Assert.AreEqual(0, Queries.CountByKind(CreateArray(), 7));
        }

        [TestMethod]
        public void CountByKind_EmptyArray_ReturnsZero()
        {
            Assert.AreEqual(0, Queries.CountByKind(new TransportVehicle[0], 1));
        }

        [TestMethod]
        public void TotalExpressPassengers_SumsAllExpresses()
        {
            // 10 * 50 + 5 * 100 = 1000
            Assert.AreEqual(1000, Queries.TotalExpressPassengers(CreateArray()));
        }

        [TestMethod]
        public void TotalExpressPassengers_NoExpresses_ReturnsZero()
        {
            TransportVehicle[] array = { new Car("Lada", 100, 5), new Train("Т", 3000, 10) };
            Assert.AreEqual(0, Queries.TotalExpressPassengers(array));
        }

        [TestMethod]
        public void MostPowerfulCar_ReturnsCarWithMaxPower()
        {
            Car best = Queries.MostPowerfulCar(CreateArray());
            Assert.IsNotNull(best);
            Assert.AreEqual("BMW", best.Name);
        }

        [TestMethod]
        public void MostPowerfulCar_NoCars_ReturnsNull()
        {
            TransportVehicle[] array = { new Train("Т", 3000, 10) };
            Assert.IsNull(Queries.MostPowerfulCar(array));
        }

        [TestMethod]
        public void AveragePower_Cars()
        {
            Assert.AreEqual(200.0, Queries.AveragePower(CreateArray(), 1), 0.001);
        }

        [TestMethod]
        public void AveragePower_Expresses()
        {
            Assert.AreEqual(7000.0, Queries.AveragePower(CreateArray(), 3), 0.001);
        }

        [TestMethod]
        public void AveragePower_Trains_IncludesExpresses()
        {
            // (3000 + 8000 + 6000) / 3
            Assert.AreEqual(17000.0 / 3, Queries.AveragePower(CreateArray(), 2), 0.001);
        }

        [TestMethod]
        public void AveragePower_NoSuchVehicles_ReturnsZero()
        {
            TransportVehicle[] array = { new Car("Lada", 100, 5) };
            Assert.AreEqual(0.0, Queries.AveragePower(array, 3), 0.001);
        }

        [TestMethod]
        public void Sort_ByIComparable_OrdersByPower()
        {
            TransportVehicle[] array = CreateArray();
            Array.Sort(array);
            for (int i = 0; i < array.Length - 1; i++)
            {
                Assert.IsTrue(array[i].Power <= array[i + 1].Power);
            }
        }

        [TestMethod]
        public void SortByName_Compare_ReturnsCorrectSign()
        {
            SortByName comparer = new SortByName();
            TransportVehicle a = new TransportVehicle("Audi", 100);
            TransportVehicle b = new TransportVehicle("BMW", 100);
            Assert.IsTrue(comparer.Compare(a, b) < 0);
            Assert.IsTrue(comparer.Compare(b, a) > 0);
            Assert.AreEqual(0, comparer.Compare(a, new TransportVehicle("Audi", 500)));
        }

        [TestMethod]
        public void Sort_ByIComparer_OrdersByName()
        {
            TransportVehicle[] array =
            {
                new Car("Lada", 100, 5),
                new Car("BMW", 300, 4),
                new Car("Audi", 200, 4)
            };
            Array.Sort(array, new SortByName());
            Assert.AreEqual("Audi", array[0].Name);
            Assert.AreEqual("BMW", array[1].Name);
            Assert.AreEqual("Lada", array[2].Name);
        }

        [TestMethod]
        public void FindByName_Existing_ReturnsIndex()
        {
            Assert.AreEqual(1, Search.FindByName(CreateArray(), "BMW"));
        }

        [TestMethod]
        public void FindByName_Missing_ReturnsMinusOne()
        {
            Assert.AreEqual(-1, Search.FindByName(CreateArray(), "Tesla"));
        }

        [TestMethod]
        public void BinarySearchByName_FindsEveryElement()
        {
            TransportVehicle[] array = CreateArray();
            Array.Sort(array, new SortByName());
            for (int i = 0; i < array.Length; i++)
            {
                int index = Search.BinarySearchByName(array, array[i].Name);
                Assert.AreEqual(array[i].Name, array[index].Name);
            }
        }

        [TestMethod]
        public void BinarySearchByName_Missing_ReturnsMinusOne()
        {
            TransportVehicle[] array = CreateArray();
            Array.Sort(array, new SortByName());
            Assert.AreEqual(-1, Search.BinarySearchByName(array, "Tesla"));
        }

        [TestMethod]
        public void BinarySearchByName_EmptyArray_ReturnsMinusOne()
        {
            Assert.AreEqual(-1, Search.BinarySearchByName(new TransportVehicle[0], "Lada"));
        }

        [TestMethod]
        public void BinarySearchByName_SingleElement()
        {
            TransportVehicle[] array = { new Car("Lada", 100, 5) };
            Assert.AreEqual(0, Search.BinarySearchByName(array, "Lada"));
        }

        [TestMethod]
        public void IInit_ArrayWithDifferentClasses_RandomInitWorks()
        {
            IInit[] items = { new Car(), new Train(), new Express(), new Driver() };
            foreach (IInit item in items)
            {
                item.RandomInit();
            }
            Assert.IsTrue(((Car)items[0]).Power >= 70);
            Assert.IsTrue(((Train)items[1]).Power >= 1000);
            Assert.IsTrue(((Express)items[2]).PassengersPerWagon >= 40);
            Assert.IsFalse(string.IsNullOrEmpty(((Driver)items[3]).Name));
        }
    }
}