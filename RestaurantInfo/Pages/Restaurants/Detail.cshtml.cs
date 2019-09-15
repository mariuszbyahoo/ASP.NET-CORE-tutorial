using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantInfo.Core;
using RestaurantInfo.Data;

namespace RestaurantInfo.Pages.Restaurants
{
    public class DetailModel : PageModel
    {
        public Restaurant Restaurant { get; set; }
        public IRestaurantData restaurantData { get; }
        [TempData]
        public string Message { get; set; }

        public DetailModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId);
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound"); // Obsługa błędu HTTP 301 
            }
            return Page();
        }
    }
}