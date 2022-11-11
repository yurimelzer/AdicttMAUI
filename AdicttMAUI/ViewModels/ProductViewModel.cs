using AdicttMAUI.Models;
using AdicttMAUI.REST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdicttMAUI.ViewModels
{
    public class ProductViewModel : BindableObject
    {
        public List<Product> produtosCollection { get; set; }

        public List<object> drops { get; set; }

        public ICommand goToProduct { get; private set; }

        public ProductViewModel()
        {
            Init();

            goToProduct = new Command(GoToProduct);
        }

        private async void Init()
        {
            List<Product> listProductApi = await TiendanubeAdictt.GetAllProducts();
            List<Category> listCategoryApi = await TiendanubeAdictt.GetAllCategories();

            App.ProductCategoryRepository.DeleteAllProductCategory();

            App.ProductRepository.AddProducts(listProductApi);
            App.CategoryRepository.AddCategories(listCategoryApi);

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

            produtosCollection = App.ProductRepository.GetAllProduct();

            drops = new List<object>
            {
                new { source = "https://d2r9epyceweg5n.cloudfront.net/stores/001/573/374/themes/amazonas/1-slide-1658883886247-7850662981-68da9c5b5ff0752338ee02c0644561671658883889-1024-1024.webp?2119636346" },
                new { source ="https://d2r9epyceweg5n.cloudfront.net/stores/001/573/374/themes/amazonas/1-slide-1644253915918-8288848822-e0511b53dd3fff458086e03e840bfdda1644253918-1024-1024.webp?2119636346" }
            };
        }

        private void GoToProduct(object param)
        {

        }
    }
}
