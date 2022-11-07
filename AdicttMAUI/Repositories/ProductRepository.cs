using AdicttMAUI.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Repositories
{
    public class ProductRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection sqlConnectionAsync;

        private SQLiteConnection sqlConnection;
        private async Task InitAsync()
        {
            if (sqlConnectionAsync != null)
                return;

            sqlConnectionAsync = new SQLiteAsyncConnection(_dbPath);
            await sqlConnectionAsync.CreateTableAsync<Product>();
        }

        private void Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteConnection(_dbPath);
            sqlConnection.CreateTable<Product>();

        }
        public ProductRepository(string dbPath)
        {
            _dbPath = dbPath;
        }   

        public List<Product> GetAllProduct()
        {
            try
            {
                Init();
                return sqlConnection.GetAllWithChildren<Product>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get Products from local DataBase. Erro: {0}", ex.Message);
            }

            return new List<Product>();
        }

        public async Task<List<Product>> GetAllProductAsync()
        {
            try
            {
                await InitAsync();
                return await sqlConnectionAsync.GetAllWithChildrenAsync<Product>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get Products from local DataBase. Erro: {0}", ex.Message);
            }

            return new List<Product>();
        }

        public async Task<Product> GetProductById(long id)
        {
            try
            {
                await InitAsync();
                return await sqlConnectionAsync.GetAsync<Product>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get Product {0}. Error: {1}", id, ex.Message);
            }

            return new Product();
        }

        public async Task AddProduct(Product product)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.InsertAsync(product);

                StatusMessage = String.Format("Product {0} has been added", product.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Add Product {0}. Error: {1}", product.id, ex.Message);
            }
        }
        public void AddProducts(List<Product> listProducts)
        {
            int result = 0;
            try
            {
                Init();

                result = sqlConnection.InsertAll(listProducts);

                StatusMessage = String.Format("{0} Product(s) has been added", result);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Products. Error {0}", ex.Message);
            }
        }

        public async Task AddProductsAsync(List<Product> listProducts)
        {
            int result = 0;
            try
            {
                await InitAsync();

                result = await sqlConnectionAsync.InsertAllAsync(listProducts);

                StatusMessage = String.Format("{0} Product(s) has been added", result);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Products. Error {0}", ex.Message);
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.UpdateWithChildrenAsync(product);

                StatusMessage = String.Format("Product {0} as been updated", product.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Update Product {0}. Error: {1}", product.id, ex.Message);
            }
        }

        public async Task DeleteProductById(long id)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.DeleteAsync<Product>(id);

                StatusMessage = String.Format("Product {0} has been deleted", id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Delete Product {0}: Error {1}", id, ex.Message);
            }
        }

        public void DeleteAll()
        {
            try
            {
                Init();

                sqlConnection.DeleteAll<Product>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Delete All Products. Error {0}", ex.Message);
            }
        }
    }
}
