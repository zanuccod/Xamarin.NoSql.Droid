using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LiteDb.Common.Entities;
using LiteDb.Common.Models;
using NUnit.Framework;
using SqLitePcl.Common.Models;

namespace LiteDb.Common.Test
{
    [TestFixture]
    public class PerformanceTest
    {
        private CarLiteDbDataStore liteDb;
        private const string liteDbPath = "dbLiteDbTest";

        private CarSqLiteDataStore sqlDb;
        private const string sqlDbPath = "dbSqLiteTest";

        private const int itemsCount = 100000;
        private List<Car> items;

        [TestFixtureSetUp]
        public void BeforeAllTest()
        {
            liteDb = new CarLiteDbDataStore(liteDbPath);
            sqlDb = new CarSqLiteDataStore(sqlDbPath);

            items = new List<Car>();
            Car item;
            for (int i = 0; i < itemsCount; i++)
            {
                item = new Car()
                {
                    Id = i,
                    Model = "testModel",
                    Productor = "testProductor",
                    Year = 1970 + i
                };
                items.Add(item);
            }
        }

        [TearDown]
        public void AfterEachTest()
        {
            // delete all database files generated for test
            DeleteFiles(liteDbPath);
            DeleteFiles(sqlDbPath);
        }

        private void DeleteFiles(string fileName)
        {
            // delete all database files generated for test
            var files = Directory.GetFiles(Path.GetDirectoryName(Path.GetFullPath(fileName)), fileName + ".*");
            foreach (var file in files)
                File.Delete(file);
        }

        [Test]
        public void LiteDbInsertTest()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();

            items.ForEach(x => Task.FromResult(liteDb.AddItem(x)));

            startTime.Stop();

            Console.WriteLine($"LiteDb elapsed ticks to insert {itemsCount} items: {startTime.ElapsedTicks}");
        }

        [Test]
        public void SqlDbInsertTest()
        {
            var startTime = System.Diagnostics.Stopwatch.StartNew();

            items.ForEach(x => Task.FromResult(sqlDb.AddItem(x)));

            startTime.Stop();

            Console.WriteLine($"SqlDb elapsed ticks to insert {itemsCount} items: {startTime.ElapsedTicks}");
        }

    }
}
