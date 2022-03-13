using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BookEShop.Services.Interface;
using BookEShop.Domain.DomainModels;
using BookEShop.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using ClosedXML.Excel;
using System.IO;

namespace BookEShop.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService; 

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: Books
        public IActionResult Index()
        {
            var allBooks = this._bookService.GetAllProducts(); //allTickets
            return View(allBooks);
        }

        public IActionResult ExportBooksByGenre(string genre) //ExportTicketsByGenre
        {
            List<Book> filteredBooks = this._bookService.GetAllBooksByGenre(genre); //filteredTickets

            string fileName = "BooksByGenre.xlsx"; //"Tickets.xlsx"
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            if (filteredBooks.Count == 0)
            {
                return RedirectToAction("Index", "Books", new { error = "No books with this genre." });
            }

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Books");

                worksheet.Cell(1, 1).Value = "Book Id";
                worksheet.Cell(1, 2).Value = "Book Genre";

                for (int i = 1, t = 0; i <= filteredBooks.Count; i++, t++)
                {
                    var item = filteredBooks[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.BookGenre.ToString();
                    //pecatenje na biletite vednas pod Ticket-brojkata
                    //worksheet.Cell(1, t + 3).Value = "Ticket-" + (t + 1);
                    //worksheet.Cell(2, t + 3).Value = item.MovieName;

                    for (int j = 0; j < 1; j++)
                    {
                        worksheet.Cell(1, j + 3).Value = "Book Name";
                        worksheet.Cell(i + 1, j + 3).Value = item.BookName;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult ExportBooksByAuthor(string author) //ExportTicketsByGenre
        {
            List<Book> filteredBooks = this._bookService.GetAllBooksByAuthor(author); //filteredTickets

            string fileName = "BooksByAuthor.xlsx"; //"Tickets.xlsx"
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            if (filteredBooks.Count == 0)
            {
                return RedirectToAction("Index", "Books", new { error = "No books by this author." });
            }

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Books");

                worksheet.Cell(1, 1).Value = "Book Id";
                worksheet.Cell(1, 2).Value = "Book Author";

                for (int i = 1, t = 0; i <= filteredBooks.Count; i++, t++)
                {
                    var item = filteredBooks[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.BookAuthor.ToString();
                    //pecatenje na biletite vednas pod Ticket-brojkata
                    //worksheet.Cell(1, t + 3).Value = "Ticket-" + (t + 1);
                    //worksheet.Cell(2, t + 3).Value = item.MovieName;

                    for (int j = 0; j < 1; j++)
                    {
                        worksheet.Cell(1, j + 3).Value = "Book Name";
                        worksheet.Cell(i + 1, j + 3).Value = item.BookName;
                    }
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }

            //var workbook = WriteToCSV(filteredTickets);

            //var stream = new MemoryStream();
            //workbook.SaveAs(stream);
            //var content = stream.ToArray();

            //return File(content, contentType, fileName);
        }

        public IActionResult AddBookToCard(Guid? id) //AddTicketToCard
        {
            var model = this._bookService.GetShoppingCartInfo(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddBookToCard([Bind("BookId", "Quantity")] AddToShoppingCardDto item) //AddTicketToCard
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = this._bookService.AddToShoppingCart(item, userId);

            if (result)
            {
                return RedirectToAction("Index", "Books");
            }

            return View(item);
        }

        // GET: Tickets/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this._bookService.GetDetailsForBook(id); //var ticket
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,BookName,BookImage,BookDescription,BookGenre,BookPrice,BookAuthor,Rating")] Book book)
        {
            if (ModelState.IsValid)
            {
                this._bookService.CreateNewProduct(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this._bookService.GetDetailsForBook(id); //ticket
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,BookName,BookImage,BookDescription,BookGenre,BookPrice,BookAuthor,Rating")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this._bookService.UpdeteExistingProduct(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = this._bookService.GetDetailsForBook(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            this._bookService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(Guid id)
        {
            return this._bookService.GetDetailsForBook(id) != null;
        }
    }
}
