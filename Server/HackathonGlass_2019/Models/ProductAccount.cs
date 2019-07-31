using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HackathonGlass_2019.Models
{
    public class ProductAccount
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal SalePrice { get; set; }

        public List<int> Categories { get; set; }
    }
}