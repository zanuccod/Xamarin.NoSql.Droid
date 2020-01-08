using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDb.Common.Entities;
using NUnit.Framework;
using SqLitePcl.Common.Models;

namespace LiteDb.Common.Test.Models
{
    [TestFixture]
    public class CarSqLiteDataStoreTest
    {
        private CarSqLiteDataStore db;
        private const string dbPath = "dbSqLiteTest";

        [SetUp]
        public void BeforeEachTest()
        {
            db = new CarSqLiteDataStore(dbPath + ".db3");
        }

        [TearDown]
        public void AfterEachTest()
        {
            // delete all database files generated for test
            var files = Directory.GetFiles(Path.GetDirectoryName(Path.GetFullPath(dbPath)), dbPath + ".*");
            foreach (var file in files)
                File.Delete(file);
        }

        [Test]
        public async Task InsertOneElement_Succes()
        {
            var item = new Car() { Id = 1, Model = "modelTest", Productor = "ProductorTest", Year = 1970 };
            await db.AddItemAsync(item).ConfigureAwait(true);

            Assert.AreEqual(1, db.GetItemsAsync().Result.Count);
        }

        [Test]
        public async Task UpdateElement_Succes()
        {
            var item = new Car() { Id = 1, Model = "modelTest", Productor = "ProductorTest", Year = 1970 };
            await db.AddItemAsync(item).ConfigureAwait(true);

            // reload element to get item with given Id
            item = db.GetItemsAsync().Result.FirstOrDefault();
            item.Model = "modelTest1";
            await db.UpdateItemAsync(item);

            Assert.True(item.Equals(db.GetItemsAsync().Result.FirstOrDefault()));
        }

        [Test]
        public async Task DeleteElement_Success()
        {
            var item = new Car() { Id = 1, Model = "modelTest", Productor = "ProductorTest", Year = 1970 };
            await db.AddItemAsync(item).ConfigureAwait(true);

            // reload element to get item with given Id
            item = db.GetItemsAsync().Result.FirstOrDefault();
            await db.DeleteItemAsync(item);

            Assert.AreEqual(0, db.GetItemsAsync().Result.Count);
        }

        [Test]
        public async Task DeleteAllAsync_Success()
        {
            // Arrange
            var items = new List<Car>()
            {
                new Car() { Id = 1, Model = "modelTest", Productor = "ProductorTest", Year = 1970 },
                new Car() { Id = 2, Model = "modelTest1", Productor = "ProductorTest1", Year = 1970 },
                new Car() { Id = 3, Model = "modelTest2", Productor = "ProductorTest2", Year = 1970 }
            };

            items.ForEach(x => db.AddItemAsync(x).ConfigureAwait(true));
            Assert.AreEqual(items.Count, db.GetItemsAsync().Result.Count);

            // Act
            await db.DeleteAllAsync();

            // Assert
            Assert.AreEqual(0, db.GetItemsAsync().Result.Count);
        }
    }
}
