using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookCrudOperationASP_Net.Data
{
    public class BookRepository : IBooksRepository
    {
        private readonly BookDbContext _entities;

        public BookRepository(BookDbContext bookDbContext)
        {
            this._entities = bookDbContext;
        }

        public List<Book> GetAllBooks()
        {
            return this._entities.Books.ToList();
        }

        public Book GetBookById(int id)
        {
            return this._entities.Books
            .Where(x => x.Id == id)
            .FirstOrDefault();
        }

        public bool AddBook(Book book)
        {
            var res = false;
            try
            {
                if (!this._entities.Books.Any(x => x.isbn == book.isbn))
                {
                    var aux = this._entities.Books.Add(book);
                    if (this._entities.SaveChanges() > 0)
                    {
                        res = true;
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return res;
        }

        public bool UpdateBook(Book book)
        {
            var res = false;
            try
            {
                var aux = this._entities.Books.Update(book);
                if (this._entities.SaveChanges() > 0)
                {
                    res = true;
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return res;
        }

        public bool DeleteBook(int id)
        {
            var res = false;
            try
            {
                var model = this.GetBookById(id);
                if (model != null)
                {
                    var aux = this._entities.Books.Remove(model);
                    if (this._entities.SaveChanges() > 0)
                    {
                        res = true;
                    }
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return res;
        }
    }
}