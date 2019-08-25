using System.Collections.Generic;

namespace Hometel.Domain.Models {
    public class Guest : User {
        public IList<Apartment> RentedApartments {get; set;} = new List<Apartment>();
        public IList<Reservation> ListOfReservations {get; set;} = new List<Reservation>();
    }
}