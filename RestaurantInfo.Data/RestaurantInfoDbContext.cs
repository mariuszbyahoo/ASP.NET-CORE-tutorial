using Microsoft.EntityFrameworkCore;
using RestaurantInfo.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantInfo.Data
{
    class RestaurantInfoDbContext : DbContext
    {
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}
