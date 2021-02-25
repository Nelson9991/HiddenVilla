using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AmenitieDto
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Description { get; set; }
        [Required] public string Timming { get; set; }
        [Required] public string Icon { get; set; }
    }
}