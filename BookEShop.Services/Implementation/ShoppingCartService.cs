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
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<BookInOrder> _bookInOrderRepository; // _ticketInOrderRepository
        private readonly IRepository<Order> _orderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<BookInOrder> bookInOrderRepository, IRepository<Order> orderRepository, IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _bookInOrderRepository = bookInOrderRepository;
        }

        public bool deleteFromShoppingCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {

                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                var itemToDelete = userShoppingCart.BookInShoppingCarts.Where(z => z.BookId.Equals(id)).FirstOrDefault();

                userShoppingCart.BookInShoppingCarts.Remove(itemToDelete);

                this._shoppingCartRepository.Update(userShoppingCart);

                return true;
            }
            return false;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userBookCart = loggedInUser.UserCart; //userTicketCart

            var AllBooks = userBookCart.BookInShoppingCarts.ToList(); //AllTickets, userTicketCart

            var allBookPrice = AllBooks.Select(z => new //allTticketPrice, AllBooks
            {
                BookPrice = z.Book.BookPrice, //TicketPrice
                Quantity = z.Quantity
            }).ToList();


            var totalPrice = 0;

            foreach (var item in allBookPrice)
            {
                totalPrice += item.BookPrice * item.Quantity;
            }

            ShoppingCartDto shoppingCartDto = new ShoppingCartDto
            {
                BookInShoppingCarts = AllBooks,
                TotalPrice = totalPrice
            };
            return shoppingCartDto; 
        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId)) 
            {
                var loggedInUser = this._userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order orderItem = new Order
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    User = loggedInUser,
                };

                this._orderRepository.Insert(orderItem);

            List<BookInOrder> booksInOrder = new List<BookInOrder>(); //ticketsInOrder

           var result = userShoppingCart.BookInShoppingCarts
                .Select(z => new BookInOrder
                {
                    OrderId = orderItem.Id,
                    BookId = z.Book.Id,
                    SelectedBook = z.Book,
                    UserOrder = orderItem,
                    Quantity = z.Quantity
                }).ToList();


               booksInOrder.AddRange(result);

            foreach (var item in booksInOrder)
            {
                    this._bookInOrderRepository.Insert(item);
            }

            loggedInUser.UserCart.BookInShoppingCarts.Clear();

                this._userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
