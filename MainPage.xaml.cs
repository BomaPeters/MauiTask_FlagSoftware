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
     //   LoadData();
	}

	//List<Product> productsList = new List<Product>();

	//private  async void LoadData()
	//{
	//	try {
	//       if(Connectivity.NetworkAccess== NetworkAccess.None)
	//           {
	//               await DisplayAlert("Error", "Internet not available.Please check your connection", "OK");
	//           }
	//           else { 

	//       HttpClient	client =new HttpClient();//Create Httpclient
	//	HttpResponseMessage response= await client.GetAsync(apiUrl);//retrieves response with GET VERB

	//	if (response.IsSuccessStatusCode)
	//	{
	//		string JsonString= await response.Content.ReadAsStringAsync();
	//		ProductVM productVM= JsonConvert.DeserializeObject<ProductVM>(JsonString);//Deserialize the jsonstring as an object

	//		foreach(Product product in productVM.products)
	//		{
	//			productsList.Add(new Product() { Id = product.Id, title = product.title, description = product.description, price = product.price, rating = product.rating, stock = product.stock, discountPercentage = product.discountPercentage, brand = product.brand, category = product.category, thumbnail = product.thumbnail, images = product.images });
	//		}

	//		productListView.ItemsSource= productsList;
	//           }
	//           else
	//           {
	//               await DisplayAlert("Error", "The API response was not successful", "OK");
	//           }

	//           }

	//       }
	//       catch (Exception ex)
	//       {
	//           await DisplayAlert("Error", ex.Message.ToString(), "OK");
	//       }
	//   }

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
}

