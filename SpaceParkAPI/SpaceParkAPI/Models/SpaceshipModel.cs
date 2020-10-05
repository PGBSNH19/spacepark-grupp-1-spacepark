using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaceParkAPI.Models
{
    public class SpaceshipModel
    {
        [Key]
        public long ID { get; set; }

        public PersonModel Person { get; set; }

        [ForeignKey("ParkingSpaceID")]
        public long ParkingSpaceID { get; set; }
        public ParkingSpaceModel ParkingSpace { get; set; }
        
    }
}
