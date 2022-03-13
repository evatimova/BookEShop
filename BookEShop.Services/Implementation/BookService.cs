using BookEShop.Domain.DomainModels;
using BookEShop.Domain.DTO;
using BookEShop.Repository.Interface;
using BookEShop.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookEShop.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository; //_ticketRepository
        private readonly IUserRepository _userRepository;
        private readonly IRepository<BookInShoppingCart> _bookInShoppingCartRepository; //_ticketInShoppingCartRepository

        public BookService(IRepository<Book> bookRepository, IUserRepository userRepository, IRepository<BookInShoppingCart> bookInShoppingCartRepository)
        {
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _bookInShoppingCartRepository = bookInShoppingCartRepository;
        }


        public bool AddToShoppingCart(AddToShoppingCardDto item, string userID)
        {
            var user = this._userRepository.Get(userID);

            var userShoppingCard = user.UserCart;

            if (item.BookId != null && userShoppingCard != null)
            {
                var book = this.GetDetailsForBook(item.BookId); //var tiket

                if (book != null)
                {
                    BookInShoppingCart itemToAdd = new BookInShoppingCart
                    {
                        Book = book,
                        BookId = book.Id,
                        ShoppingCart = userShoppingCard,
                        ShoppingCartId = userShoppingCard.Id,
                        Quantity = item.Quantity
                    };

                    this._bookInShoppingCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewProduct(Book p)
        {
            this._bookRepository.Insert(p);
        }

        public void DeleteProduct(Guid id)
        {
            var book = this.GetDetailsForBook(id);
            this._bookRepository.Delete(book);
        }

        public List<Book> GetAllProducts()
        {
            return this._bookRepository.GetAll().ToList();
        }

        public List<Book> GetAllBooksByAuthor(string author) //GetAllTicketsByGenre
        {
            return GetAllProducts().Where(z => z.BookAuthor.Equals(author)).ToList();
        }

        public List<Book> GetAllBooksByGenre(string genre)
        {
            return GetAllProducts().Where(z => z.BookGenre.Equals(genre)).ToList();
        }

        public Book GetDetailsForBook(Guid? id)
        {
            return this._bookRepository.Get(id);
        }

        public AddToShoppingCardDto GetShoppingCartInfo(Guid? id)
        {
            var book = this.GetDetailsForBook(id);
            AddToShoppingCardDto model = new AddToShoppingCardDto
            {
                SelectedBook = book,
                BookId = book.Id,
                Quantity = 1
            };
            return model;
        }

        public void UpdeteExistingProduct(Book p)
        {
            this._bookRepository.Update(p);
        }
    }
}
