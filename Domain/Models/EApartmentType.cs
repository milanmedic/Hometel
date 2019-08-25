using System.ComponentModel;
namespace Hometel.Domain.Models {
    public enum EApartmentType
    {
        [Description("Full suite")]
        FullSuite = 1,
        [Description("Room")]
        Room = 2,
    }
}