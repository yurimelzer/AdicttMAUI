using AdicttMAUI.Models;
using AdicttMAUI.Repositories;
using AdicttMAUI.REST;
using AdicttMAUI.ViewModels;

namespace AdicttMAUI.Views;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
		this.BindingContext = new ProductViewModel();
	}
}

