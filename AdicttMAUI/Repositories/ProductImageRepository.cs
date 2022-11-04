using AdicttMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Repositories
{
    public class ProductImageRepository
    {
        string _dbPath;

        public string StatusMassage { get; set; }

        private SQLiteAsyncConnection sqlConnection;

        private async Task Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteAsyncConnection(_dbPath);
            await sqlConnection.CreateTableAsync<ProductImage>();
        }

        public ProductImageRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<ProductImage>> GetAllProductImages()
        {
            try
            {
                await Init();

                return await (from productImage in sqlConnection.Table<ProductImage>() orderby productImage.createdAt select productImage).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to Get all Product Images. Error: {0}", ex.Message);
            }

            return new List<ProductImage>();
        }

        public async Task<ProductImage> GetProductImageById(long id)
        {
            try
            {
                await Init();

                return await sqlConnection.GetAsync<ProductImage>(id);
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to Get Product Image {0}. Error: {1}", id, ex.Message);
            }

            return new ProductImage();
        }

        public async Task AddProductImage(ProductImage productImage)
        {
            try
            {
                await Init();

                await sqlConnection.InsertAsync(productImage);

                StatusMassage = String.Format("Product Image {0} has been added", productImage.id);
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to add Product Image {0}. Error: {1}", productImage.id, ex.Message);
            }
        }

        public async Task UpdateProductImage(ProductImage productImage)
        {
            try
            {
                await Init();

                await sqlConnection.UpdateAsync(productImage);

                StatusMassage = String.Format("Product Image {0} has been update", productImage.id);
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to update Product Image {0}. Error: {1}", productImage.id, ex.Message);
            }
        }

        public async Task DeleteProductImageById(long id)
        {
            try
            {
                await Init();

                await sqlConnection.DeleteAsync<ProductImage>(id);

                StatusMassage = String.Format("Product Image {0} has been deleted", id);
            }
            catch (Exception ex)
            {
                StatusMassage = String.Format("Failed to delete Product Image {0}. Error: {1}", id, ex.Message);
            }
        }

    }
}
