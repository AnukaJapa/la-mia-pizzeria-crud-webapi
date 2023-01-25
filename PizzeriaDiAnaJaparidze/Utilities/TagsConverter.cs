using Microsoft.AspNetCore.Mvc.Rendering;
using PizzeriaDiAnaJaparidze.Database;
using PizzeriaDiAnaJaparidze.Models;

namespace PizzeriaDiAnaJaparidze.Utilities
{
    public static class TagsConverter
    {
        public static List<SelectListItem> getListTagsForMultipleSelect()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Tag> tagsFromDb = db.Tags.ToList<Tag>();
List<SelectListItem> listaPerLaSelectMultipla = new List<SelectListItem>();

                foreach(Tag tag in tagsFromDb)
                {
                    SelectListItem opzioneSingolaSelectMultipla = new SelectListItem()
                    {
                        Text = tag.Title,
                        Value = tag.Id.ToString()
                    };
                    listaPerLaSelectMultipla.Add(opzioneSingolaSelectMultipla);

                }

                return listaPerLaSelectMultipla;
            }
        }
    }
}
