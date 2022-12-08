using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Level2CacheDAL.Repository
{
    public class BookRepository : IBookDomain
    {
        private IMemoryCache _memCache;

        public BookRepository(IMemoryCache memCache)
        {
            _memCache = memCache;
        }


        public async Task<bool> AddAsync(BookDomain entity)
        {
            try
            {
                _memCache.Set(entity.Id, entity);
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<BookDomain> GetByIdAsync(long id)
        {
            var book = (BookDomain)_memCache.Get(id);

            return await Task.FromResult(book);
        }
    }
}
