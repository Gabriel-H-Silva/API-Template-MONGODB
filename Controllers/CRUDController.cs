
using Business.Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Generic;
using ModelsIM;

namespace Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class CRUDController : ControllerBase
    {
        private ICrudBusiness _business;

        public CRUDController(ICrudBusiness business)
        {
            _business = business;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            ResultDM result = new ResultDM();
            result.Req = "GET";
            result = await _business.GetAll(result);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID([FromRoute] string id)
        {
            ResultDM result = new ResultDM();
            result.Req = "GET";
            result = await _business.GetAll(result);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> SendToSave([FromBody] CrudIM crudIM)
        {
            ResultDM result = new ResultDM();
            result.Req = crudIM;
            result = await _business.SendToSave(result);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveById([FromRoute] string id)
        {
            ResultDM result = new ResultDM();
            result.Req = id;
            result = await _business.RemoveById(result);
            return Ok(result);
        }

    }
}
