using BookEShop.Domain.DomainModels;
using BookEShop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookEShop.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Order>();
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.BooksInOrder)
                .Include(z => z.User)
                .Include("BooksInOrder.SelectedBook")
                .ToListAsync().Result;
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                .Include(z => z.BooksInOrder)
                .Include(z => z.User)
                .Include("BooksInOrder.SelectedBook")
                .SingleOrDefaultAsync(z => z.Id == model.Id).Result;
        }
    }
}
