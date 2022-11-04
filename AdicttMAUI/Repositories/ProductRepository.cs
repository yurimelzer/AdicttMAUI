﻿using AdicttMAUI.Models;
using SQLite;
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

        private SQLiteAsyncConnection sqlConnection;

        private async Task Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteAsyncConnection(_dbPath);
            await sqlConnection.CreateTableAsync<Product>();
        }

        public ProductRepository(string dbPath)
        {
            _dbPath = dbPath;
        }   

        public async Task<List<Product>> GetAllProduct()
        {
            try
            {
                await Init();
                return await (from product in sqlConnection.Table<Product>() orderby product.createdAt select product).ToListAsync();
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
                await Init();
                return await sqlConnection.GetAsync<Product>(id);
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
                await Init();

                await sqlConnection.InsertAsync(product);

                StatusMessage = String.Format("Product {0} has been added", product.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Add Product {0}. Error: {1}", product.id, ex.Message);
            }
        }

        public async Task UpdateProduct(Product product)
        {
            try
            {
                await Init();

                await sqlConnection.UpdateAsync(product);

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
                await Init();

                await sqlConnection.DeleteAsync<Product>(id);

                StatusMessage = String.Format("Product {0} has been deleted", id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Delete Product {0}: Error {1}", id, ex.Message);
            }
        }
    }
}
