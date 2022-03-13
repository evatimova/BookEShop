using BookEShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEShop.Domain.DTO
{
    public class AddToShoppingCardDto
    {
        public Book SelectedBook { get; set; } //SelectedTicket
        public Guid BookId { get; set; }//TicketId
        public int Quantity { get; set; }

    }
}
