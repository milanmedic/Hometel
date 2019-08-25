using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Hometel.Domain.Models;
using Hometel.Domain.Services;
using Hometel.Domain.Models.Dto;
using AutoMapper;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UsersController(IUserService userService, IMapper mapper){
            _userService = userService; 
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto){
            userDto.Role = "User"; //MySQL doesn't allow string fields to have default values

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<User>(userDto);

            try {

                var result = await _userService.RegisterUserAsync(user, userDto.Password);
                var userResponse = await _userService.GetUserAsync(user.Username);
                var response = _mapper.Map<UserResponse>(userResponse);
                return Ok(response);

            } catch(AppException ex){

                return BadRequest(new {message = ex.Message});

            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto){

            if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password)){
                  return BadRequest("Username or password is empty");
             }   
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = await _userService.Authenticate(userDto.Username, userDto.Password);
            if(user == null){
                return BadRequest("Username or password is incorrect");
            }

            var response = _mapper.Map<UserResponse>(user);

            return Ok(response);
        }
    }
}
