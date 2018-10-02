using AutoMapper;
using Harold.IdentityProvider.IService;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Requests;
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

        [HttpPost("register")]
        public IActionResult Register([FromBody] UsersRequest userRequest)
        {
            var user = _mapper.Map<Users>(userRequest);
            var result = _usersService.Register(user, userRequest.Password);
            if (!result.Success) return BadRequest(result.Message);
            return NoContent();
        }
    }
}