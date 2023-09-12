using System.ComponentModel.DataAnnotations;

namespace HNGStage2.Models
{
    public class PersonDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
