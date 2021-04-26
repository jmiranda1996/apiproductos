using ApiProductos.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiProductos.Data
{
    public interface IProductsRepository : IDataRepository<Products>
    {
        Task<IEnumerable<Products>> GetAllByPrice(double lower, double higher);
    }
}
