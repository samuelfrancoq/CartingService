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
        Cart GetById(string id);
        void Update(Cart cart);
        // Agrega Delete si lo ves necesario para el RemoveItem
    }
}
