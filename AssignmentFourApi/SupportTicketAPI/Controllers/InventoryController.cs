using Microsoft.AspNetCore.Mvc;
using ShoppingCartApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Controllers
{
    [ApiController]
    [Route("InventorySearch")]
    public class InventoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Search")]
        public ActionResult<List<Product>> SearchApi([FromBody] string searchTerm)
        {
            // If the search box is empty return the full inventory
            if(searchTerm == "" || searchTerm == null)
            {
                return Ok(DataContext.Inventory);
            }

            // Otherwise get all items in the inventory that match the searchTerm
            var newList = from product in DataContext.Inventory
                          where product.Name.ToLower().Contains(searchTerm.ToLower()) || product.Description.ToLower().Contains(searchTerm.ToLower())
                          select product;

            return Ok(newList.ToList());
        }
    }
}
