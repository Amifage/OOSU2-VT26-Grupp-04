using System.ComponentModel.DataAnnotations;

namespace Entites
{
   
        public class Cookie
        {
            public int CookieID { get; set; }
            [Required]
            public string Name { get; set; } = "";
            public string? Description { get; set; }
            public decimal Price { get; set; }
            public ICollection<Ingredient>? Ingredients { get; set; }
            public ICollection<Employee>? Employees { get; set; }

        }
    
}
