
using Newtonsoft.Json;
using System.ComponentModel;

namespace ShoppingCartApplication.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class ProductByWeight : Product, INotifyPropertyChanged
    {
        public double PricePerOunce;
        public double Ounces;

        // Constructor
        public ProductByWeight(string name, string description, int id, double pricePerOunce, double ounces, string typeOfProduct)
        {
            Name = name;
            Description = description;
            Id = id;
            PricePerOunce = pricePerOunce;
            Ounces = ounces;
            TypeOfProduct = typeOfProduct;
            Amount = ounces;

            NotifyPropertyChanged();
        }

        public ProductByWeight()
        {
            NotifyPropertyChanged();
        }


        // Calculates and overrides the price member data
        public override double Price
        {
            get
            {
                return PricePerOunce * Ounces;
            }
            set
            {
                //Price = PricePerOunce * Ounces;
            }
        }

        // Gets the price of one ounce
        public override double getItemPrice()
        {
            return PricePerOunce;
        }

        // Gets the number of ounces of a product
        public override double getAmount()
        {
            return Ounces;
        }

        // Sets the amount of a product
        public override void setAmount(double newAmount)
        {
            Ounces = newAmount;
            return;
        }
        public override string UpdateCartString
        {
            get
            {
                return $"{Price:C}\t{Ounces} Ounces(s) of {Name} - {Description}";
            }
        }
    }
}
