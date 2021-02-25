using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class HotelRoomDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a room name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Occupancy")]
        public int Occupancy { get; set; }

        [Range(1, 3000, ErrorMessage = "Regular rate must be bewteen 1 and 3000")]
        public double RegularRate { get; set; }

        public string Details { get; set; }
        public string SqFt { get; set; }
        public double TotalDays { get; set; }
        public double TotalAmmount { get; set; }

        public IList<HotelRoomImageDto> HotelRoomImages { get; set; }
        public List<string> ImagesUrls { get; set; }
    }
}