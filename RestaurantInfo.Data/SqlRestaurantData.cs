﻿using System.Collections.Generic;
using RestaurantInfo.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace RestaurantInfo.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly RestaurantInfoDbContext db;

        public SqlRestaurantData (RestaurantInfoDbContext cd)
        {
            this.db = db;
        }
        public Restaurant Add(Restaurant newRestaurant)
        {
/** EntityFrameworkCore wie że jeśli chcę dodać obiekt klasy Restaurant do bazy
* danych, to wtedy także doda ten obiekt do kolekcji typu "DbSet" którą mamy w 
* klasie RestaurantInfoDbContext.cs */
            db.Add(newRestaurant);
            return newRestaurant;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
            {
                db.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant);
            entity.State = EntityState.Modified;
            return updatedRestaurant;
        }
    }
}