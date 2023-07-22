using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public ShoppingCartVM()
        {
            OrderHeader = new OrderHeader();
        }

        public IEnumerable<ShoppingCart> ShoppingCartsList { get; set; }

        public OrderHeader OrderHeader { get; set; }
    }
}
