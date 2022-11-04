using AdicttMAUI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdicttMAUI.REST
{
    public static class TiendanubeAdictt
    {
        static string _jsonResponse;

        public static async Task<List<Product>> GetAllProducts()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.tiendanube.com/v1/");
            client.DefaultRequestHeaders.Add("Authentication", "bearer c702a62983ee8b062e5cbdf78ef4d4d93df0b469");
            client.DefaultRequestHeaders.Add("User-Agent", "Bruno");

            string url = "1573374/products";
            HttpResponseMessage response = client.GetAsync(url).Result;
            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<Product> listProduct = new List<Product>();

            JArray jsonListProduct = JArray.Parse(jsonResponse);

            foreach (JObject jsonProduct in jsonListProduct)
            {
                listProduct.Add(JsonToProduct(jsonProduct));
            }

            return listProduct;
        }

        public static async Task<List<Category>> GetAllCategories()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.tiendanube.com/v1/");
            client.DefaultRequestHeaders.Add("Authentication", "bearer c702a62983ee8b062e5cbdf78ef4d4d93df0b469");
            client.DefaultRequestHeaders.Add("User-Agent", "Bruno");

            string url = "1573374/categories";
            HttpResponseMessage response = client.GetAsync(url).Result;
            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<Category> listCategory = new List<Category>();

            JArray jsonCategories = JArray.Parse(jsonResponse);

            foreach (JObject categoria in jsonCategories)
            {
                listCategory.Add(JsonToCategory(categoria));
            }

            return listCategory;
        }

        private static Product JsonToProduct(JObject jsonProduct)
        {
            Product objProduto = new Product();

            JArray jsonVariants = JArray.Parse(jsonProduct["variants"].ToString());
            JArray jsonImages = JArray.Parse(jsonProduct["images"].ToString());
            JArray jsonCategories = JArray.Parse(jsonProduct["categories"].ToString());

            objProduto.id = long.Parse(jsonProduct["id"].ToString());
            objProduto.name = jsonProduct["name"]["pt"].ToString();
            objProduto.description = jsonProduct["description"].ToString();

            int stock = 0;

            objProduto.variants = new List<ProductVariant>();
            foreach (JObject variant in jsonVariants)
            {
                stock += int.Parse(variant["stock"].ToString());
                objProduto.variants.Add(JsonToVariant(variant));
            }

            objProduto.images = new List<ProductImage>();
            foreach(JObject image in jsonImages)
            {
                objProduto.images.Add(JsonToProductImage(image));
            }

            objProduto.categories = new List<Category>();
            foreach (JObject category in jsonCategories)
            {
                objProduto.categories.Add(JsonToCategory(category));
            }

            objProduto.stock = stock;
            objProduto.specification = jsonProduct["seo_description"]["pt"].ToString();
            objProduto.brand = jsonProduct["brand"].ToString();
            objProduto.freeShipping = bool.Parse(jsonProduct["free_shipping"].ToString());
            objProduto.createdAt = DateTime.Parse(jsonProduct["created_at"].ToString());
            objProduto.updatedAt = DateTime.Parse(jsonProduct["updated_at"].ToString());
            objProduto.tags = jsonProduct["tags"].ToString();

            return objProduto;
        }

        private static ProductVariant JsonToVariant(JObject jsonVariant)
        {
            ProductVariant objVariant = new ProductVariant();

            objVariant.id = long.Parse(jsonVariant["id"].ToString());
            objVariant.produtctId = long.Parse(jsonVariant["product_id"].ToString());
            objVariant.position = int.Parse(jsonVariant["position"].ToString());

            double.TryParse(jsonVariant["price"].ToString(), out double result);
            objVariant.price = result;

            double.TryParse(jsonVariant["promotional_price"].ToString(), out result);
            objVariant.promotionalPrice = result;

            objVariant.stock = int.Parse(jsonVariant["stock"].ToString());

            double.TryParse(jsonVariant["weight"].ToString(), out result);
            objVariant.weight = result;

            double.TryParse(jsonVariant["width"].ToString(), out result);
            objVariant.width = result;

            double.TryParse(jsonVariant["height"].ToString(), out result);
            objVariant.height = result;

            double.TryParse(jsonVariant["depth"].ToString(), out result);
            objVariant.depth = result;

            objVariant.color = jsonVariant["values"][0]["pt"].ToString();
            objVariant.size = jsonVariant["values"][1]["pt"].ToString();
            objVariant.gender = jsonVariant["gender"].ToString();
            objVariant.barcode = jsonVariant["barcode"].ToString();

            objVariant.createdAt = DateTime.Parse(jsonVariant["created_at"].ToString());
            objVariant.updatedAt = DateTime.Parse(jsonVariant["updated_at"].ToString());

            return objVariant;
        }

        public static Category JsonToCategory(JObject jsonCategoria)
        {
            Category objCategoria = new Category();

            objCategoria.id = long.Parse(jsonCategoria["id"].ToString());

            long.TryParse(jsonCategoria["parent"].ToString(), out long result);
            objCategoria.parent = result;

            objCategoria.name = jsonCategoria["name"]["pt"].ToString();
            objCategoria.title = jsonCategoria["seo_title"]["pt"].ToString();
            objCategoria.description = jsonCategoria["description"]["pt"].ToString();

            return objCategoria;
        }

        public static ProductImage JsonToProductImage(JObject jsonImage)
        {
            ProductImage objImage = new ProductImage();

            objImage.id = long.Parse(jsonImage["id"].ToString());
            objImage.productId = long.Parse(jsonImage["product_id"].ToString());
            objImage.source = jsonImage["src"].ToString();
            objImage.position = int.Parse(jsonImage["position"].ToString());
            objImage.createdAt = DateTime.Parse(jsonImage["created_at"].ToString());
            objImage.updatedAt = DateTime.Parse(jsonImage["updated_at"].ToString());

            return objImage;
        }
    }
}
