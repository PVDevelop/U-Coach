using MongoDB.Driver;
using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace Mongo.Tests
{
    [TestFixture]
    public class MongoRepositoryIntegrTests
    {
        private class TestObj
        {
            public string Name { get; set; }
            public Guid Id { get; set; }
        }

        [Test]
        public void Insert_ValidObject_SavesToDb()
        {
            var settings = MockRepository.GenerateStub<IMongoConnectionSettings>();
            settings.Stub(s => s.DatabaseName).Return(Guid.NewGuid().ToString());
            settings.Stub(s => s.CollectionName).Return("test_objects");
            settings.Stub(s => s.ConnectionString).Return("mongodb://localhost");

            var testObj = new TestObj()
            {
                Id = Guid.NewGuid(),
                Name = "SomeName"
            };

            var rep = new MongoRepository<TestObj>(settings);
            rep.Insert(testObj);

            var coll = 
                new MongoClient(settings.ConnectionString).
                GetDatabase(settings.DatabaseName).
                GetCollection<TestObj>(settings.CollectionName);

            var foundObj = coll.Find(o => o.Id == testObj.Id && o.Name == testObj.Name).FirstOrDefault();
            Assert.NotNull(foundObj, "Объект не был сохранен в MongoDb");
        }
    }
}
