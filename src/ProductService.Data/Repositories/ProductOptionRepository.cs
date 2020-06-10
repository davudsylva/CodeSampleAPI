using Dapper;
using ProductMicroservice.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProductMicroservice.Data.Repositories
{
    public class ProductOptionRepository: Repository, IProductOptionRepository
    {
        public ProductOptionRepository(IConfiguration config) : base(config)
        {
        }


        public async Task<IEnumerable<ProductOption>> GetProductOptions(Guid productId)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"select * from productoptions where productid = '{productId}' collate nocase";
                var dbResult = await connection.QueryAsync<ProductOption>(sql);
                return dbResult.AsList();
            }
        }

        public async Task<ProductOption> GetProductOptionById(Guid productId, Guid optionId)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"select * from productoptions where productid = '{productId}' collate nocase and id = '{optionId}' collate nocase";
                var dbResult = await connection.QueryAsync<ProductOption>(sql);
                return dbResult.FirstOrDefault();
            }
        }

        public async Task CreateOption(ProductOption productOption)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"insert into productoptions (id, productid, name, description) values ('{productOption.Id}', '{productOption.ProductId}', '{productOption.Name}', '{productOption.Description}')";
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task UpdateOption(ProductOption productOption)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"update productoptions set name = '{productOption.Name}', description = '{productOption.Description}' where id = '{productOption.Id}' collate nocase";
                await connection.ExecuteAsync(sql);
            }
        }

        public async Task DeleteOption(Guid optionId)
        {
            using (var connection = CreateConnection())
            {
                await connection.OpenAsync();
                var sql = $"delete from productoptions where id = '{optionId}' collate nocase";
                await connection.ExecuteAsync(sql);
            }
        }
    }
}
