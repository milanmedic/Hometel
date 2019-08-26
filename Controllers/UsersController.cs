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

namespace Hometel.Controllers
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
            userDto.Role = "Guest"; //MySQL doesn't allow string fields to have default values

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<Guest>(userDto);

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
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto){

            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password)){
                  return BadRequest("Username or password is empty");
             }   
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = await _userService.Authenticate(loginDto.Username, loginDto.Password);
            if(user == null){
                return BadRequest("Username or password is incorrect");
            }

            var response = _mapper.Map<UserResponse>(user);

            return Ok(response);
        }
        [Authorize]
        [HttpGet("view_profile/{username}")]
        public async Task<IActionResult> ViewMyData([FromRoute] string username){
            // Find user profile
            if(string.IsNullOrEmpty(username)){
                return BadRequest("The provided username doesn't exist");
            }
            var user = await _userService.GetUserAsync(username);
            if(user == null){
                return BadRequest("There has been a problem while returning your data");
            }
            var userResponse = _mapper.Map<UserResponse>(user);
            return Ok(userResponse);
        }
        [Authorize]
        [HttpPost("change_user_data")]
        public async Task<IActionResult> ChangeUserData([FromBody] UserDto userDataDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            if(await _userService.Authenticate(userDataDto.Username, userDataDto.Password) == null){
                return BadRequest("Username or password is incorrect");
            }
            var user = _mapper.Map<User>(userDataDto);

            var updatedUser = await _userService.UpdateUserDataAsync(user, userDataDto.Password);
            if(updatedUser == null){
                return BadRequest("Something went wrong while updating your data");
            }
            var userResponse = _mapper.Map<UserResponse>(updatedUser);
            return Ok(userResponse);
        }
    }
}
