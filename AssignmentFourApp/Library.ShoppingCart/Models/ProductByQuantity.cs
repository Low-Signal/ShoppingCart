using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace ShoppingCartApplication.Models
{
    [JsonConverter(typeof(ProductJsonConverter))]
    public class ProductByQuantity : Product, INotifyPropertyChanged
    {
        public double UnitPrice;   // The products price per unit
        public int Units;          // The number of units stored

        // Constructor
        public ProductByQuantity(string name, string description, int id, double unitPrice, int units, string typeOfProduct)
        {
            Name = name;
            Description = description;
            Id = id;
            UnitPrice = unitPrice;
            Units = units;
            TypeOfProduct = typeOfProduct;
            Amount = units;

            NotifyPropertyChanged();
        }

        public ProductByQuantity()
        {
            NotifyPropertyChanged();
        }

        // Calculates and overrides the price member data
        public override double Price
        {
            get
            {
                return UnitPrice * Units;
            }
            set
            {
                //Price = UnitPrice * Units;
            }
        }

        // Gets the price of one unit
        public override double getItemPrice()
        {
            return UnitPrice;
        }

        // Gets the amount of units of a specific product
        public override double getAmount()
        {
            return Units;
        }

        // Sets the amount of a product baed a given input
        public override void setAmount(double newAmount)
        {
            Amount = Convert.ToInt32(newAmount);
            Units = Convert.ToInt32(newAmount);
            return;
        }

        public override string UpdateCartString
        {
            get
            {
                return $"{Price:C}\t{Units} Unit(s) of {Name} - {Description}";
            }
        }

    }
}