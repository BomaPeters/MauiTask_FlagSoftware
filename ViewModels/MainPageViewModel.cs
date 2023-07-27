using MauiTask_FlagSoftware.Models;
using MauiTask_FlagSoftware.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiTask_FlagSoftware.ViewModels
{
 public class MainPageViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Product> productsList;


        private List<Product> _originalProductList;
        private string _selectedCategory;
        private ObservableCollection<Product> _sortedProductList;


        public ObservableCollection<string> CategoryItems { get; set; } = new ObservableCollection<string>();

        public MainPageViewModel()
        {

            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                LoadData();
                SortByCategoryCommand = new Command(SortProductList);
            }
            else
            {
                Application.Current.MainPage.DisplayAlert("Error", "Please checked your internet connection", "OK");
            }
        }
       
        public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<Product> ProductList
        {
            get => _sortedProductList;
            set
            {
                _sortedProductList = value;
                OnPropertyChanged(nameof(ProductList));
            }
        }

        public string SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                SortByCategoryCommand.Execute(null);
            }
        }

        public Command SortByCategoryCommand { get; }
        private async void LoadData()
        {
            try { 
            // Load data and store the original list
            var productService = new ProductService();

           // productsList= 
            _originalProductList = await productService.GetProductAsync(); ;
            ProductList = new ObservableCollection<Product>(_originalProductList);

            // Populate the categories for the Picker
            foreach (var category in _originalProductList.Select(p => p.category).Distinct())
            {
                Categories.Add(category);
            }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private void SortProductList()
        {
            // Filter the original list based on the selected category
            if (!string.IsNullOrEmpty(SelectedCategory))
            {
                var filteredList = _originalProductList.Where(p => p.category == SelectedCategory).ToList();
                ProductList = new ObservableCollection<Product>(filteredList);
            }
            else
            {
                // If no category is selected, display the original unsorted list
                ProductList = new ObservableCollection<Product>(_originalProductList);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       

        
        
      
    }
}
