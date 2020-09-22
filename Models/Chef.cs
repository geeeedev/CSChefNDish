using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// one more for custom validations - future date
// one more for custom validations - 18 y/o and up


namespace CSChefNDish.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }         //dont forget PK from MySQL DB!! and mark it [Key] for the framework


        [Required(ErrorMessage = "...required!")]
        [Display(Name = "First Name")]
        public string First { get; set; }


        [Required(ErrorMessage = "...required!")]
        [Display(Name = "Last Name")]
        public string Last { get; set; }


        [DataType(DataType.Date)] 
        [Required(ErrorMessage = "...required!")]
        // [FutureDate(ErrorMessage="...must be a past date!")]
        // [Adult(ErrorMessage="...min must be 18 y/o!")]
        [Display(Name="Date of Birth")]
        public DateTime Birthdate { get; set; }


        //[DataType(DataType.Date)]     //not displaying
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        //[DataType(DataType.Date)]     //not displaying
        public DateTime UpdatedAt { get; set; } = DateTime.Now;


        public int Age(){
            int age = DateTime.Now.Year - this.Birthdate.Year;  
            if (DateTime.Now.DayOfYear < this.Birthdate.DayOfYear)  
                {age = age - 1;}  
            return age;
        }

        //One Chef can have Many Dishes;
        //Each Dish is created by One Chef
        public List<Dish> createdDishes { get; set;}

    }
}