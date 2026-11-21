using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportLibrary;

namespace TransportTests
{
    [TestClass]
    public class HashtableTasksTests
    {
       
        private static Hashtable CreateTable()
        {
            Hashtable table = new Hashtable();
            HashtableTasks.Add(table, new Car("Lada", 100, 5));
            HashtableTasks.Add(table, new Car("BMW", 300, 4));
            HashtableTasks.Add(table, new Train("Грузовой", 3000, 10));
            HashtableTasks.Add(table, new Express("Сапсан", 8000, 10, 50));
            return table;
        }

        [TestMethod]
        public void NextKey_EmptyTable_ReturnsOne()
        {
            Assert.AreEqual(1, HashtableTasks.NextKey(new Hashtable()));
        }

        [TestMethod]
        public void NextKey_FilledTable_ReturnsMaxPlusOne()
        {
            Assert.AreEqual(5, HashtableTasks.NextKey(CreateTable()));
        }

        [TestMethod]
        public void NextKey_AfterRemovingLast_ReusesKey()
        {
            Hashtable table = CreateTable();
            HashtableTasks.Remove(table, 4);
            Assert.AreEqual(4, HashtableTasks.NextKey(table));
        }

        [TestMethod]
        public void Add_ReturnsKeyAndStoresObject()
        {
            Hashtable table = new Hashtable();
            Car car = new Car("Lada", 100, 5);
            int key = HashtableTasks.Add(table, car);
            Assert.AreEqual(1, key);
            Assert.AreEqual(1, table.Count);
            Assert.AreSame(car, table[key]);
        }

        [TestMethod]
        public void Add_TwoObjects_GetDifferentKeys()
        {
            Hashtable table = new Hashtable();
            int k1 = HashtableTasks.Add(table, new Car());
            int k2 = HashtableTasks.Add(table, new Car());
            Assert.AreNotEqual(k1, k2);
            Assert.AreEqual(2, table.Count);
        }

        [TestMethod]
        public void Remove_ExistingKey_ReturnsTrue()
        {
            Hashtable table = CreateTable();
            Assert.IsTrue(HashtableTasks.Remove(table, 2));
            Assert.AreEqual(3, table.Count);
            Assert.IsFalse(table.ContainsKey(2));
        }

        [TestMethod]
        public void Remove_MissingKey_ReturnsFalse()
        {
            Hashtable table = CreateTable();
            Assert.IsFalse(HashtableTasks.Remove(table, 99));
            Assert.AreEqual(4, table.Count);
        }

        [TestMethod]
        public void CountByKind_Cars()
        {
            Assert.AreEqual(2, HashtableTasks.CountByKind(CreateTable(), 1));
        }

        [TestMethod]
        public void CountByKind_Trains_IncludesExpress()
        {
            Assert.AreEqual(2, HashtableTasks.CountByKind(CreateTable(), 2));
        }

        [TestMethod]
        public void CountByKind_Expresses()
        {
            Assert.AreEqual(1, HashtableTasks.CountByKind(CreateTable(), 3));
        }

        [TestMethod]
        public void CountByKind_UnknownKind_ReturnsZero()
        {
            Assert.AreEqual(0, HashtableTasks.CountByKind(CreateTable(), 9));
        }

        [TestMethod]
        public void GetByKind_ReturnsOnlyCars()
        {
            ArrayList cars = HashtableTasks.GetByKind(CreateTable(), 1);
            Assert.AreEqual(2, cars.Count);
            foreach (object o in cars)
            {
                Assert.IsTrue(o is Car);
            }
        }

        [TestMethod]
        public void GetByKind_NoSuchKind_ReturnsEmpty()
        {
            Hashtable table = new Hashtable();
            HashtableTasks.Add(table, new Car("Lada", 100, 5));
            Assert.AreEqual(0, HashtableTasks.GetByKind(table, 3).Count);
        }

        [TestMethod]
        public void AveragePower_Cars()
        {
            Assert.AreEqual(200.0, HashtableTasks.AveragePower(CreateTable(), 1), 0.001);
        }

        [TestMethod]
        public void AveragePower_NoSuchKind_ReturnsZero()
        {
            Hashtable table = new Hashtable();
            HashtableTasks.Add(table, new Car("Lada", 100, 5));
            Assert.AreEqual(0.0, HashtableTasks.AveragePower(table, 3), 0.001);
        }

        [TestMethod]
        public void ShallowClone_ObjectsAreShared()
        {
            Hashtable table = CreateTable();
            Hashtable clone = HashtableTasks.ShallowClone(table);
            Assert.AreNotSame(table, clone);
            Assert.AreEqual(table.Count, clone.Count);
            Assert.AreSame(table[1], clone[1]);
        }

        [TestMethod]
        public void ShallowClone_ChangeInOriginalVisibleInClone()
        {
            Hashtable table = CreateTable();
            Hashtable clone = HashtableTasks.ShallowClone(table);
            ((TransportVehicle)table[1]).Name = "Изменено";
            Assert.AreEqual("Изменено", ((TransportVehicle)clone[1]).Name);
        }

        [TestMethod]
        public void DeepClone_ObjectsAreIndependent()
        {
            Hashtable table = CreateTable();
            Hashtable deep = HashtableTasks.DeepClone(table);
            Assert.AreEqual(table.Count, deep.Count);
            Assert.AreNotSame(table[1], deep[1]);
            Assert.IsTrue(table[1].Equals(deep[1]));
        }

        [TestMethod]
        public void DeepClone_ChangeInOriginalNotVisibleInClone()
        {
            Hashtable table = CreateTable();
            Hashtable deep = HashtableTasks.DeepClone(table);
            ((TransportVehicle)table[1]).Name = "Изменено";
            Assert.AreEqual("Lada", ((TransportVehicle)deep[1]).Name);
        }

        [TestMethod]
        public void GetSortedByPower_OrdersAscending()
        {
            Hashtable table = new Hashtable();
            HashtableTasks.Add(table, new Car("Б", 300, 4));
            HashtableTasks.Add(table, new Car("А", 100, 4));
            HashtableTasks.Add(table, new Car("В", 200, 4));
            ArrayList sorted = HashtableTasks.GetSortedByPower(table);
            Assert.AreEqual(100, ((TransportVehicle)sorted[0]).Power);
            Assert.AreEqual(200, ((TransportVehicle)sorted[1]).Power);
            Assert.AreEqual(300, ((TransportVehicle)sorted[2]).Power);
        }

        [TestMethod]
        public void GetSortedByName_OrdersByName()
        {
            ArrayList sorted = HashtableTasks.GetSortedByName(CreateTable());
            Assert.AreEqual("BMW", ((TransportVehicle)sorted[0]).Name);
            Assert.AreEqual("Lada", ((TransportVehicle)sorted[1]).Name);
        }

        [TestMethod]
        public void FindKeyByName_Existing_ReturnsKey()
        {
            Assert.AreEqual(2, HashtableTasks.FindKeyByName(CreateTable(), "BMW"));
        }

        [TestMethod]
        public void FindKeyByName_Missing_ReturnsMinusOne()
        {
            Assert.AreEqual(-1, HashtableTasks.FindKeyByName(CreateTable(), "Tesla"));
        }

        [TestMethod]
        public void ContainsVehicle_SearchesByContent()
        {
            Hashtable table = CreateTable();
            Assert.IsTrue(HashtableTasks.ContainsVehicle(table, new Car("Lada", 100, 5)));
        }

        [TestMethod]
        public void ContainsVehicle_DifferentContent_ReturnsFalse()
        {
            Hashtable table = CreateTable();
            Assert.IsFalse(HashtableTasks.ContainsVehicle(table, new Car("Lada", 100, 4)));
        }
    }
}