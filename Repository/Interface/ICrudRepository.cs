using ModelsOM;

namespace Repository.Interface
{
    public interface ICrudRepository
    {
        Task<List<CrudOM>> GetAll();
        Task<CrudOM> GetById(string id);
        Task RemoveById(string id);
        Task<CrudOM> Save(CrudOM crud);
    }
}
