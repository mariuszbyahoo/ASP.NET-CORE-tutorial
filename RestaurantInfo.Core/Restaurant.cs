using System;
using System.ComponentModel.DataAnnotations;

namespace RestaurantInfo.Core
{
    public class Restaurant 
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        [Required]
        [StringLength(80)]
        public string Location { get; set; }
        public CuisineType Cuisine { get; set; }
    }
}
