using BookEShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEShop.Domain.DomainModels
{
    public class ShoppingCart : BaseEntity
    {
        public string OwnerId { get; set; }
        public BookEShopApplicationUser Owner { get; set; }

        public virtual ICollection<BookInShoppingCart> BookInShoppingCarts{ get; set; } //TicketInShoppingCarts

    }
}
