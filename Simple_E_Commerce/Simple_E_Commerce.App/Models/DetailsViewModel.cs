using Simple_E_Commerce.Data.Models;

namespace Simple_E_Commerce.App.Models
{
    public class DetailsViewModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }
    }
}
