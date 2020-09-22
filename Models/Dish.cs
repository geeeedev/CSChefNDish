using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CSChefNDish.Models
{
    public class Dish
    {
        [Key]
        public int DishId { get; set; }                                                 //dont forget PK from MySQL DB!! and mark it [Key] for the framework


        [Required(ErrorMessage = "...missing!")]
        [Display(Name = "Name of Dish")]
        public string Name { get; set; }


        // [Required(ErrorMessage = "...missing!")]
        // [Display(Name = "Chef's Name")]                                                  //no longer needed here
        // public string Chef { get; set; }


        public int Tastiness { get; set; }


        [Required(ErrorMessage = "...missing!"), Range(1, 9000, ErrorMessage = "... no way!?!")]
        [Display(Name = "# of Calories")]
        public int Calories { get; set; }


        [Required(ErrorMessage = "...What's this dish about?"), MinLength(10, ErrorMessage = "...Tell me more? (10 char min)")]
        public string Description { get; set; }


        //[DataType(DataType.Date)]                                                          //not displaying
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //[DataType(DataType.Date)]                                                         //not displaying
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        //One Chef can have Many Dishes;
        //Each Dish is created by One Chef
        [Display(Name = "Select a Chef")]
        public int ChefId { get; set; }
        public Chef dishChef { get; set; }


    }
}