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

        private SQLiteAsyncConnection sqlConnection;
        
        private async Task Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteAsyncConnection(_dbPath);
            await sqlConnection.CreateTableAsync<ProductCategory>();
        }

        public ProductCategoryRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<ProductCategory>> GetAllProductCategories()
        {
            try
            {
                await Init();

                return await sqlConnection.Table<ProductCategory>().ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get all Products Categories. Error: {0}", ex.Message);
            }

            return new List<ProductCategory>();
        }

        public async Task AddProductCategory(ProductCategory productCategory)
        {
            try
            {
                await Init();

                await sqlConnection.InsertAsync(productCategory);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add ProductCategory. Error: {0}", ex.Message);
            }
        }
    }
}
