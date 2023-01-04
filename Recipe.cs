using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace reactServer.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string CookingMethod { get; set; }
        public float  Time { get; set; }
        public List<int> Ingredients { get; set; } //list of ingredients


        public string Insert()// insert recipe to DB
        {
            DBServices dbs = new DBServices();
            return dbs.InsertRecipe(this);
        }

        public static List<Recipe> ReadRecipes()
        {

            DBServices dbs = new DBServices();
            return dbs.GetRecipes();
        }

    }
}