using MongoDB.Bson;
using MongoDB.Driver;
using Person.Domain.Interfaces;
using Person.Infra.Data;

public abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;

    public Repository(MongoContext mongoContext, string collectionName)
    {
        _collection = mongoContext.GetCollection<T>(collectionName);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<List<T>> GetAll()
    {
        var result = await _collection.FindAsync(Builders<T>.Filter.Empty);
        return await result.ToListAsync();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        var objectId = ParseObjectId(id);
        if (objectId == ObjectId.Empty)
        {
            return null;
        }

        var filter = Builders<T>.Filter.Eq("_id", objectId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task Remove(string id)
    {
        var objectId = ParseObjectId(id);
        if (objectId == ObjectId.Empty)
        {
            return;
        }

        var filter = Builders<T>.Filter.Eq("_id", objectId);
        await _collection.DeleteOneAsync(filter);
    }

    public async Task<T> Update(string id, T entity)
    {
        var objectId = ParseObjectId(id);
        if (objectId == ObjectId.Empty)
        {
            return null;
        }

        var filter = Builders<T>.Filter.Eq("_id", objectId);
        var options = new FindOneAndReplaceOptions<T> { ReturnDocument = ReturnDocument.After };
        return await _collection.FindOneAndReplaceAsync(filter, entity, options);
    }

    private ObjectId ParseObjectId(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            return ObjectId.Empty;
        }
        return objectId;
    }
}
