using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Data;
using System.Text;
using reactServer.Models;

namespace reactServer.Models
{
    public class DBServices
    {
        public SqlDataAdapter da;
        public DataTable dt;

        public SqlConnection connect(string conString)
        {

            // read the connection string from the configuration file
            string cStr = WebConfigurationManager.ConnectionStrings[conString].ConnectionString;
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommand(String CommandSTR, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = CommandSTR;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.Text; // the type of the command, can also be stored procedure

            return cmd;
        }

        public int InsertIngredient(Ingredient ingredient)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("Recipe"); // create a connection to the database using the connection String defined in the web config file
                string pStr = "INSERT INTO IngredientsA (Name, Image, Calories) values ('" + ingredient.Name + "','" + ingredient.Image + "','" + ingredient.Calories + "')";
                cmd = CreateCommand(pStr, con);
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }


        public List<Ingredient> GetIngredients()
        {
            SqlConnection con = null;
            List<Ingredient> ingredients = new List<Ingredient>();
            try
            {
                con = connect("Recipe"); // create a connection to the database using the connection String defined in the web config file

                string selectSTR = "select * from IngredientsA";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Ingredient ingredient = new Ingredient();
                    ingredient.Id = Convert.ToInt32(dr["Id"]);//
                    ingredient.Name = dr["Name"].ToString();
                    ingredient.Image = dr["Image"].ToString();
                    ingredient.Calories = Convert.ToInt32(dr["Calories"]);//

                    ingredients.Add(ingredient);
                }

                return ingredients;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public Ingredient GetIngredient(int id)
        {
            SqlConnection con = null;
            Ingredient ingredient = new Ingredient();
            try
            {
                con = connect("Recipe"); // create a connection to the database using the connection String defined in the web config file

                string selectSTR = "select * from IngredientsA where Id='" + id + "'";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {
                    ingredient.Id = Convert.ToInt32(dr["Id"]);//
                    ingredient.Name = dr["Name"].ToString();
                    ingredient.Image = dr["Image"].ToString();
                    ingredient.Calories = Convert.ToInt32(dr["Calories"]);//
                }

                return ingredient;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }

        public string InsertRecipe(Recipe recipe)
        {
            SqlConnection con = null;
            SqlCommand cmd;
            try
            {
                con = connect("Recipe"); // create a connection to the database using the connection String defined in the web config file
                string pStr = "INSERT INTO RecipesA (Name, Image, cookingMethod,Time) values ('" + recipe.Name + "','" + recipe.Image + "','" + recipe.CookingMethod + "','" + recipe.Time + "');  SELECT SCOPE_IDENTITY()";
                cmd = CreateCommand(pStr, con);
                string modified = cmd.ExecuteScalar().ToString(); // returns last modified row
                foreach (var ingredient in recipe.Ingredients)
                {
                    pStr = "INSERT INTO ingredientsInRecipesA (ingredientId,recipeId) values ('" + ingredient + "','" + modified + "');";
                    cmd = CreateCommand(pStr, con);
                    cmd.ExecuteNonQuery();
                }
                return modified;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
        public List<Recipe> GetRecipes()
        {
            SqlConnection con = null;
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                con = connect("Recipe"); // create a connection to the database using the connection String defined in the web config file

                string selectSTR = "select * from RecipesA";
                SqlCommand cmd = new SqlCommand(selectSTR, con);

                // get a reader
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                while (dr.Read())
                {   // Read till the end of the data into a row
                    Recipe recipe = new Recipe();
                    recipe.Id = Convert.ToInt32(dr["Id"]);
                    recipe.Name = (string)dr["Name"];
                    recipe.Image = (string)dr["Image"];
                    recipe.CookingMethod = (string)dr["CookingMethod"];
                    recipe.Time = (float)Convert.ToDouble(dr["Time"]);
                    recipe.Ingredients = new List<int>();
                    recipes.Add(recipe);
                }

                foreach (var recipe in recipes)
                {
                    con.Close();
                    con = connect("Recipe");
                    selectSTR = "select * from ingredientsInRecipesA where recipeId='" + recipe.Id + "'";
                    SqlCommand cmd2 = new SqlCommand(selectSTR, con);
                    // get a reader
                    dr = cmd2.ExecuteReader(CommandBehavior.CloseConnection); // CommandBehavior.CloseConnection: the connection will be closed after reading has reached the end

                    while (dr.Read())
                    {   // Read till the end of the data into a row
                        recipe.Ingredients.Add(Convert.ToInt32(dr["ingredientId"]));
                    }
                }

                return recipes;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
        }
 }
    //    public void InsertRecipeAndIngredient(Recipe recipe, int recipeId)
    //    {
    //        SqlConnection con = null;
    //        SqlCommand cmd;
    //        try
    //        {
    //            con = connect("Recipe"); // create a connection to the database using the connection String defined in the web config file
    //            foreach (var ingredient in recipe.Ingredients)
    //            {
    //                string pStr = "INSERT INTO IngredientInRecipe (IngredientId, RecipeId) values ('" + ingredient + "','" + recipeId + "')";
    //                cmd = CreateCommand(pStr, con);
    //                int numEffected = cmd.ExecuteNonQuery(); // execute the command
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }
    //        finally
    //        {
    //            if (con != null)
    //            {
    //                con.Close();
    //            }
    //        }
    //    }
    //}
}