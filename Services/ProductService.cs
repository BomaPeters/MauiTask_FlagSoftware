
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiTask_FlagSoftware.Models;
using Newtonsoft.Json;


namespace MauiTask_FlagSoftware.Services
{
   public class ProductService
    {
        private const string apiUrl = "https://dummyjson.com/products?Limit=10";
        List<Product> myproducts = new List<Product>();
        public ProductService() {
         //   GetProductAsync();
        
        }

        public async Task< List<Product>> GetProductAsync()
        {
            HttpClient Client = new HttpClient();
            HttpResponseMessage responseMessage = await Client.GetAsync(apiUrl);

            if (responseMessage.IsSuccessStatusCode)
            {
                string json = await responseMessage.Content.ReadAsStringAsync();
                ProductVM products = JsonConvert.DeserializeObject<ProductVM>(json);
                foreach(var  product in products.products)
                {
                    myproducts.Add( new Product { Id = product.Id, title = product.title, description = product.description, price = product.price, rating = product.rating, stock = product.stock, discountPercentage = product.discountPercentage, brand = product.brand, category = product.category, thumbnail = product.thumbnail, images = product.images });
                }

                return  myproducts;
            }
            else
            {
            await    Application.Current.MainPage.DisplayAlert("Error", "Bad Request from server", "OK");
                return null;
            }

         
        }

       
    }
}
