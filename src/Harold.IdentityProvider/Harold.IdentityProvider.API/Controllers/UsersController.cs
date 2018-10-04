using AutoMapper;
using FluentValidation.AspNetCore;
using Harold.IdentityProvider.API.Filters;
using Harold.IdentityProvider.API.Models;
using Harold.IdentityProvider.IService;
using Harold.IdentityProvider.Model.Models;
using Harold.IdentityProvider.Model.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Harold.IdentityProvider.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
            
        public UsersController(IUsersService usersService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _usersService = usersService;
            _mapper = mapper;
            _appSettings = appSettings.Value;            
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] [CustomizeValidator(RuleSet = ("authenticate"))] UsersRequest userRequest)
        {
            var result = _usersService.Authenticate(userRequest.Username, userRequest.Password);
            if (!result.Success) return BadRequest(result.Message);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, result.Data.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);            
            result.Data.Token = tokenHandler.WriteToken(token);

            return Ok(result.Data);
        }

        [AllowAnonymous]
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