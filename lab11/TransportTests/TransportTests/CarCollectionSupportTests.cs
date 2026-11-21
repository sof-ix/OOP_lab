using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportLibrary;

namespace TransportTests
{
   
    [TestClass]
    public class CarCollectionSupportTests
    {
        [TestMethod]
        public void Vehicle_ToString_ContainsNameAndPower()
        {
            Assert.AreEqual("Транспорт, 200 л.с.", new TransportVehicle("Транспорт", 200).ToString());
        }

        [TestMethod]
        public void Car_ToString_ContainsSeats()
        {
            Assert.AreEqual("Lada, 100 л.с., мест: 5", new Car("Lada", 100, 5).ToString());
        }

        [TestMethod]
        public void Car_BaseVehicle_ReturnsObjectOfBaseClass()
        {
            Car car = new Car("Lada", 100, 5);
            TransportVehicle baseObject = car.BaseVehicle;
            Assert.AreEqual(typeof(TransportVehicle), baseObject.GetType());
            Assert.AreEqual("Lada", baseObject.Name);
            Assert.AreEqual(100, baseObject.Power);
        }

        [TestMethod]
        public void Car_BaseVehicle_EqualForCarsWithSameNameAndPower()
        {
            Car c1 = new Car("Lada", 100, 5);
            Car c2 = new Car("Lada", 100, 2);
            Assert.IsTrue(c1.BaseVehicle.Equals(c2.BaseVehicle));
            Assert.AreEqual(c1.BaseVehicle.GetHashCode(), c2.BaseVehicle.GetHashCode());
        }

        [TestMethod]
        public void Queries_IsKind_Car()
        {
            Assert.IsTrue(Queries.IsKind(new Car(), 1));
            Assert.IsFalse(Queries.IsKind(new Train(), 1));
        }

        [TestMethod]
        public void Queries_IsKind_ExpressIsAlsoTrain()
        {
            Assert.IsTrue(Queries.IsKind(new Express(), 2));
            Assert.IsTrue(Queries.IsKind(new Express(), 3));
            Assert.IsFalse(Queries.IsKind(new Train(), 3));
        }

        [TestMethod]
        public void Queries_IsKind_UnknownKind_ReturnsFalse()
        {
            Assert.IsFalse(Queries.IsKind(new Car(), 7));
        }
    }
}