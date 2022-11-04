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
		List<ProductVariant> listVariantApi = await TiendanubeAdictt.GetAllProductVariants();
		List<ProductImage> listImages = await TiendanubeAdictt.GetAllProductImages();
		List<Category> category = await TiendanubeAdictt.GetAllCategories();
    }
}

