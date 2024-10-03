using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookCrudOperationASP_Net.Data;
using Microsoft.AspNetCore.Mvc;
using BookCrudOperationASP_Net.Models;

namespace BookCrudOperationASP_Net.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var books = _booksRepository.GetAllBooks();

            return View(books);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            var res = _booksRepository.AddBook(book);

            if (res)
            {
                return Redirect("Index");
            }
            else
            {
                return Error();
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var book = _booksRepository.GetBookById(id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Book objBook)
        {
            var res = this._booksRepository.UpdateBook(objBook);
            if (res)
            {
                return Redirect("Index");
            }
            else
            {
                return Error();
            }
        }

        public IActionResult Delete(int id)
        {
            var res = this._booksRepository.DeleteBook(id);
            if (res)
            {
                return Redirect("Index");
            }
            else
            {
                return Error();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
