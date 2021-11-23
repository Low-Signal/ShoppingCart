using ShoppingCartApplication.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartApplication
{
    public class ProductJsonConverter : JsonCreationConverter<Product>
    {
        protected override Product Create(Type objectType, JObject jObject)
        {
            if (jObject == null) throw new ArgumentNullException("jObject");

            if (jObject["UnitPrice"] != null || jObject["unitPrice"] != null)
            {
                return new ProductByQuantity();
            }
            else if (jObject["PricePerOunce"] != null || jObject["pricePerOunce"] != null)
            {
                return new ProductByWeight();
            }
            else
            {
                return new Product();
            }
        }
    }
}
