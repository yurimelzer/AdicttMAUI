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

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection sqlConnectionAsync;

        private SQLiteConnection sqlConnection;

        private void Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteConnection(_dbPath);
            sqlConnection.CreateTable<ProductImage>();
        }

        private async Task InitAsync()
        {
            if (sqlConnectionAsync != null)
                return;

            sqlConnectionAsync = new SQLiteAsyncConnection(_dbPath);
            await sqlConnectionAsync.CreateTableAsync<ProductImage>();
        }

        public ProductImageRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<ProductImage>> GetAllProductImages()
        {
            try
            {
                await InitAsync();

                return await (from productImage in sqlConnectionAsync.Table<ProductImage>() orderby productImage.createdAt select productImage).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get all Product Images. Error: {0}", ex.Message);
            }

            return new List<ProductImage>();
        }

        public async Task<ProductImage> GetProductImageById(long id)
        {
            try
            {
                await InitAsync();

                return await sqlConnectionAsync.GetAsync<ProductImage>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get Product Image {0}. Error: {1}", id, ex.Message);
            }

            return new ProductImage();
        }

        public async Task AddProductImage(ProductImage productImage)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.InsertAsync(productImage);

                StatusMessage = String.Format("Product Image {0} has been added", productImage.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Product Image {0}. Error: {1}", productImage.id, ex.Message);
            }
        }

        public void AddProductImages(List<ProductImage> listProductImage)
        {
            int result = 0;
            try
            {
                Init();

                result = sqlConnection.InsertAll(listProductImage);

                StatusMessage = String.Format("{0} Product Images(s) has been added", result);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Product Images. Error {0}", ex.Message);
            }
        }

        public async Task AddProductImagesAsync(List<ProductImage> listProductImage)
        {
            int result = 0;
            try
            {
                await InitAsync();

                result = await sqlConnectionAsync.InsertAllAsync(listProductImage);

                StatusMessage = String.Format("{0} Product Images(s) has been added", result);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Product Images. Error {0}", ex.Message);
            }
        }

        public async Task UpdateProductImage(ProductImage productImage)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.UpdateAsync(productImage);

                StatusMessage = String.Format("Product Image {0} has been update", productImage.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to update Product Image {0}. Error: {1}", productImage.id, ex.Message);
            }
        }

        public async Task DeleteProductImageById(long id)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.DeleteAsync<ProductImage>(id);

                StatusMessage = String.Format("Product Image {0} has been deleted", id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to delete Product Image {0}. Error: {1}", id, ex.Message);
            }
        }

        public void DeleteAllProductImages()
        {
            try
            {
                Init();

                sqlConnection.DeleteAll<ProductImage>();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to delete All Product Images. Error: {0}", ex.Message);
            }
        }

    }
}
