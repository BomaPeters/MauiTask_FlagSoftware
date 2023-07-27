using MauiTask_FlagSoftware.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using MauiTask_FlagSoftware.Views;
using MauiTask_FlagSoftware.ViewModels;

namespace MauiTask_FlagSoftware;

public partial class MainPage : ContentPage
{
    private const string apiUrl = "https://dummyjson.com/products?Limit=10";

    public MainPage()
	{
		InitializeComponent();
        BindingContext =  new MainPageViewModel();
     
	}

	

	private async void productListView_ItemTapped(object sender, ItemTappedEventArgs e)
	{
		try
		{
			if (e.Item is Product selectedProduct)
			{
				await Navigation.PushAsync(new ProductDetailsPage { BindingContext = selectedProduct });
			}
				 ((ListView)sender).SelectedItem = null;
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", ex.Message.ToString(), "OK");
		}
	}

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
		var viewmodel = BindingContext as MainPageViewModel;

		viewmodel?.SortByCategoryCommand.Execute(this);
    }
}

