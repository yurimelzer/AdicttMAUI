using AdicttMAUI.Models;
using AdicttMAUI.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.ViewModels
{
    public class ProductViewModel
    {
        public List<Product> produtosCollection { get; private set; }

        public ProductViewModel()
        {
            //this.GetApiData();

            produtosCollection = App.ProductRepository.GetAllProduct();
        }

        private async void GetApiData()
        {
            List<Product> listProductApi = await TiendanubeAdictt.GetAllProducts();

            await App.ProductRepository.AddProducts(listProductApi);

            List<ProductCategory> listProductCategory = await App.ProductCategoryRepository.GetAllProductCategories();

            foreach (Product product in listProductApi)
            {
                await App.ProductImageRepository.AddProductImages(product.images);
                await App.ProductVariantRepository.AddProductVariants(product.variants);

                foreach (Category category in product.categories)
                {
                    ProductCategory productCategory = new ProductCategory
                    {
                        productId = product.id,
                        categoryId = category.id
                    };

                    await App.ProductCategoryRepository.AddProductCategory(productCategory);
                }
            }

            List<Category> listCategoryApi = await TiendanubeAdictt.GetAllCategories();

            await App.CategoryRepository.AddCategories(listCategoryApi);
        }
    }
}
