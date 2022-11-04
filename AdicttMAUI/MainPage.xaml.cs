using AdicttMAUI.Models;
using AdicttMAUI.Repositories;
using AdicttMAUI.REST;

namespace AdicttMAUI;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);

		ProductDebugRepository();
	}

	private async void ProductDebugRepository()
	{
		List<Product> listProductApi = await TiendanubeAdictt.GetAllProducts();

		await App.ProductRepository.AddProducts(listProductApi);

		List<Product> listProductDb = await App.ProductRepository.GetAllProduct();
		List<ProductCategory> listProductCategory = await App.ProductCategoryRepository.GetAllProductCategories();

		foreach (Product product in listProductApi)
		{
			await App.ProductImageRepository.AddProductImages(product.images);
			await App.ProductVariantRepository.AddProductVariants(product.variants);

			foreach(Category category in product.categories)
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

