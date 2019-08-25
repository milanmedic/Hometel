namespace Hometel.Domain.Models {
    public class Location {
        public int Id {get; set;}
        public float Latitude {get; set;}
        public float Longitude {get; set;}
        public Address Address {get; set;} //format: Street StreetNum, Town TownPostalNum
    }
}