using System;
using System.ComponentModel.DataAnnotations;

namespace CND.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }

        [Required]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }

        [Required]
        [Range(1, 5000)]
        [Display(Name = "# of Calories")]
        public int Calories { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Tastiness { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        [Required]
        public int ChefId { get; set; }

        // Navigation Properties
        public Chef Creator { get; set; }
    }
}