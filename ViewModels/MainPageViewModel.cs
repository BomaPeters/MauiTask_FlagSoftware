using MauiTask_FlagSoftware.Models;
using MauiTask_FlagSoftware.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiTask_FlagSoftware.ViewModels
{
 public class MainPageViewModel:INotifyPropertyChanged
    {
        private List<Product> productsList;

      

        public List<Product> ProductList {
            get
            {
                return productsList;
            } 
            set {
                productsList = value;
                OnPropertyChanged();
            } }
        public event PropertyChangedEventHandler PropertyChanged;
     
        //protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        void OnPropertyChanged( string name=null)
        {
            PropertyChanged?.Invoke( this, new PropertyChangedEventArgs( name ) );
        }
        public MainPageViewModel() {
          
             if (Connectivity.NetworkAccess==NetworkAccess.Internet)
            {
             LoadData();
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please checked your internet connection", "OK");
            }
        }
         private async Task  LoadData()
        {
            try
            {
                var productService = new ProductService();
                ProductList = await productService.GetProductAsync();
            }
            catch(Exception ex) {
              await  Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
            
        }
        public async Task<bool> IsInternetConnected()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
