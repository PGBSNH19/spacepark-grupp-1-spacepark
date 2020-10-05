using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceParkAPI.Models
{
    public class ParkingSpaceModel
    {
        [Key]
        public long ID { get; set; }
        public SpaceshipModel Spaceship { get; set; }

        [ForeignKey("ParkingLotID")]
        public long ParkingLotID { get; set; }
        public ParkingLotModel ParkingLot { get; set; }
    }
}
