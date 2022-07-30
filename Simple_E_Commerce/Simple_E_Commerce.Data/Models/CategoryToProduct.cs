using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple_E_Commerce.Data.Models
{
    public class CategoryToProduct
    {
        public int CtegoryId { get; set; }
        public int ProductId { get; set; }

        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
