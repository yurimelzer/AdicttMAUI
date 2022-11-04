using AdicttMAUI.Helpers;
using AdicttMAUI.Repositories;

namespace AdicttMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		string dbPath = FileAccessHelper.GetLocalFilePath("adictt.db3");

		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ProductRepository>(s, dbPath));
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ProductVariantRepository>(s, dbPath));
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ProductImageRepository>(s, dbPath));
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CategoryRepository>(s, dbPath));
		builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<ProductCategoryRepository>(s, dbPath));

		return builder.Build();
	}
}
