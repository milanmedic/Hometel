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
    public class AdminsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public AdminsController(IUserService userService, IMapper mapper){
            _userService = userService; 
            _mapper = mapper;
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("register_hosts")]
        public async Task<IActionResult> RegisterHosts([FromBody] UserDto userDto){
            userDto.Role = "Host"; //MySQL doesn't allow string fields to have default values

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            var user = _mapper.Map<Host>(userDto);

            try {

                var result = await _userService.RegisterUserAsync(user, userDto.Password);
                var userResponse = await _userService.GetUserAsync(user.Username);
                var response = _mapper.Map<UserResponse>(userResponse);
                return Ok(response);

            } catch(AppException ex){

                return BadRequest(new {message = ex.Message});
            }
        }

        //Admin list users
        [Authorize(Roles = Role.Admin)]
        [HttpGet("get_users")]
        public async Task<IActionResult> ListAllUsers(){
            // Get all users from Repository
            var users = await _userService.GetAllUsersAsync();
            // convert all users to UserResponse
            List<UserResponse> userResponses = new List<UserResponse>();
            foreach(var user in users){
                var userResponse = _mapper.Map<UserResponse>(user);
                userResponses.Add(userResponse);
            }
            //Return list of user responses
            return Ok(userResponses);
        }
    }
}
