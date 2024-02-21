using Flunt.Notifications;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace Person.Domain.Core
{
    public abstract class Entity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; protected set; }
        public Entity()
        {
        }

        public Entity(string id)
        {
            Id = id;
        }
    }
}