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

        private SQLiteAsyncConnection sqlConnection;

        private async Task Init()
        {
            if (sqlConnection != null)
                return;

            sqlConnection = new SQLiteAsyncConnection(_dbPath);
            await sqlConnection.CreateTableAsync<ProductVariant>();
        }

        public ProductVariantRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task<List<ProductVariant>> GetAllProductVariants()
        {
            try
            {
                await Init();

                return await (from productsVariant in sqlConnection.Table<ProductVariant>() orderby productsVariant.createdAt select productsVariant).ToListAsync();
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
                await Init();

                return await sqlConnection.GetAsync<ProductVariant>(id);
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
                await Init();

                await sqlConnection.InsertAsync(variant);

                StatusMessage = String.Format("Product Variant {0} has been added", variant.id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to add Product Variant {0}. Error: {1}", variant.id, ex.Message);
            }
        }

        public async Task UpdateProductVariant(ProductVariant variant)
        {
            try
            {
                await Init();

                await sqlConnection.UpdateAsync(variant);

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
                await Init();

                await sqlConnection.DeleteAsync<ProductVariant>(id);

                StatusMessage = String.Format("Product Variant {0} has been deleted", id);
            }
            catch (Exception ex)
            {
                StatusMessage = String.Format("Failed to delete Product Variant {0}; Error: {1}", id, ex.Message);
            }
        }


    }
}
