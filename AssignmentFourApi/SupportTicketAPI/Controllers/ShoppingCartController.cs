using Microsoft.AspNetCore.Mvc;
using ShoppingCartApplication.Models;
using ShoppingCartAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("Inventory")]
    public class ShoppingCartController : ControllerBase
    {
        // Returns the Inventory
        [HttpGet("GetInventory")]
        public ActionResult<List<Product>> Get()
        {
            return Ok(DataContext.Inventory);
        }

        // Returns the Cart
        [HttpGet("GetCart")]
        public ActionResult<List<Product>> GetCart()
        {
            return Ok(DataContext.Cart);
        }

        // Returns the Reciept
        [HttpGet("GetReceipt")]
        public ActionResult<List<Product>> GetReceipt()
        {
            return Ok(DataContext.Receipt);
        }

        // Adds a inventory item to the cart
        [HttpPost("add")]
        public ActionResult<List<Product>> add([FromBody] Product cartItem)
        {

            int amount = 1;
            if (cartItem == null)
            {
                return Ok(DataContext.Cart);
            }
            // Find the matching product name and adds to the number of units or ounces wanted
            for (int i = 0; i < DataContext.Cart.Count; i++)
            {
                if (cartItem.Name == DataContext.Cart[i].Name)
                {

                    if (DataContext.Cart[i].TypeOfProduct == "unit" && amount % Convert.ToInt32(amount) == 0)
                    {
                        DataContext.Cart[i] = new ProductByQuantity(cartItem.Name, cartItem.Description, DataContext.Cart[i].Id, cartItem.getItemPrice(), Convert.ToInt32(DataContext.Cart[i].getAmount()) + Convert.ToInt32(amount), cartItem.TypeOfProduct);
                    }
                    else if (DataContext.Cart[i].TypeOfProduct == "ounce")
                    {
                        DataContext.Cart[i] = new ProductByWeight(cartItem.Name, cartItem.Description, DataContext.Cart[i].Id, cartItem.getItemPrice(), DataContext.Cart[i].getAmount() + amount, cartItem.TypeOfProduct);
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount");
                    }

                    return Ok(DataContext.Cart);
                }
            }

            // Creates a new product if there are no matching products in the users cart
            if (cartItem.TypeOfProduct == "unit" && amount % Convert.ToInt32(amount) == 0)
            {
                DataContext.Cart.Add(new ProductByQuantity(cartItem.Name, cartItem.Description, DataContext.Cart.Count + 1, cartItem.getItemPrice(), Convert.ToInt32(amount), cartItem.TypeOfProduct));
            }
            else if (cartItem.TypeOfProduct == "ounce")
            {
                DataContext.Cart.Add(new ProductByWeight(cartItem.Name, cartItem.Description, DataContext.Cart.Count + 1, cartItem.getItemPrice(), amount, cartItem.TypeOfProduct));
            }
            else
            {
                Console.WriteLine("Invalid amount");
            }

            return Ok(DataContext.Cart);

        }

        // Removes an item from the cart
        [HttpPost("remove")]
        public ActionResult<List<Product>> Remove([FromBody] Product cartItem)
        {
            int amount = 1;

            if (cartItem == null)
            {
                return Ok(DataContext.Cart);
            }

            for (int i = 0; i < DataContext.Cart.Count; i++)
            {
                // Find the matching product name and removes the number of units or ounces given
                if (cartItem.Name == DataContext.Cart[i].Name)
                {
                    if (DataContext.Cart[i].getAmount() - amount >= 0)
                    {
                        if (DataContext.Cart[i].TypeOfProduct == "unit" && amount % Convert.ToInt32(amount) == 0)
                        {

                            DataContext.Cart[i] = new ProductByQuantity(cartItem.Name, cartItem.Description, DataContext.Cart[i].Id, cartItem.getItemPrice(), Convert.ToInt32(DataContext.Cart[i].getAmount()) - Convert.ToInt32(amount), cartItem.TypeOfProduct);
                        }
                        else if (DataContext.Cart[i].TypeOfProduct == "ounce")
                        {
                            DataContext.Cart[i] = new ProductByWeight(cartItem.Name, cartItem.Description, DataContext.Cart[i].Id, cartItem.getItemPrice(), DataContext.Cart[i].getAmount() - amount, cartItem.TypeOfProduct);
                        }
                        else
                        {
                            Console.WriteLine("Invalid amount");
                        }

                        // If there are no more of an item remove it
                        if (DataContext.Cart[i].getAmount() == 0)
                        {
                            DataContext.Cart.RemoveAt(i);
                        }

                        return Ok(DataContext.Cart);
                    }
                    else
                    {
                        Console.WriteLine("You can not have less than 0 products.");
                    }
                }

            }
            return Ok(DataContext.Cart);
        }

        // Clears the cart
        [HttpPost("clearcart")]
        public ActionResult<Product> ClearCart([FromBody] List<Product> cartToRemove)
        {
            DataContext.Cart.Clear();
            return null;
        }

        // Prints the reciept to the API and clears the cart
        [HttpPost("checkout")]
        public ActionResult<Product> CheckOut([FromBody] List<Product> cartToRemove)
        {
            DataContext.Receipt = "";
            double subtotal = DataContext.Cart.Sum(i => i.Price);
            double salesTax = DataContext.Cart.Sum(i => i.Price) * 0.07;

            DataContext.Receipt += "Receipt\n";
            DataContext.Receipt += "--------------------------------------------------\n";
            DataContext.Receipt += string.Format("{0, -15} {1, -20} {2, -15}", "Amount", "Item Name", "Price of Item\n");
            DataContext.Receipt += string.Format("{0, -15} {1, -20} {2, -15}", "------", "---------", "-------------\n");

            for (int i = 0; i < DataContext.Cart.Count; i++)
            {
                DataContext.Receipt += string.Format("{0, -15} {1, -20} {2, -15}", DataContext.Cart[i].getAmount() + " " + DataContext.Cart[i].TypeOfProduct + "s", DataContext.Cart[i].Name, DataContext.Cart[i].Price.ToString("C"));
                DataContext.Receipt += "\n";
            }

            DataContext.Receipt += string.Format("--------------------------------------------------\n");
            DataContext.Receipt += string.Format("{0, -36} {1,-15}", "Subtotal", subtotal.ToString("C"));
            DataContext.Receipt += "\n";
            DataContext.Receipt += string.Format("--------------------------------------------------\n");
            DataContext.Receipt += string.Format("{0, -36} {1,-15}", "Sales Tax", salesTax.ToString("C"));
            DataContext.Receipt += "\n";
            DataContext.Receipt += string.Format("--------------------------------------------------\n");
            DataContext.Receipt += string.Format("{0, -36} {1,-15}", "Total", (salesTax + subtotal).ToString("C"));
            DataContext.Receipt += "\n";

            DataContext.Cart.Clear();


            return null;
        }
    }
}
