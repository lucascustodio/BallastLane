using MongoDB.Driver;

namespace Person.Infra.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;
        const string databaseName = "PersonDataBase";
        

        public MongoContext(string connectionString)
        {        
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }


        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}