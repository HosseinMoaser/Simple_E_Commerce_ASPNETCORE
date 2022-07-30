namespace Simple_E_Commerce.Data.Models
{
    public class Cart
    {
        public Cart()
        {
            CartItems = new List<CartItem>();
        }
        public int OrderID { get; set; }
        public List<CartItem> CartItems { get; set; }

        public void AddItem(CartItem cartItem)
        {
            if(CartItems.Exists(i=> i.Item.Id == cartItem.Id))
            {
                CartItems.Find(i => i.Item.Id == cartItem.Item.Id).Quantity += 1;
            }
            else
            {
                CartItems.Add(cartItem);
            }
        }

        public void RemoveItem(int itemId)
        {
            var item = CartItems.SingleOrDefault(i=> i.Item.Id == itemId);
            if(item?.Quantity <= 1)
            {
                CartItems.Remove(item);
            }
            else if(item != null)
            {
                item.Quantity -= 1;
            }

        }
    }
}
