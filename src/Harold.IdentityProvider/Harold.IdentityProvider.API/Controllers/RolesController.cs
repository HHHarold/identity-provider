using Harold.IdentityProvider.IService;
using Harold.IdentityProvider.Model.Request;
using Microsoft.AspNetCore.Mvc;

namespace Harold.IdentityProvider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _service;
        public RolesController(IRolesService service)
        {
            _service = service;
        }

        [HttpGet("{rolId}")]
        public IActionResult GetById([FromRoute]int rolId)
        {
            var response = _service.GetById(rolId);
            return Ok(response.Data);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _service.Get(orderBy: "Name");           
            return Ok(response.Data);
        }        

        [HttpPost]
        public IActionResult Create([FromBody] RolesRequest rolesRequest)
        {
            var response = _service.Create(rolesRequest);
            return Ok(response.Data);
        }

        [HttpPut]
        public IActionResult Update([FromBody] RolesRequest rolesRequest)
        {
            var response = _service.Update(rolesRequest);
            return Ok(response.Data);
        }

        [HttpDelete("{rolId}")]
        public IActionResult Delete([FromRoute] int rolId)
        {
            var response = _service.Delete(rolId);
            return Ok(response.Data);
        }


    }
}