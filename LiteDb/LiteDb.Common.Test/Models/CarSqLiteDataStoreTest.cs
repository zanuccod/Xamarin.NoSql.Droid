﻿using System.Collections.Generic;
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
        public void InsertOneElement_Succes()
        {
            var item = new Car() { Id = 1, Model = "modelTest", Productor = "ProductorTest", Year = 1970 };
            Task.FromResult(db.AddItem(item));

            Assert.AreEqual(1, db.GetItemsAsync().Result.Count);
        }

        [Test]
        public void UpdateElement_Succes()
        {
            var item = new Car() { Id = 1, Model = "modelTest", Productor = "ProductorTest", Year = 1970 };
            Task.FromResult(db.AddItem(item));

            // reload element to get item with given Id
            item = db.GetItemsAsync().Result.FirstOrDefault();
            item.Model = "modelTest1";
            Task.FromResult(db.UpdateItemAsync(item));

            Assert.True(item.Equals(db.GetItemsAsync().Result.FirstOrDefault()));
        }

        [Test]
        public void DeleteElement_Success()
        {
            var item = new Car() { Id = 1, Model = "modelTest", Productor = "ProductorTest", Year = 1970 };
            Task.FromResult(db.AddItem(item));

            // reload element to get item with given Id
            item = db.GetItemsAsync().Result.FirstOrDefault();
            Task.FromResult(db.DeleteItemAsync(item));

            Assert.AreEqual(0, db.GetItemsAsync().Result.Count);
        }

        [Test]
        public void DeleteAllAsync_Success()
        {
            // Arrange
            var items = new List<Car>()
            {
                new Car() { Id = 1, Model = "modelTest", Productor = "ProductorTest", Year = 1970 },
                new Car() { Id = 2, Model = "modelTest1", Productor = "ProductorTest1", Year = 1970 },
                new Car() { Id = 3, Model = "modelTest2", Productor = "ProductorTest2", Year = 1970 }
            };

            items.ForEach(x => Task.FromResult(db.AddItem(x)));
            Assert.AreEqual(items.Count, db.GetItemsAsync().Result.Count);

            // Act
            Task.FromResult(db.DeleteAllAsync());

            // Assert
            Assert.AreEqual(0, db.GetItemsAsync().Result.Count);
        }
    }
}
