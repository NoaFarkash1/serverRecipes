using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reactServer.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int Calories { get; set; }


        public int Insert()// insert ingredient to DB
        {
            DBServices dbs = new DBServices();
            return dbs.InsertIngredient(this);
        }


        public static List<Ingredient> ReadIngredients()
        {

            DBServices dbs = new DBServices();
            return dbs.GetIngredients();
        }

        public static Ingredient GetIngredientByID(int id)
        {

            DBServices dbs = new DBServices();
            return dbs.GetIngredient(id);
        }

        //public override string ToString()
        //{
        //    return $"{Id}, {Name}, {Grade}";
        //}

        //public override int GetHashCode()
        //{
        //    return Id;
        //}

        //public override bool Equals(object obj)
        //{
        //    return GetHashCode() == obj.GetHashCode();
        //}
    }
}