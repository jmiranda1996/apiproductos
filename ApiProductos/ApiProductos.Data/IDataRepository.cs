using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiProductos.Data
{
    public interface IDataRepository<T> where T : class
    {
        Task<T> GetById(int id);
        Task Insert(T newent);
        Task<(int, string)> Update(int id, T newent);
        Task<(int, string)> DeleteById(int id);
    }
}
