using MongoDB.Driver;
using System;

namespace Mongo
{
    public class MongoRepository<T>
    {
        private readonly IMongoConnectionSettings _settings;

        public MongoRepository(IMongoConnectionSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException("settings");
            }
            _settings = settings;
        }

        public void Insert(T document)
        {
            //var mongoClient = new MongoClient(_settings.ConnectionString);
            //var db = mongoClient.GetDatabase(_settings.DatabaseName);
            //var coll = db.GetCollection<T>(_settings.CollectionName);
            //coll.InsertOne(document);
        }
    }
}
