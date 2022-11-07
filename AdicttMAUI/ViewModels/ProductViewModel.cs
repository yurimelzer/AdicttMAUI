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
            this.GetApiData();

            produtosCollection = App.ProductRepository.GetAllProduct();
        }

        private void GetApiData()
        {
            List<Product> listProductApi = TiendanubeAdictt.GetAllProducts();

            App.ProductCategoryRepository.DeleteAllProductCategory();

            //Excluir depois
            App.ProductImageRepository.DeleteAllProductImages();

            App.ProductRepository.AddProducts(listProductApi);

            foreach (Product product in listProductApi)
            {
                App.ProductImageRepository.AddProductImages(product.images);
                App.ProductVariantRepository.AddProductVariants(product.variants);

                foreach (Category category in product.categories)
                {
                    ProductCategory productCategory = new ProductCategory
                    {
                        productId = product.id,
                        categoryId = category.id
                    };

                    App.ProductCategoryRepository.AddProductCategory(productCategory);
                }
            }

            List<Category> listCategoryApi = TiendanubeAdictt.GetAllCategories();

            App.CategoryRepository.AddCategoriesAsync(listCategoryApi);

            produtosCollection = App.ProductRepository.GetAllProduct();
        }
    }
}
