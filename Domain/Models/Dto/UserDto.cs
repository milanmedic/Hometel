
using System.ComponentModel.DataAnnotations;
namespace Hometel.Domain.Models.Dto
{
    public class UserDto
    {
        [Required]
        [StringLength(255)]
        public string Username {get; set;}
        [Required]
        [StringLength(255)]
        public string Name {get; set;}
        [Required]
        [StringLength(255)]
        public string Surname {get; set;}
        [Required]
        public EGender Gender {get; set;}
        public string Role {get; set;}
        [Required]
        public string Password { get; set; }
    }
}