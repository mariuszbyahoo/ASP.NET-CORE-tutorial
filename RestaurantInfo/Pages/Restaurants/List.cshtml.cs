using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestaurantInfo.Core;
using RestaurantInfo.Data;

namespace RestaurantInfo.Pages.Restaurants
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        private readonly ILogger<ListModel> logger;

        [BindProperty(SupportsGet =true)]
        public string SearchTerm { get; set; }

        public string Message { get; set; }
        public IEnumerable<Restaurant> restaurants { get; set; }

        public ListModel(IConfiguration config, 
            IRestaurantData restaurantData,
            ILogger<ListModel> logger)
        {
            this.config = config;
            this.restaurantData = restaurantData;
            this.logger = logger;
        }

        public void OnGet()
        {
            logger.LogInformation("Executing ListModel");
            Message = config["Message"];
            restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }
    }
}