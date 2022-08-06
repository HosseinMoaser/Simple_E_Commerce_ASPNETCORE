namespace Simple_E_Commerce.Data.Models
{
    public class Product
    {
        
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int ItemId { get; set; }

        public Item Item { get; set; }
        public ICollection<CategoryToProduct> CategoryToProducts { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }
}
