using System.Collections.Generic;
using Hometel.Domain.Models.Helpers;
using System;

namespace Hometel.Domain.Models{
    public class Apartment {
        public int Id {get; set;}
        public EApartmentType ApartmentType {get; set;}
        public int RoomNumber {get; set;}
        public int GuestNumber {get; set;}
        public Location Location {get; set;}
        public IList<Dates> Dates {get; set;} = new List<Dates>(); //DbContext se buni kada ga stavim kao DateTime
        public IList<AvailableDates> AvailableDates {get; set;} = new List<AvailableDates>(); //DbContext se buni kada ga stavim kao DateTime
        public Host Host {get; set;}
        public string HostId {get; set;}
        public IList<Comment> Comments {get; set;} = new List<Comment>();
        public IList<Image> Images {get; set;} = new List<Image>();
        public int Price {get; set;}
        public int ReservationStartTime {get; set;}
        public int ReservationEndTime {get; set;}
        public EApartmentStatus AppartmentStatus {get; set;}
        public IList<Amenity> Amenities {get; set;} = new List<Amenity>();
        public IList<Reservation> Reservations {get; set;} = new List<Reservation>();
    }
}