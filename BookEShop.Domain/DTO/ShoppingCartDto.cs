using BookEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEShop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<BookInShoppingCart> BookInShoppingCarts { get; set; } //TicketInShoppingCarts
        public int TotalPrice { get; set; }
    }
}
