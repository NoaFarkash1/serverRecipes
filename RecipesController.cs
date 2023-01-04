using reactServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace reactServer.Controllers
{
    public class RecipesController : ApiController
    {
        public IEnumerable<Recipe> Get()
        {
            return Recipe.ReadRecipes();
           
        }


        //public IHttpActionResult Get(string name)//DOSENT WORK
        //{
        //    try
        //    {
        //        Recipe recipe2Retun = RecipeDBMOck.recipes.FirstOrDefault(recipe => recipe.Name == name);
        //        if (recipe2Retun != null)
        //        {
        //            return Ok(recipe2Retun);
        //        }
        //        return Content(HttpStatusCode.NotFound, $"recipe with name = {name} was not found in Get by name!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content(HttpStatusCode.BadRequest, ex);
        //    }
        //}

        public IHttpActionResult Post([FromBody] Recipe value)
        {
            try
            {
                value.Insert();
                return Created(new Uri(Request.RequestUri.AbsoluteUri + value.Name), value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public IHttpActionResult Put(string name, [FromBody] Recipe value)//DOSENT WORK
        //{
        //    try
        //    {
        //        Recipe recipe2Update = RecipeDBMOck.recipes.FirstOrDefault(recipe => recipe.Name == name);
        //        if (recipe2Update != null)
        //        {
        //            recipe2Update.Name = value.Name;
        //            recipe2Update.Image = value.Image;
        //            recipe2Update.CookingMethod = value.CookingMethod;
        //            recipe2Update.Time = value.Time;

        //            return Ok();
        //        }
        //        return Content(HttpStatusCode.NotFound, $"reacipe with name = {name} was not found in PUT!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        ////[Route("{name}")]
        //public IHttpActionResult Delete(string name)
        //{
        //    try
        //    {
        //        Recipe recipe2Delete = RecipeDBMOck.recipes.FirstOrDefault(recipe => recipe.Name == name);
        //        if (recipe2Delete != null)
        //        {
        //            RecipeDBMOck.recipes.Remove(recipe2Delete);
        //            return Ok();
        //        }
        //        return Content(HttpStatusCode.NotFound, $"recipe with name = {name} was not found in DELETE!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //        throw;
        //    }
        //}
    }

}
