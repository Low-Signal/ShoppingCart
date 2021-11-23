using ShoppingCartApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartAPI
{
    public class DataContext
    {
        public static List<Product> Inventory = new List<Product> {
            new ProductByQuantity("Milk", "One gallon, 95% dairy!", 1, 4.29, 1, "unit"),
            new ProductByQuantity("Eggs", "One dozen, Yolking good", 2, 2.99, 1, "unit"),
            new ProductByQuantity("Bread", "Heated yeast!", 3, 3.99, 1, "unit"),
            new ProductByQuantity("Flour", "One Pound, pre-bread.", 4, 4.99, 1, "unit"),
            new ProductByQuantity("Nut Butter", "Pureed nuts, ouch!", 5, 5.99, 1, "unit"),
            new ProductByQuantity("Grape Jelly", "0% real fruit", 6, 3.99, 1, "unit"),
            new ProductByQuantity("Water", "24 pack, Liquid air", 7, 4.99, 1, "unit"),
            new ProductByQuantity("Orange Juice", "One gallon, 110% pulp", 8, 3.99, 1, "unit"),
            new ProductByQuantity("Paper Plates", "150 pack, tree slices", 9, 5.99, 1, "unit"),
            new ProductByQuantity("Paper Towel", "12 rolls, rolled trees", 10, 8.99, 1, "unit"),
            new ProductByWeight("Bananas", "Individually wrapped fruit tube", 11, 0.15, 1, "ounce"),
            new ProductByWeight("Avacado", "Seedy guacamole", 12, 0.40, 1, "ounce"),
            new ProductByWeight("Apples", "Make Washington proud!", 13, 0.20, 1, "ounce"),
            new ProductByWeight("Pumpkin", "Pre-Jack-O-Lantern", 14, 0.10, 1, "ounce"),
            new ProductByWeight("Butternut Squash", "Not butter and not nuts", 15, 0.20, 1, "ounce"),
            new ProductByWeight("Spaghetti Squash", "Noodles without the carbs", 16, 0.25, 1, "ounce"),
            new ProductByWeight("Peaches", "The emoji is cool but these taste better", 17, 0.15, 1, "ounce"),
            new ProductByWeight("Onions", "Goggles required", 18, 0.10, 1, "ounce"),
            new ProductByWeight("Garlic", "Great for repelling vampires", 19, .40, 1, "ounce"),
            new ProductByWeight("Gasoline", "Not sure why we have this", 20, 0.75, 1, "ounce")
        };

        public static List<Product> Cart = new List<Product> { };

        public static string Receipt = "";
    }
}
