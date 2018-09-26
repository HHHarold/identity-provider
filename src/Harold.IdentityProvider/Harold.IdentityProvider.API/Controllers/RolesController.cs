using FluentValidation.AspNetCore;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Harold.IdentityProvider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{rolId}")]
        public IActionResult GetById([FromRoute]int rolId)
        {
            var rol = _unitOfWork.Roles.GetById(rolId);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var roles = _unitOfWork.Roles.Get(orderBy: "Name");
            if (roles.Count() == 0) return NotFound();
            return Ok(roles);
        }        

        [HttpPost]
        public IActionResult Create([FromBody] [CustomizeValidator(RuleSet=("default, create"))] Roles rol)
        {
            _unitOfWork.Roles.Create(rol);
            _unitOfWork.Save();
            return CreatedAtRoute("GetById",rol.RolId,rol);
        }

        [HttpPut("{rolId}")]
        public IActionResult Update(int rolId, [FromBody] [CustomizeValidator(RuleSet = ("default, update"))] Roles rol)
        {
            var dbRol = _unitOfWork.Roles.GetById(rolId);
            if (dbRol == null) return NotFound();
            _unitOfWork.Roles.Update(rol);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{rolId}")]
        public IActionResult Delete([FromRoute] int rolId)
        {
            var logins = _unitOfWork.Logins.Get(filter: l => l.RoleId == rolId);
            if (logins.Any()) return BadRequest("Can't delete rol. It has related logins. Delete those logins first.");
            var rol = _unitOfWork.Roles.GetById(rolId);
            if (rol == null) return NotFound();
            _unitOfWork.Roles.Delete(rol);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}