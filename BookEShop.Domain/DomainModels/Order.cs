using BookEShop.Domain.DomainModels;
using BookEShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEShop.Domain.DomainModels
{
    public class Order : BaseEntity
    {
        public BookEShopApplicationUser User { get; set; }
        public string UserId { get; set; }
        public virtual ICollection<BookInOrder> BooksInOrder { get; set; } //TicketsInOrder
    } 
}
