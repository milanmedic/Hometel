using System.Collections.Generic;
using System;
using Hometel.Domain.Models;
using System.Threading.Tasks;
using Hometel.Domain.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hometel.Domain.Repositories;
using System.Text;
using Microsoft.Extensions.Options;
using Hometel.Domain.Models.Dto;

namespace Hometel.Services {
    public class UserService : IUserService {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityService _securityService;
        private readonly AppSettings _appSettings;
        public UserService(IUserRepository userRepository, ISecurityService securityService, IOptions<AppSettings> appSettings) {
            _userRepository = userRepository;
            _securityService = securityService;
            _appSettings = appSettings.Value;
        }
        public async Task<User> Authenticate(string email, string password){

            var user = await _userRepository.FindUser(email);
            if(user == null){
                return null;
            }

            if(!_securityService.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)){
                return null;
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            return user;
        }
        public async Task<User> RegisterUserAsync(User user, string password){
            if(string.IsNullOrWhiteSpace(password)){
                throw new AppException("Password is required");
            }
            var existingUser = await _userRepository.FindUser(user.Username);
            if(existingUser != null){
                throw new AppException("User already exists!");
            }

            byte[] passwordHash, passwordSalt;
            _securityService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _userRepository.CreateUser(user);
            return user;
        }

        public async Task<User> UpdateUserDataAsync(User user, string password){
            var existingUser = await _userRepository.FindUser(user.Username);
            if(existingUser == null){
                throw new AppException("We haven't been able to locate your user data");
            }
            byte[] passwordHash, passwordSalt;
            _securityService.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<User> GetUserAsync(string username){
            return await _userRepository.FindUser(username);
        }

        public async Task<IList<User>> GetAllUsersAsync(){
            return await _userRepository.ListAllUsersAsync();
        }
    }
}