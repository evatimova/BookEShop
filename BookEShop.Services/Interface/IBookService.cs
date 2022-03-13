using BookEShop.Domain.DomainModels;
using BookEShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookEShop.Services.Interface
{
    public interface IBookService
    {
        List<Book> GetAllProducts();
        List<Book> GetAllBooksByAuthor(string author);
        List<Book> GetAllBooksByGenre(string genre);

        //List<Ticket> FilterTicketsByDate();
        Book GetDetailsForBook(Guid? id);
        void CreateNewProduct(Book b);
        void UpdeteExistingProduct(Book b);
        AddToShoppingCardDto GetShoppingCartInfo(Guid? id);
        void DeleteProduct(Guid id);
        bool AddToShoppingCart(AddToShoppingCardDto item, string userID);
    }
}
