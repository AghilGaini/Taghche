using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericDomain<T>
    {
        Task<T> GetByIdAsync(long id);
        Task<bool> AddAsync(T entity);
    }
}
