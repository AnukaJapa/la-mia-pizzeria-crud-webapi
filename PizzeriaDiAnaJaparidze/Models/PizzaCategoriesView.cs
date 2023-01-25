using Microsoft.AspNetCore.Mvc.Rendering;

namespace PizzeriaDiAnaJaparidze.Models
{
    public class PizzaCategoriesView
    {
        public Pizza Pizza { get; set; }

        public List<Category>? Categories { get; set; }

        public List<SelectListItem>?Tags { get; set; }

        public List<String>? TagsSelectedFromMultipleSelect { get; set; }  
        
    }
}
