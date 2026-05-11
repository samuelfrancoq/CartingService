using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartingService.Domain.Entities
{
    public class Cart
    {
        public string Id { get; set; } = string.Empty;
        public List<CartItem> Items { get; set; } = new();
    }
}
