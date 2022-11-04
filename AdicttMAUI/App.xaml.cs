using AdicttMAUI.Repositories;

namespace AdicttMAUI;

public partial class App : Application
{

	public static ProductRepository ProductRepository { get; set; }
	public static ProductVariantRepository ProductVariantRepository { get; set; }
	public static ProductImageRepository ProductImageRepository { get; set; }
	public static CategoryRepository CategoryRepository { get; set; }
	public static ProductCategoryRepository ProductCategoryRepository { get; set; }

	public App(ProductRepository productRepo, ProductVariantRepository productVariantRepo, ProductImageRepository productImageRepo, CategoryRepository caregoryRepo, ProductCategoryRepository productCategoryRepo)
	{
		InitializeComponent();

		MainPage = new AppShell();

		ProductRepository = productRepo;
		ProductVariantRepository = productVariantRepo;
		ProductImageRepository = productImageRepo;
		CategoryRepository = caregoryRepo;
		ProductCategoryRepository = productCategoryRepo;
    }
}
