namespace Hometel.Domain.Models {
    public class Comment {
        public int Id {get; set;}
        public Guest Guest {get; set;}
        public Apartment Apartment {get; set;}
        public string Description {get; set;}
        public string Grade {get; set;}
    }
}