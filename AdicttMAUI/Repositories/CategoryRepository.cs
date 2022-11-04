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

        public string StatusMassage { get; set; }

        private SQLiteAsyncConnection sqlConnection;

        private async Task Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteAsyncConnection(_dbPath);
            await sqlConnection.CreateTableAsync<Category>();
        }

        public CategoryRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                await Init();

                return await (from category in sqlConnection.Table<Category>() orderby category.title select category).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to Get all Categories. Error: {0}", ex.Message);
            }

            return new List<Category>();
        }

        public async Task<Category> GetCategoryById(long id)
        {
            try
            {
                await Init();

                return await sqlConnection.GetAsync<Category>(id);
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to Get Category {0}. Error: {1}", id, ex.Message);
            }

            return new Category();
        }

        public async Task AddCategory(Category category)
        {
            try
            {
                await Init();

                await sqlConnection.InsertAsync(category);

                StatusMassage = String.Format("Category {0} has been added", category.id);
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to add Category {0}. Error: {1}", category.id, ex.Message);
            }
        }

        public async Task UpdatedCategory(Category category)
        {
            try
            {
                await Init();

                await sqlConnection.UpdateAsync(category);

                StatusMassage = String.Format("Category {0} has been updated", category.id);
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to update Category {0}. Error: {1}", category.id, ex.Message);
            }
        }

        public async Task DeleteCategoryById(long id)
        {
            try
            {
                await Init();

                await sqlConnection.DeleteAsync<Category>(id);

                StatusMassage = String.Format("Category {0} has been deleted", id);
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to delete Category {0}. Error: {1}", id, ex.Message);
            }
        }
    }
}
