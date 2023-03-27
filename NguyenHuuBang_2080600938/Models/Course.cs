using System;
using System.ComponentModel.DataAnnotations;

namespace NguyenHuuBang_2080600938.Models
{
    public class Course
    {
        public int Id { get; set; }
        public ApplicationUser Leturer { get; set; }
        [Required]
        [StringLength(255)]
        public string LeturerId { get; set; }
        [Required]
        [StringLength(255)]
        public string Place { get; set; }
        public DateTime DateTime { get; set; }
        public Category category { get; set; }
        [Required]
        public byte CategoryId { get; set; }

        public bool IsCanceled { get; set; }
    }
}