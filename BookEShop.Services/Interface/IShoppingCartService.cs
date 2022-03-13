using BookEShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookEShop.Services.Interface
{
   public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteFromShoppingCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}
