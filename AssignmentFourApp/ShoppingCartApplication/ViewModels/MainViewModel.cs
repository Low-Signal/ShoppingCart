using Newtonsoft.Json;
using ShoppingCartApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Windows.Storage;

namespace ShoppingCartApplication.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Stores the displayed Cart
        public ObservableCollection<Product> Cart { get; set; }

        // Stores the displayed Products in inventory
        public ObservableCollection<Product> Products { get; set; }

        // Prints Subtotal
        public string SubTotal => $"SubTotal:\t{Cart.Sum(i => i.Price):C}";

        // Prints Tax
        public string Tax => $"Tax:\t{Cart.Sum(i => i.Price) * 0.07:C}";

        // Prints Total
        public string Total => $"Total\t{(Cart.Sum(i => i.Price) * 0.07) + Cart.Sum(i => i.Price):C}";

        // String that stores the search text
        public string SearchText { get; set; }

        // Selected inventory item
        public Product SelectedProduct { get; set; }

        // Selected cart item
        public Product SelectedCartItem { get; set; }

        // Sets the list based on deserialized data.
        public MainViewModel()
        {
            Products = new ObservableCollection<Product>();
            Cart = new ObservableCollection<Product>();
            NotifyPropertyChanged();
            NotifyPropertyChanged("SelectedCartItem");
            NotifyPropertyChanged("SelectedProduct");
            NotifyPropertyChanged("SubTotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Searches for items in the inventory that match the SearchText, by description and by name.
        public async System.Threading.Tasks.Task SearchAsync()
        {
            // Gets a list of the matching items
            var handler = new WebRequestHandler();
            var inventory = JsonConvert.DeserializeObject<List<Product>>(await handler.Post("http://localhost/ShoppingCartAPI/InventorySearch/Search", SearchText));

            Products.Clear();
            inventory.ForEach(Products.Add);

            SearchText = "";
            NotifyPropertyChanged("SearchText");
            NotifyPropertyChanged("Display");

        }

        // Adds an item to the users cart (Can change "amount" variable in the call to set custom amount)
        public async System.Threading.Tasks.Task AddToCartAsync()
        {
            // Calls the API to add a product from the inventory to the cart
            var handler = new WebRequestHandler();
            var inventory = JsonConvert.DeserializeObject<List<Product>>(await handler.Post("http://localhost/ShoppingCartAPI/Inventory/add", SelectedProduct));
            Cart.Clear();
            inventory.ForEach(Cart.Add);


            SelectedProduct = null;
            NotifyPropertyChanged("SelectedProduct");
            NotifyPropertyChanged("SubTotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");

            return;
        }


        // Removes an item from cart (Can change "amount" variable to set custom amount)
        public async System.Threading.Tasks.Task RemoveFromCartAsync()
        {
            // Calls the API to remove an item from the cart.
            var handler = new WebRequestHandler();
            var newCart = JsonConvert.DeserializeObject<List<Product>>(await handler.Post("http://localhost/ShoppingCartAPI/Inventory/remove", SelectedCartItem));
            Cart.Clear();
            newCart.ForEach(Cart.Add);

            // Deselects the product
            SelectedCartItem = null;
            NotifyPropertyChanged("SelectedCartItem");
            NotifyPropertyChanged("SubTotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");
            return;
        }

        // Calls the API to print a reciept and clear the cart.
        public async System.Threading.Tasks.Task CheckoutAsync()
        {
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost/ShoppingCartAPI/Inventory/checkout", Cart);

            // Clears the cart
            Cart.Clear();

            NotifyPropertyChanged("SubTotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");
        }

        // Clears the API in the cart and application
        public async System.Threading.Tasks.Task ClearCartAsync()
        {
            var handler = new WebRequestHandler();
            await handler.Post("http://localhost/ShoppingCartAPI/Inventory/clearcart", Cart);

            Cart.Clear();

            NotifyPropertyChanged("SubTotal");
            NotifyPropertyChanged("Tax");
            NotifyPropertyChanged("Total");

            return;
        }

    }
}
