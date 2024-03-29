﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using GemBox.Document;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BookEShop.Domain.DomainModels;

namespace BookEShop.Web.Controllers
{
    //[Authorize]
    public class OrderController : Controller
    {
        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44322/api/Admin/GetAllActiveOrders";

            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Order>>().Result;

            return View(data);
        }

        public IActionResult Details(Guid orderId)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44322/api/Admin/GetDetailsForOrder";

            var model = new
            {
                Id = orderId
            };
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;
            return View(data);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public FileContentResult ExportAllOrders()
        {
            string fileName = "Orders.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Orders");

                worksheet.Cell(1, 1).Value = "Order Id";
                worksheet.Cell(1, 2).Value = "Customer Email";

                HttpClient client = new HttpClient();

                string URL = "https://localhost:44322/api/Admin/GetAllActiveOrders";

                HttpResponseMessage response = client.GetAsync(URL).Result;

                var data = response.Content.ReadAsAsync<List<Order>>().Result;

                for (int i = 1; i <= data.Count; i++)
                {
                    var item = data[i - 1];

                    worksheet.Cell(i + 1, 1).Value = item.Id.ToString();
                    worksheet.Cell(i + 1, 2).Value = item.User.Email;

                    for (int t = 0; t < item.BooksInOrder.Count; t++)
                    {
                        worksheet.Cell(1, t + 3).Value = "Book-" + (t + 1);
                        worksheet.Cell(i + 1, t + 3).Value = item.BooksInOrder.ElementAt(t).SelectedBook.BookName;
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

        public FileContentResult CreateInvoice(Guid id)
        {
            HttpClient client = new HttpClient();

            string URL = "https://localhost:44322/api/Admin/GetDetailsForOrder";

            var model = new
            {
                Id = id
            };

            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(URL, content).Result;

            var data = response.Content.ReadAsAsync<Order>().Result;

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", data.Id.ToString());
            document.Content.Replace("{{UserName}}", data.User.UserName);

            StringBuilder sb = new StringBuilder();

            var totalPrice = 0.0;

            foreach (var item in data.BooksInOrder)
            {
                totalPrice += item.Quantity * item.SelectedBook.BookPrice;
                sb.AppendLine(item.SelectedBook.BookName + " by author: " + item.SelectedBook.BookAuthor + ", with quantity of: " + item.Quantity + " and price of: " + item.SelectedBook.BookPrice + "MKD den");
            }
            document.Content.Replace("{{BookList}}", sb.ToString()); 
            document.Content.Replace("{{TotalPrice}}", totalPrice.ToString());

            var stream = new MemoryStream();

            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }
    }
}
