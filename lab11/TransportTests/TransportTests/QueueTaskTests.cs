using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransportLibrary;

namespace TransportTests
{
    [TestClass]
    public class QueueTasksTests
    {

        private static Queue<TransportVehicle> CreateQueue()
        {
            Queue<TransportVehicle> queue = new Queue<TransportVehicle>();
            QueueTasks.Add(queue, new Car("Lada", 100, 5));
            QueueTasks.Add(queue, new Car("BMW", 300, 4));
            QueueTasks.Add(queue, new Train("Грузовой", 3000, 10));
            QueueTasks.Add(queue, new Express("Сапсан", 8000, 10, 50));
            return queue;
        }

        [TestMethod]
        public void Add_PutsObjectToTheEnd()
        {
            Queue<TransportVehicle> queue = CreateQueue();
            Car car = new Car("Новый", 150, 4);
            QueueTasks.Add(queue, car);
            Assert.AreEqual(5, queue.Count);
            TransportVehicle[] array = queue.ToArray();
            Assert.AreSame(car, array[array.Length - 1]);
        }

        [TestMethod]
        public void RemoveFirst_ReturnsFirstAddedObject()
        {
            Queue<TransportVehicle> queue = CreateQueue();
            TransportVehicle removed = QueueTasks.RemoveFirst(queue);
            Assert.AreEqual("Lada", removed.Name);
            Assert.AreEqual(3, queue.Count);
            Assert.AreEqual("BMW", queue.Peek().Name);
        }

        [TestMethod]
        public void RemoveFirst_EmptyQueue_ReturnsNull()
        {
            Assert.IsNull(QueueTasks.RemoveFirst(new Queue<TransportVehicle>()));
        }

        [TestMethod]
        public void CountByKind_Cars()
        {
            Assert.AreEqual(2, QueueTasks.CountByKind(CreateQueue(), 1));
        }

        [TestMethod]
        public void CountByKind_Trains_IncludesExpress()
        {
            Assert.AreEqual(2, QueueTasks.CountByKind(CreateQueue(), 2));
        }

        [TestMethod]
        public void CountByKind_Expresses()
        {
            Assert.AreEqual(1, QueueTasks.CountByKind(CreateQueue(), 3));
        }

        [TestMethod]
        public void CountByKind_UnknownKind_ReturnsZero()
        {
            Assert.AreEqual(0, QueueTasks.CountByKind(CreateQueue(), 0));
        }

        [TestMethod]
        public void GetByKind_ReturnsOnlyCars()
        {
            List<TransportVehicle> cars = QueueTasks.GetByKind(CreateQueue(), 1);
            Assert.AreEqual(2, cars.Count);
            Assert.AreEqual("Lada", cars[0].Name);
            Assert.AreEqual("BMW", cars[1].Name);
        }

        [TestMethod]
        public void TotalExpressPassengers_SumsExpresses()
        {
            Assert.AreEqual(500, QueueTasks.TotalExpressPassengers(CreateQueue()));
        }

        [TestMethod]
        public void TotalExpressPassengers_NoExpress_ReturnsZero()
        {
            Queue<TransportVehicle> queue = new Queue<TransportVehicle>();
            queue.Enqueue(new Car("Lada", 100, 5));
            Assert.AreEqual(0, QueueTasks.TotalExpressPassengers(queue));
        }

        [TestMethod]
        public void ShallowCopy_SharesObjects()
        {
            Queue<TransportVehicle> queue = CreateQueue();
            Queue<TransportVehicle> copy = QueueTasks.ShallowCopy(queue);
            Assert.AreNotSame(queue, copy);
            Assert.AreEqual(queue.Count, copy.Count);
            Assert.AreSame(queue.Peek(), copy.Peek());
        }

        [TestMethod]
        public void ShallowCopy_RemovingFromCopyDoesNotChangeOriginal()
        {
            Queue<TransportVehicle> queue = CreateQueue();
            Queue<TransportVehicle> copy = QueueTasks.ShallowCopy(queue);
            copy.Dequeue();
            Assert.AreEqual(4, queue.Count);
            Assert.AreEqual(3, copy.Count);
        }

        [TestMethod]
        public void DeepCopy_ObjectsAreIndependent()
        {
            Queue<TransportVehicle> queue = CreateQueue();
            Queue<TransportVehicle> deep = QueueTasks.DeepCopy(queue);
            Assert.AreEqual(queue.Count, deep.Count);
            Assert.AreNotSame(queue.Peek(), deep.Peek());
            Assert.IsTrue(queue.Peek().Equals(deep.Peek()));
        }

        [TestMethod]
        public void DeepCopy_ChangeInOriginalNotVisibleInCopy()
        {
            Queue<TransportVehicle> queue = CreateQueue();
            Queue<TransportVehicle> deep = QueueTasks.DeepCopy(queue);
            queue.Peek().Name = "Изменено";
            Assert.AreEqual("Lada", deep.Peek().Name);
        }

        [TestMethod]
        public void GetSortedByPower_ReturnsNewSortedQueue()
        {
            Queue<TransportVehicle> queue = new Queue<TransportVehicle>();
            queue.Enqueue(new Car("BMW", 300, 4));
            queue.Enqueue(new Car("Lada", 100, 5));
            Queue<TransportVehicle> sorted = QueueTasks.GetSortedByPower(queue);
            Assert.AreEqual("Lada", sorted.Peek().Name);
            Assert.AreEqual("BMW", queue.Peek().Name);   // исходная очередь не изменилась
        }

        [TestMethod]
        public void GetSortedByName_ReturnsNewSortedQueue()
        {
            Queue<TransportVehicle> queue = new Queue<TransportVehicle>();
            queue.Enqueue(new Car("Lada", 100, 5));
            queue.Enqueue(new Car("BMW", 300, 4));
            Queue<TransportVehicle> sorted = QueueTasks.GetSortedByName(queue);
            Assert.AreEqual("BMW", sorted.Peek().Name);
            Assert.AreEqual("Lada", queue.Peek().Name);
        }

        [TestMethod]
        public void FindByName_Existing_ReturnsObject()
        {
            TransportVehicle found = QueueTasks.FindByName(CreateQueue(), "BMW");
            Assert.IsNotNull(found);
            Assert.AreEqual(300, found.Power);
        }

        [TestMethod]
        public void FindByName_Missing_ReturnsNull()
        {
            Assert.IsNull(QueueTasks.FindByName(CreateQueue(), "Tesla"));
        }

        [TestMethod]
        public void BinarySearchByName_FindsEveryElement()
        {
            Queue<TransportVehicle> sorted = QueueTasks.GetSortedByName(CreateQueue());
            TransportVehicle[] array = sorted.ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                int index = QueueTasks.BinarySearchByName(sorted, array[i].Name);
                Assert.AreEqual(array[i].Name, array[index].Name);
            }
        }

        [TestMethod]
        public void BinarySearchByName_Missing_ReturnsMinusOne()
        {
            Queue<TransportVehicle> sorted = QueueTasks.GetSortedByName(CreateQueue());
            Assert.AreEqual(-1, QueueTasks.BinarySearchByName(sorted, "Tesla"));
        }

        [TestMethod]
        public void Contains_SearchesByContent()
        {
            Queue<TransportVehicle> queue = CreateQueue();
            Assert.IsTrue(QueueTasks.Contains(queue, new Car("Lada", 100, 5)));
            Assert.IsFalse(QueueTasks.Contains(queue, new Car("Lada", 100, 4)));
        }
    }
}