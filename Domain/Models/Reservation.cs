using System;

namespace Hometel.Domain.Models{
    public class Reservation { //Gost može da rezerviše niz slobodnih/dostupnih datuma samo ako oni nemaju prekida
        public int Id {get; set;}
        public Apartment ReservedApartment {get; set;}
        public DateTime ReservationStart {get; set;}
        public int NumberOfDays {get; set;}
        public int TotalPrice {get; set;}
        public Guest Guest {get; set;}
        public EReservationStatus ReservationStatus {get; set;}
    }
}