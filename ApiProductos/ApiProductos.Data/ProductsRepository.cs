using ApiProductos.Tables;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiProductos.Data
{
    public class ProductsRepository : BaseRepository, IProductsRepository
    {
        public async Task<IEnumerable<Products>> GetAllByPrice(double lower, double higher)
        {
            string query = "select *from products where price between @lower and @higher";
            return await DbQueryAsync<Products>(query.ToString(), new { lower, higher });
        }

        public async Task<Products> GetById(int id)
        {
            string query = "select *from products where id = @id";
            return await DbQuerySingleAsync<Products>(query.ToString(), new { id });
        }

        public async Task Insert(Products newent)
        {
            string query = "insert into products (id, name, type, price) values (@id, @name, @type, @price)";
            await DbExecuteAsync(query.ToString(), newent);
        }

        public async Task<(int, string)> Update(int id, Products newent)
        {
            string query = "update products set name = @name, type = @type, price = @price where id = " + id;
            var result = await DbExecuteAsync(query.ToString(), newent);
            if (result == 0) return (404, "El producto no se encuentra");
            return (200, "Actualizacion correcta");
        }

        public async Task<(int, string)> DeleteById(int id)
        {
            string query = "delete products where id = @id";
            var result = await DbExecuteAsync(query.ToString(), new { id });
            if (result == 0) return (404, "El producto no se encuentra");
            return (200, "Eliminacion correcta");
        }

    }
}
