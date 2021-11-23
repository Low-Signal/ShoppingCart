
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;


namespace ShoppingCartApplication.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class Product : INotifyPropertyChanged
    {
        public virtual double Price { get; set; }       // The price of the product
        public string Name { get; set; }                // The name of the product
        public string Description { get; set; }         // The product description
        public int Id { get; set; }                     // The product Id
        public string TypeOfProduct { get; set; }       // Tag for the type of product "Cart" and "Inventory" are supported

        public double Amount { get; set; }

        // Default Constructor
        public Product()
        { }

        // Constructor
        public Product(double price, string name, string description, int id, double amount)
        {
            Price = price;
            Name = name;
            Description = description;
            Id = id;
            Amount = amount;

            NotifyPropertyChanged();
        }

        // Gets the price of a product
        public virtual double getItemPrice()
        {
            return Price;
        }

        // Gets the amount of a product
        public virtual double getAmount()
        {
            return Amount;
        }

        // Sets the amount of a product
        public virtual void setAmount(double newAmount)
        {
            return;
        }

        public virtual string Display
        {
            get
            {
                return $"{getItemPrice():C} - {Name} - {Description}";
            }
        }

        public virtual string UpdateCartString
        {
            get
            {
                return $"{Price:C} ---- {Name} - {Description}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

