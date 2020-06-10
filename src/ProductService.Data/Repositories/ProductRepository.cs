using Dapper;
using Refactored.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Refactored.Data.Repositories
{
    public class ProductRepository : Repository, IProductRepository
    {
        public ProductRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            using (var connection = CreateConnection())
            {
                var baseDir = System.IO.Directory.GetCurrentDirectory();
                await connection.OpenAsync();
                var sql = $"select * from Products";
                var dbResult = await connection.QueryAsync<Product>(sql);
                return dbResult.AsList();
            }
        }

        public async Task<IEnumerable<Product>> GetByName(string name)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"select * from Products where lower(name) like '%{name.ToLower()}%'";
                var dbResult = await connection.QueryAsync<Product>(sql);
                return dbResult.AsList();
            }
        }

        public async Task<Product> GetById(Guid productId)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    await connection.OpenAsync();
                    var sql = $"select * from Products where id = '{productId}' collate nocase";
                    var dbResult = await connection.QueryAsync<Product>(sql);
                    return dbResult.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task Update(Product product)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"update Products set name = '{product.Name}', description = '{product.Description}', price = {product.Price}, deliveryprice = {product.DeliveryPrice} where id = '{product.Id}' collate nocase";
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task Create(Product product)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"insert into Products (id, name, description, price, deliveryprice) values ('{product.Id}', '{product.Name}', '{product.Description}', {product.Price}, {product.DeliveryPrice})";
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task DeleteById(Guid productId)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"delete from Products where id = '{productId}' collate nocase";
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
