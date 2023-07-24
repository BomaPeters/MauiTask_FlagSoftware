using MauiTask_FlagSoftware.Models;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace MauiTask_FlagSoftware;

public partial class MainPage : ContentPage
{
    private const string apiUrl = "https://dummyjson.com/products?Limit=10";

    public MainPage()
	{
		InitializeComponent();
		LoadData();
	}

	List<Product> productsList = new List<Product>();

	private  async void LoadData()
	{
	HttpClient	client =new HttpClient();//Create Httpclient
		HttpResponseMessage response= await client.GetAsync(apiUrl);//retrieves response with GET VERB
	
		if (response.IsSuccessStatusCode)
		{
			string JsonString= await response.Content.ReadAsStringAsync();
			ProductVM productVM= JsonConvert.DeserializeObject<ProductVM>(JsonString);//Deserialize the jsonstring as an object

			foreach(Product product in productVM.products)
			{
				productsList.Add(new Product() { Id = product.Id, title = product.title, description = product.description, price = product.price, rating = product.rating, stock = product.stock, discountPercentage = product.discountPercentage, brand = product.brand, category = product.category, thumbnail = product.thumbnail, images = product.images });
			}

			productListView.ItemsSource= productsList;
		}
	
	}

}

