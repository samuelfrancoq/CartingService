using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CartingService.DAL.Interfaces;
using CartingService.Domain.Entities;
using LiteDB;

namespace CartingService.DAL.Repositories
{
    public class LiteDbCartRepository : ICartRepository
    {
        private readonly string _dbPath = "CartingDatabase.db";
        private const string CollectionName = "carts";

        public Cart GetById(string id)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<Cart>(CollectionName);
            return collection.FindOne(x => x.Id == id) ?? new Cart { Id = id };
        }

        public void Update(Cart cart)
        {
            using var db = new LiteDatabase(_dbPath);
            var collection = db.GetCollection<Cart>(CollectionName);
            collection.Upsert(cart);
        }
    }
}
