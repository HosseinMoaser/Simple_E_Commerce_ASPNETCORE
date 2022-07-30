namespace Simple_E_Commerce.Data.Models
{
    public class Item
    {
        public int Id { get; set; }      
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }


        public Product Product { get; set; }
    }
}
