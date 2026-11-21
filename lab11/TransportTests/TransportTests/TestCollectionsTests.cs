using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportLibrary;

namespace TransportTests
{
    [TestClass]
    public class TestCollectionsTests
    {
        private static void AssertAllCounts(TestCollections t, int expected)
        {
            Assert.AreEqual(expected, t.Collection1.Count);
            Assert.AreEqual(expected, t.Collection1Str.Count);
            Assert.AreEqual(expected, t.Collection2.Count);
            Assert.AreEqual(expected, t.Collection2Str.Count);
        }

     
        [TestMethod]
        public void Constructor_CreatesRequestedNumberOfElements()
        {
            AssertAllCounts(new TestCollections(100), 100);
        }

        [TestMethod]
        public void Constructor_Zero_CreatesEmptyCollections()
        {
            AssertAllCounts(new TestCollections(0), 0);
        }

        [TestMethod]
        public void Constructor_AllCollectionsContainSameElements()
        {
            TestCollections t = new TestCollections(50);
            foreach (Car car in t.Collection1)
            {
                Assert.IsTrue(t.Collection1Str.Contains(car.ToString()));
                Assert.IsTrue(t.Collection2.ContainsKey(car.BaseVehicle));
                Assert.IsTrue(t.Collection2Str.ContainsKey(car.BaseVehicle.ToString()));
                Assert.IsTrue(t.Collection2[car.BaseVehicle].Equals(car));
            }
        }


        [TestMethod]
        public void Add_NewCar_AddsToAllCollections()
        {
            TestCollections t = new TestCollections(10);
            Assert.IsTrue(t.Add(new Car("Новый автомобиль", 150, 4)));
            AssertAllCounts(t, 11);
        }

        [TestMethod]
        public void Add_SameKey_ReturnsFalseAndChangesNothing()
        {
            TestCollections t = new TestCollections(10);
            Car car = new Car("Новый автомобиль", 150, 4);
            t.Add(car);
            Assert.IsFalse(t.Add(new Car("Новый автомобиль", 150, 2)));
            AssertAllCounts(t, 11);
        }

        [TestMethod]
        public void Remove_ExistingCar_RemovesFromAllCollections()
        {
            TestCollections t = new TestCollections(10);
            Car car = t.GetFirst();
            Assert.IsTrue(t.Remove(car));
            AssertAllCounts(t, 9);
            Assert.IsFalse(t.Collection2.ContainsKey(car.BaseVehicle));
        }

        [TestMethod]
        public void Remove_SameNameAndPowerButOtherSeats_StillRemovesFromAllCollections()
        {
            TestCollections t = new TestCollections(10);
            Car first = t.GetFirst();
            int otherSeats = first.Seats == 1 ? 2 : 1;
            Car other = new Car(first.Name, first.Power, otherSeats);
            Assert.IsTrue(t.Remove(other));
            AssertAllCounts(t, 9);
        }

        [TestMethod]
        public void Remove_MissingCar_ReturnsFalse()
        {
            TestCollections t = new TestCollections(10);
            Assert.IsFalse(t.Remove(new Car("Нет в коллекции", 1, 1)));
            AssertAllCounts(t, 10);
        }

        [TestMethod]
        public void AddThenRemove_ReturnsToStartCount()
        {
            TestCollections t = new TestCollections(10);
            Car car = new Car("Новый автомобиль", 150, 4);
            t.Add(car);
            t.Remove(car);
            AssertAllCounts(t, 10);
        }


        [TestMethod]
        public void GetFirst_ReturnsCopyOfFirstElement()
        {
            TestCollections t = new TestCollections(11);
            Car first = t.GetFirst();
            Assert.IsTrue(first.Equals(t.Collection1[0]));
            Assert.AreNotSame(first, t.Collection1[0]);   
        }

        [TestMethod]
        public void GetMiddle_ReturnsCopyOfMiddleElement()
        {
            TestCollections t = new TestCollections(11);
            Assert.IsTrue(t.GetMiddle().Equals(t.Collection1[5]));
        }

        [TestMethod]
        public void GetLast_ReturnsCopyOfLastElement()
        {
            TestCollections t = new TestCollections(11);
            Assert.IsTrue(t.GetLast().Equals(t.Collection1[10]));
        }

        private static void AssertFound(TestCollections t, Car car, bool expected)
        {
            bool found;
            long ticks;

            ticks = t.TimeContainsInList(car, out found);
            Assert.AreEqual(expected, found);
            Assert.IsTrue(ticks >= 0);

            ticks = t.TimeContainsInStringList(car.ToString(), out found);
            Assert.AreEqual(expected, found);
            Assert.IsTrue(ticks >= 0);

            ticks = t.TimeContainsKey(car.BaseVehicle, out found);
            Assert.AreEqual(expected, found);
            Assert.IsTrue(ticks >= 0);

            ticks = t.TimeContainsKeyString(car.BaseVehicle.ToString(), out found);
            Assert.AreEqual(expected, found);
            Assert.IsTrue(ticks >= 0);

            ticks = t.TimeContainsValue(car, out found);
            Assert.AreEqual(expected, found);
            Assert.IsTrue(ticks >= 0);
        }

        [TestMethod]
        public void Search_FirstElement_FoundEverywhere()
        {
            TestCollections t = new TestCollections(100);
            AssertFound(t, t.GetFirst(), true);
        }

        [TestMethod]
        public void Search_MiddleElement_FoundEverywhere()
        {
            TestCollections t = new TestCollections(100);
            AssertFound(t, t.GetMiddle(), true);
        }

        [TestMethod]
        public void Search_LastElement_FoundEverywhere()
        {
            TestCollections t = new TestCollections(100);
            AssertFound(t, t.GetLast(), true);
        }

        [TestMethod]
        public void Search_MissingElement_NotFoundAnywhere()
        {
            TestCollections t = new TestCollections(100);
            AssertFound(t, new Car("Нет в коллекции", 1, 1), false);
        }
    }
}