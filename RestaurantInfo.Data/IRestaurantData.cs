using RestaurantInfo.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantInfo.Data
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetRestaurantsByName(string name);
        Restaurant GetById(int id);
        Restaurant Update(Restaurant updatedRestaurant);
        Restaurant Add(Restaurant restaurant);
        int Commit();
    }

    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id = 1, Name = "Scott's Pizza",
                    Location = "Maryland", Cuisine = CuisineType.Italian},
                new Restaurant {Id = 2, Name = "Cinamon Club",
                    Location = "London", Cuisine = CuisineType.Indian},
                new Restaurant {Id = 3, Name = "La Costa",
                    Location = "California", Cuisine = CuisineType.Mexican}
            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Location = updatedRestaurant.Location;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }
            return restaurant;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            // 56 only 4 test&dev
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public int Commit()// Ta metoda ma znaczenie tylko gdy używamy 
            //Prawdziwego źródła danych, np. DB
        {
            return 0; 
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name) 
                   || r.Location.StartsWith(name) 
                   orderby r.Name
                   select r;
        }
    }
}
