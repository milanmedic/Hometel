
using System.ComponentModel.DataAnnotations;
namespace Hometel.Domain.Models.Dto
{
    public class LoginDto
    {
        [Required]
        [StringLength(255)]
        public string Username {get; set;}
        [Required]
        public string Password { get; set; }
    }
}