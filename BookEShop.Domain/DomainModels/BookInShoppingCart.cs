using BookEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEShop.Domain.DomainModels
{
    public class BookInShoppingCart : BaseEntity
    { 
        public Guid BookId { get; set; } //TicketId
        public Book Book { get; set; } //ticket
        public Guid ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
    }
}
