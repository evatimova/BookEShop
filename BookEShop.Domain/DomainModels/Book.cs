using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookEShop.Domain.DomainModels
{
    public class Book : BaseEntity
    {
        [Required]
        public string BookName { get; set; } 
        [Required]
        public string BookImage { get; set; } 
        [Required]
        public string BookDescription { get; set; } 
        [Required]
        public int BookPrice { get; set; } 
        [Required]
        public string BookGenre { get; set; }
        [Required]
        public string BookAuthor { get; set; }
        public float Rating { get; set; }
        public virtual ICollection<BookInShoppingCart> BookInShoppingCarts { get; set; } 
        public virtual ICollection<BookInOrder> BooksInOrder { get; set; } 

    }
}
