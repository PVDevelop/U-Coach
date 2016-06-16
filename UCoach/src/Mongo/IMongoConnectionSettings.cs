namespace Mongo
{
    /// <summary>
    /// Настройки подключения к MongoDb
    /// </summary>
    public class IMongoConnectionSettings
    {
        public string ConnectionString { get; }
        public string DatabaseName { get; }
        public string CollectionName { get;}
    }
}
