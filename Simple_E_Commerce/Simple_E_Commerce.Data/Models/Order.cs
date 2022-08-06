using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_E_Commerce.Data.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }
        
        public bool IsFinally { get; set; }


        // Navigation Property
        [ForeignKey("UserId")]
        public User User { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
