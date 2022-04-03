using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class MongoDBService {

    private readonly IMongoCollection<Locations> _locationCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings) {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _locationCollection = database.GetCollection<Locations>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<Locations>> GetAsync() { 
        return await _locationCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task CreateAsync(Locations location) { 
        await _locationCollection.InsertOneAsync(location);
        return;

    }

    public async Task AddToLocationsAsync(string id, string areaId) {
        FilterDefinition<Locations> filter = Builders<Locations>.Filter.Eq("Id", id);
        UpdateDefinition<Locations> update = Builders<Locations>.Update.AddToSet<string>("areaIds", areaId);
        await _locationCollection.UpdateOneAsync(filter, update);
        return;

    }

    public async Task DeleteAsync(string id) { 
        FilterDefinition<Locations> filter = Builders<Locations>.Filter.Eq("Id", id);
        await _locationCollection.DeleteOneAsync(filter);
        return;
    }
}