namespace Hometel.Domain.Models {
    public class User {
        public string Username {get; set;} //unique
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Token { get; set; }
        public string Name {get; set;}
        public string Surname {get; set;}
        public EGender Gender {get; set;}
        public string Role {get; set;}
    }
}