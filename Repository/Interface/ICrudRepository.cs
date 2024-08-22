using ModelsOM;

namespace Repository.Interface
{
    public interface ICrudRepository
    {
        Task<List<CrudOM>> GetAll();
        Task<bool> Save(CrudOM crud);
    }
}
