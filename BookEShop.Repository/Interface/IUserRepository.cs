using BookEShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookEShop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<BookEShopApplicationUser> GetAll();
        BookEShopApplicationUser Get(string id);
        void Insert(BookEShopApplicationUser entity);
        void Update(BookEShopApplicationUser entity);
        void Delete(BookEShopApplicationUser entity);
    }
}
