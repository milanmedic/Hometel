using System.ComponentModel;
namespace Hometel.Domain.Models {
    public enum EApartmentStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("Inactive")]
        Inactive = 2,
    }
}