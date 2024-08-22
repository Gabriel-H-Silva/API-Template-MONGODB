using ModelsOM;
using Mongo.Services;
using MongoDB.Driver;
using Repository.Interface;

namespace Repository
{
    public class CrudRepository : ICrudRepository
    {
        private readonly IMongoCollection<CrudOM> _crudCollection;

        public CrudRepository(MongoDBService mongoDBService)
        {
            _crudCollection = mongoDBService.GetCollection<CrudOM>("CRUD");
        }
        public async Task<List<CrudOM>> GetAll() => await _crudCollection.Find(_ => true).ToListAsync();

        public async Task<bool> Save(CrudOM crud)
        {
            if (string.IsNullOrEmpty(crud.Id))
            {
                await _crudCollection.InsertOneAsync(crud);
                return true;
            }
            else
            {
                var filter = Builders<CrudOM>.Filter.Eq(doc => doc.Id, crud.Id);
                var updateResult = await _crudCollection.ReplaceOneAsync(filter, crud);
                return true;
            }
        }
    }
}
