using Business.Interface;
using Model.Generic;
using ModelsIM;
using ModelsOM;
using Repository.Interface;

namespace Business
{
    public class CrudBusiness : ICrudBusiness
    {
        private ICrudRepository _crudRepository;
        private Information information = new Information();

        public CrudBusiness(ICrudRepository crudRepository)
        {
            _crudRepository = crudRepository;
        }

        public async Task<ResultDM> GetAll(ResultDM result)
        {
            try
            {
                List<CrudOM> crud = await _crudRepository.GetAll();

                if (crud.Count > 0)
                {
                    result.Res = crud;
                    result.Information = information.ResultInformation("CRUD", (int)EnumResultDM.EventCode.CodeGet, (int)EnumResultDM.StatusCode.StatusSuccess);

                }
                else
                {
                    result.Information = information.ResultInformation("Não foi encontrado nenhum CRUD no banco", (int)EnumResultDM.EventCode.CodeCustom, (int)EnumResultDM.StatusCode.StatusWarning);
                }
            }
            catch (Exception e)
            {
                result.Information = information.ResultInformation("CRUD", (int)EnumResultDM.EventCode.CodeSave, (int)EnumResultDM.StatusCode.StatusError);
                result.Res = e.Message;
            }


            return result;
        }

        public async Task<ResultDM> GetById(ResultDM result)
        {
            string id = (string)result.Req;

            try
            {

                CrudOM crud = await _crudRepository.GetById(id);
                if (crud != null)
                {
                    result.Res = crud;
                }
                else
                {
                    result.Information = information.ResultInformation("Registro não encontrado", (int)EnumResultDM.EventCode.CodeCustom, (int)EnumResultDM.StatusCode.StatusWarning);
                }
            }
            catch (Exception e)
            {
                result.Information = information.ResultInformation("CRUD", (int)EnumResultDM.EventCode.CodeSave, (int)EnumResultDM.StatusCode.StatusError);
                result.Res = e.Message;
            }

            return result;
        }

        public async Task<ResultDM> RemoveById(ResultDM result)
        {
            string id = (string)result.Req;

            try
            {
                CrudOM crud = await _crudRepository.GetById(id);
                if (crud != null)
                {
                    await _crudRepository.RemoveById(id);
                    result.Res = "Removido";

                    result.Information = information.ResultInformation("CRUD", (int)EnumResultDM.EventCode.CodeRemove, (int)EnumResultDM.StatusCode.StatusSuccess);
                }
                else
                {
                    result.Information = information.ResultInformation("Registro não encontrado", (int)EnumResultDM.EventCode.CodeCustom, (int)EnumResultDM.StatusCode.StatusWarning);
                }

            }
            catch (Exception e)
            {
                result.Information = information.ResultInformation("CRUD", (int)EnumResultDM.EventCode.CodeSave, (int)EnumResultDM.StatusCode.StatusError);
                result.Res = e.Message;
            }

            return result;
        }

        public async Task<ResultDM> SendToSave(ResultDM result)
        {

            CrudIM crudIM = (CrudIM)result.Req;

            try
            {
                CrudOM crudOM = new CrudOM();

                if (!string.IsNullOrEmpty(crudIM.Id)) crudOM.Id = crudIM.Id;
                if (!string.IsNullOrEmpty(crudIM.Name)) crudOM.Name = crudIM.Name;


                result.Res = await _crudRepository.Save(crudOM);
                result.Information = information.ResultInformation("CRUD", (int)EnumResultDM.EventCode.CodeSave, (int)EnumResultDM.StatusCode.StatusSuccess);

            }
            catch (Exception e)
            {
                result.Information = information.ResultInformation("CRUD", (int)EnumResultDM.EventCode.CodeSave, (int)EnumResultDM.StatusCode.StatusError);
                result.Res = e.Message;
            }

            return result;
        }
    }
}
