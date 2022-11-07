using AdicttMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Repositories
{
    public class ProductCategoryRepository
    {
        string _dbPath;
        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection sqlConnectionAsync;
        
        private SQLiteConnection sqlConnection;

        private void Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteConnection(_dbPath);
            sqlConnection.CreateTable<ProductCategory>();
        }

        private async Task InitAsync()
        {
            if (sqlConnectionAsync != null)
                return;

            sqlConnectionAsync = new SQLiteAsyncConnection(_dbPath);
            await sqlConnectionAsync.CreateTableAsync<ProductCategory>();
        }

        public ProductCategoryRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<ProductCategory>> GetAllProductCategories()
        {
            try
            {
                await InitAsync();

                return await sqlConnectionAsync.Table<ProductCategory>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get all Products Categories. Error: {0}", ex.Message);
            }

            return new List<ProductCategory>();
        }

        public void AddProductCategory(ProductCategory productCategory)
        {
            try
            {
                Init();

                sqlConnection.Insert(productCategory);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add ProductCategory. Error: {0}", ex.Message);
            }
        }

        public async Task AddProductCategoryAsync(ProductCategory productCategory)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.InsertAsync(productCategory);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add ProductCategory. Error: {0}", ex.Message);
            }
        }

        public void DeleteAllProductCategory()
        {
            try
            {
                Init();

                sqlConnection.DeleteAll<Product>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Delete All ProductCategory. Error: {0}", ex.Message);
            }
        }
    }
}
