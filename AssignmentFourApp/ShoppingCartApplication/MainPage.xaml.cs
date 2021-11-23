using Newtonsoft.Json;
using ShoppingCartApplication.Models;
using ShoppingCartApplication.ViewModels;
using ShoppingCartApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ShoppingCartApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [JsonConverter(typeof(ProductJsonConverter))]
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

            InitializeComponent();
            DataContext = new MainViewModel();

            var handler = new WebRequestHandler();
            var inventory = JsonConvert.DeserializeObject<List<Product>>(handler.Get("http://localhost/ShoppingCartAPI/Inventory/GetInventory").Result);
            var cart = JsonConvert.DeserializeObject<List<Product>>(handler.Get("http://localhost/ShoppingCartAPI/Inventory/GetCart").Result);
            var context = DataContext as MainViewModel;

            // Fills the inventory and cart with the deseialized data.
            inventory.ForEach(context.Products.Add);
            cart.ForEach(context.Cart.Add);

        }

        private void ListBoxInventory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = (DataContext as MainViewModel).AddToCartAsync();
        }

        private void ListBoxCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = (DataContext as MainViewModel).RemoveFromCartAsync();
        }

        private async void Checkout_Click(object sender, RoutedEventArgs e)
        {
            var  _ = (DataContext as MainViewModel).CheckoutAsync();
            await _;
        }

        private async void Clear_Cart_Click(object sender, RoutedEventArgs e)
        {
            var _ =(DataContext as MainViewModel).ClearCartAsync();
            await _;
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            var _ =(DataContext as MainViewModel).SearchAsync();
        }
    }
}
