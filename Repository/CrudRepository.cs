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

        public async Task<CrudOM> GetById(string id)
        {
            var filter = Builders<CrudOM>.Filter.Eq(doc => doc.Id, id);
            return await _crudCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task RemoveById(string id)
        {
            var filter = Builders<CrudOM>.Filter.Eq(doc => doc.Id, id);
            await _crudCollection.DeleteOneAsync(filter);
        }

        public async Task<CrudOM> Save(CrudOM crud)
        {
            if (string.IsNullOrEmpty(crud.Id))
            {
                await _crudCollection.InsertOneAsync(crud);
                return crud;
            }
            else
            {
                var filter = Builders<CrudOM>.Filter.Eq(doc => doc.Id, crud.Id);

                var updateResult = await _crudCollection.ReplaceOneAsync(filter, crud);
                return crud;
            }
        }
    }
}
