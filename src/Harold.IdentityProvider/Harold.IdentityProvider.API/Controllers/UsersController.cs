using AutoMapper;
using FluentValidation.AspNetCore;
using Harold.IdentityProvider.API.Filters;
using Harold.IdentityProvider.IService;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Harold.IdentityProvider.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
            
        public UsersController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] [CustomizeValidator(RuleSet = ("authenticate"))] UsersRequest userRequest)
        {
            var result = _usersService.Authenticate(userRequest.Username, userRequest.Password);
            if (!result.Success) return BadRequest(result.Message);
            return Ok(result.Data);
        }

        [HttpPost]
        [ValidateModelState]
        public IActionResult Create([FromBody] [CustomizeValidator(RuleSet = ("default,create"))] UsersRequest userRequest)
        {
            var user = _mapper.Map<Users>(userRequest);
            var result = _usersService.Register(user, userRequest.Password);
            if (!result.Success) return BadRequest(result.Message);
            return NoContent();
        }
    }
}