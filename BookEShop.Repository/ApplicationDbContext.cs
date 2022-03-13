using BookEShop.Domain.DomainModels;
using BookEShop.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookEShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<BookEShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } //Tickets
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<BookInOrder> BookInOrders { get; set; } //TicketInOrders
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<BookInShoppingCart> BookInShoppingCarts { get; set; } //TicketInShoppingCarts

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>().Property(z => z.Id).ValueGeneratedOnAdd();

            builder.Entity<ShoppingCart>().Property(z => z.Id).ValueGeneratedOnAdd();

            //builder.Entity<TicketInShoppingCart>().HasKey(z => new { z.TicketId, z.ShoppingCartId });

            builder.Entity<BookInShoppingCart>().HasOne(z => z.Book)
                .WithMany(z => z.BookInShoppingCarts).HasForeignKey(z => z.ShoppingCartId);

            builder.Entity<BookInShoppingCart>().HasOne(z => z.ShoppingCart)
               .WithMany(z => z.BookInShoppingCarts).HasForeignKey(z => z.BookId);

            builder.Entity<ShoppingCart>().HasOne<BookEShopApplicationUser>(z => z.Owner)
                .WithOne(z => z.UserCart)
                .HasForeignKey<ShoppingCart>(z => z.OwnerId);

            //builder.Entity<TicketInOrder>()
            //   .HasKey(z => new { z.TicketId, z.OrderId });

            builder.Entity<BookInOrder>()
                .HasOne(z => z.SelectedBook)
                .WithMany(z => z.BooksInOrder)
                .HasForeignKey(z => z.OrderId);

            builder.Entity<BookInOrder>()
                .HasOne(z => z.UserOrder)
                .WithMany(z => z.BooksInOrder)
                .HasForeignKey(z => z.BookId);
        }
    }
}
