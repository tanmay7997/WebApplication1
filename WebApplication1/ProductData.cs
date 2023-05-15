using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ProductData
    {
        private readonly string _connectionString;

        public ProductData(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Product> GetProducts()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection.Query<Product>("SELECT * FROM Products");
        }

        public void AddProduct(Product product)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", product);
            }
        }

        public void UpdateProduct(Product product)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute("UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id", product);
            }
        }

        public void DeleteProduct(int id)
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                connection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
