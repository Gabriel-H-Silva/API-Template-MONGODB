using Business.Interface;
using Model.Generic;
using ModelsIM;
using ModelsOM;
using Repository;

namespace Business
{
    public class CrudBusiness : ICrudBusiness
    {
        private CrudRepository _crudRepository;

        public CrudBusiness(CrudRepository crudRepository)
        {
            _crudRepository = crudRepository;
        }

        public async Task<ResultDM> GetAll(ResultDM result)
        {
            List<CrudOM> crud = await _crudRepository.GetAll();
            if(crud.Count > 0)
            {
                result.Res = crud;
            }
            else
            {
                result.Information.Status = 1;
                result.Information.Message = "Dados não encontrado";
            }

            return result;
        }

        public async Task<ResultDM> SendToSave(ResultDM result)
        {
            CrudIM crudIM = (CrudIM)result.Req;

            CrudOM crudOM = new CrudOM();

            if(string.IsNullOrEmpty(crudIM.Id)) crudOM.Id = crudIM.Id;
            if(string.IsNullOrEmpty(crudIM.Name)) crudOM.Name = crudIM.Name;


            result.Saved = await _crudRepository.Save(crudOM);

            if (result.Saved != true)
            {
                result.Res = crudIM;
            }
            else
            {
                result.Information.Status = 0;
                result.Information.Message = "Falha no Salvar";
            }

            return result;
        }
    }
}
