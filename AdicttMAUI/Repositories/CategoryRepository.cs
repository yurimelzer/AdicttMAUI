using AdicttMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Repositories
{
    public class CategoryRepository
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
            sqlConnection.CreateTable<Category>();
        }

        private async Task InitAsync()
        {
            if (sqlConnectionAsync != null)
                return;

            sqlConnectionAsync = new SQLiteAsyncConnection(_dbPath);
            await sqlConnectionAsync.CreateTableAsync<Category>();
        }

        public CategoryRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                await InitAsync();

                return await (from category in sqlConnectionAsync.Table<Category>() orderby category.title select category).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get all Categories. Error: {0}", ex.Message);
            }

            return new List<Category>();
        }

        public async Task<Category> GetCategoryById(long id)
        {
            try
            {
                await InitAsync();

                return await sqlConnectionAsync.GetAsync<Category>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get Category {0}. Error: {1}", id, ex.Message);
            }

            return new Category();
        }

        public void AddCategories(List<Category> listCategory)
        {
            int result = 0;
            try
            {
                Init();

                result = sqlConnection.InsertAll(listCategory);

                StatusMessage = String.Format("{0} Category(es) has been added", result);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Categories. Error {0}", ex.Message);
            }
        }


        public async Task AddCategoriesAsync(List<Category> listCategory)
        {
            int result = 0;
            try
            {
                await InitAsync();

                result = await sqlConnectionAsync.InsertAllAsync(listCategory);

                StatusMessage = String.Format("{0} Category(es) has been added", result);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Categories. Error {0}", ex.Message);
            }
        }

        public async Task AddCategory(Category category)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.InsertAsync(category);

                StatusMessage = String.Format("Category {0} has been added", category.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Category {0}. Error: {1}", category.id, ex.Message);
            }
        }

        public async Task UpdatedCategory(Category category)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.UpdateAsync(category);

                StatusMessage = String.Format("Category {0} has been updated", category.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to update Category {0}. Error: {1}", category.id, ex.Message);
            }
        }

        public async Task DeleteCategoryById(long id)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.DeleteAsync<Category>(id);

                StatusMessage = String.Format("Category {0} has been deleted", id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to delete Category {0}. Error: {1}", id, ex.Message);
            }
        }
    }
}
