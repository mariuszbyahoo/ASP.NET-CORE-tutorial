using Microsoft.EntityFrameworkCore;
using RestaurantInfo.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantInfo.Data
{
    public class RestaurantInfoDbContext : DbContext
    {
        public RestaurantInfoDbContext(DbContextOptions<RestaurantInfoDbContext> options)
            : base (options)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }

    }
}
