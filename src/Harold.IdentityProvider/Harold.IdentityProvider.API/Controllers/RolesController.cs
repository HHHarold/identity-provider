using FluentValidation.AspNetCore;
using Harold.IdentityProvider.API.Filters;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;



namespace Harold.IdentityProvider.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public RolesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("{roleId}", Name = nameof(GetById))]
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
        [ValidateModelState]
        public IActionResult Create([FromBody] [CustomizeValidator(RuleSet = ("default,create"))] Roles rol)
        {
            _unitOfWork.Roles.Create(rol);
            _unitOfWork.Save();
            return CreatedAtRoute(nameof(RolesController.GetById), new { roleId = rol.RoleId }, rol);
        }

        [HttpPut]
        [ValidateModelState]
        public IActionResult Update([FromBody] [CustomizeValidator(RuleSet = ("default,update"))] Roles rol)
        {
            var dbRol = _unitOfWork.Roles.GetById(rol.RoleId);
            if (dbRol == null) return NotFound();
            _unitOfWork.Roles.Update(rol);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{rolId}")]
        public IActionResult Delete([FromRoute] int rolId)
        {
            var users = _unitOfWork.Users.Get(filter: l => l.RoleId == rolId);
            if (users.Any()) return BadRequest("Can't delete rol. It has related logins. Delete those logins first.");
            var rol = _unitOfWork.Roles.GetById(rolId);
            if (rol == null) return NotFound();
            _unitOfWork.Roles.Delete(rol);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}