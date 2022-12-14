using System.ComponentModel.DataAnnotations;

namespace Gama_API.Data.Dtos
{
    public class CreateProductDto
    {
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
        [MaxLength(30)]
        public string? Category { get; set; }
    }
}
