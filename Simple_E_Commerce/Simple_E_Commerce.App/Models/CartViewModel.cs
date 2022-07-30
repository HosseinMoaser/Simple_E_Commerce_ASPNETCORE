using Simple_E_Commerce.Data.Models;

namespace Simple_E_Commerce.App.Models
{
    public class CartViewModel
    {
        public CartViewModel()
        {
            CartItems = new List<CartItem>();
        }
        public List<CartItem> CartItems { get; set; }
        public decimal TotalCost { get; set; }

    }
}
