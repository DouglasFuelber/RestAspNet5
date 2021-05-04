﻿using RestAspNet5.Model;
using RestAspNet5.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestAspNet5.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private MySQLContext _context;

        public BookRepositoryImplementation(MySQLContext context) {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Book.ToList();
        }

        public Book FindById(long id)
        {
            return _context.Book.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Book.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return book;
        }

        public Book Update(Book book)
        {
            if (!Exists(book.Id)) return null;

            Book bookDb = _context.Book.SingleOrDefault(p => p.Id.Equals(book.Id));

            if (bookDb != null) {
                try
                {
                    _context.Entry(bookDb).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return book;
        }

        public void Delete(long id)
        {
            Book bookDb = _context.Book.SingleOrDefault(p => p.Id.Equals(id));

            if (bookDb != null)
            {
                try
                {
                    _context.Book.Remove(bookDb);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return _context.Book.Any(p => p.Id.Equals(id));
        }
    }
}
