using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Level2CacheDAL.Repository
{
    public class BookRepository : IBookDomain
    {
        private static List<BookDomain> _books = new List<BookDomain>()
        {
            new BookDomain() { Id = 2 ,Description="test2" }
        };

        public async Task<bool> AddAsync(BookDomain entity)
        {
            try
            {
                _books.Add(entity);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<BookDomain> GetByIdAsync(long id)
        {
            var book = _books.FirstOrDefault(r => r.Id == id);
            return await Task.FromResult(book);
        }
    }
}
