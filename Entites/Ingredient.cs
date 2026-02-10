using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<Cookie> Cookies { get; set; }
    }
}
