using System.ComponentModel;
namespace Hometel.Domain.Models {
    public enum EReservationStatus
    {
        [Description("Created")]
        Created = 1,
        [Description("Rejected")]
        Rejected = 2,
        [Description("Bailed")]
        Bailed = 3,
        [Description("Accepted")]
        Accepted = 4,
        [Description("Finished")]
        Finished = 5,
    }
}