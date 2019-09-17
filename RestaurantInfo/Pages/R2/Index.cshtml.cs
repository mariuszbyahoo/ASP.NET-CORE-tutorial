using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RestaurantInfo.Core;
using RestaurantInfo.Data;

namespace RestaurantInfo.Pages.R2
{
    public class IndexModel : PageModel
    {
        private readonly RestaurantInfo.Data.RestaurantInfoDbContext _context;

        public IndexModel(RestaurantInfo.Data.RestaurantInfoDbContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurant { get;set; }

        public async Task OnGetAsync()
        {
            Restaurant = await _context.Restaurants.ToListAsync();
        }
    }
}
