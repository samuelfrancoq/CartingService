using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartingService.Domain.Entities;

namespace CartingService.DAL.Interfaces
{
    public interface ICartRepository
    {
        public Cart GetById(string id);
        public void Update(Cart cart);
        public void UpdateItemInAllCarts(int productId, string newName, decimal newPrice);
    }
}
