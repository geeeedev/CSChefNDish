using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSChefNDish.Models
{
    [NotMapped]
    public class ChefNDish
    {
        public Dish NewDish {get; set;}
        public List<Chef> ListOfChefs { get; set; }
    }
}