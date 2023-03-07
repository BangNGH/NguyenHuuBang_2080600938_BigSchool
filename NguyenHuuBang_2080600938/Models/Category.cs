using System.ComponentModel.DataAnnotations;

namespace NguyenHuuBang_2080600938.Models
{
    public class Category
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}