using AdicttMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.Repositories
{
    public class ProductVariantRepository
    {
        string _dbPath;

        public string StatusMessage { get; set; }

        private SQLiteAsyncConnection sqlConnectionAsync;

        private SQLiteConnection sqlConnection;

        private async void Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteConnection(_dbPath);
            sqlConnection.CreateTable<ProductVariant>();
        }

        private async Task InitAsync()
        {
            if (sqlConnectionAsync != null)
                return;

            sqlConnectionAsync = new SQLiteAsyncConnection(_dbPath);
            await sqlConnectionAsync.CreateTableAsync<ProductVariant>();
        }

        public ProductVariantRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<ProductVariant>> GetAllProductVariants()
        {
            try
            {
                await InitAsync();

                return await (from productsVariant in sqlConnectionAsync.Table<ProductVariant>() orderby productsVariant.createdAt select productsVariant).ToListAsync();
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get all Product Variants. Error {0}", ex.Message);
            }

            return new List<ProductVariant>();
        }

        public async Task<ProductVariant> GetProductVariantById(long id)
        {
            try
            {
                await InitAsync();

                return await sqlConnectionAsync.GetAsync<ProductVariant>(id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to Get Product Variant {0}. Error {1}", id, ex.Message);
            }

            return new ProductVariant();
        }

        public async Task AddProductVariant(ProductVariant variant)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.InsertAsync(variant);

                StatusMessage = String.Format("Product Variant {0} has been added", variant.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Product Variant {0}. Error: {1}", variant.id, ex.Message);
            }
        }

        public void AddProductVariants(List<ProductVariant> listProductVariants)
        {
            int result = 0;
            try
            {
                Init();

                result = sqlConnection.InsertAll(listProductVariants);

                StatusMessage = String.Format("{0} Product Variant(s) has been added", result);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Product Variants. Error {0}", ex.Message);
            }
        }

        public async Task AddProductVariantsAsync(List<ProductVariant> listProductVariants)
        {
            int result = 0;
            try
            {
                await InitAsync();

                result = await sqlConnectionAsync.InsertAllAsync(listProductVariants);

                StatusMessage = String.Format("{0} Product Variant(s) has been added", result);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Product Variants. Error {0}", ex.Message);
            }
        }

        public async Task UpdateProductVariant(ProductVariant variant)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.UpdateAsync(variant);

                StatusMessage = String.Format("Product Variant {0} has been updated", variant.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to update Product Variant {0}. Error: {1}", variant.id, ex.Message);
            }
        }

        public async Task DeleteProductVariantById(long id)
        {
            try
            {
                await InitAsync();

                await sqlConnectionAsync.DeleteAsync<ProductVariant>(id);

                StatusMessage = String.Format("Product Variant {0} has been deleted", id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to delete Product Variant {0}; Error: {1}", id, ex.Message);
            }
        }


    }
}
