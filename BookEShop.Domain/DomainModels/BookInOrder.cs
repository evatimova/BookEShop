using BookEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEShop.Domain.DomainModels
{
    public class BookInOrder : BaseEntity
    {
        public Guid BookId { get; set; } //tikcetid
        public Book SelectedBook { get; set; } //SelectedTicket
        public Guid OrderId { get; set; }
        public Order UserOrder { get; set; }
        public int Quantity { get; set; }

    }
}
