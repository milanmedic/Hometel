using System.Collections.Generic;

namespace Hometel.Domain.Models{
    public class Host : User {
        public IList<Apartment> ListOfApartments {get; set;} = new List<Apartment>();
    }
}