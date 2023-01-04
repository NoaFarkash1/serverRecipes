using reactServer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using System.Xml.Linq;

namespace reactServer.Controllers
{
    public class IngredientController : ApiController
    {
        public IEnumerable<Ingredient> Get()
       {
             return Ingredient.ReadIngredients();                     
        }

        [HttpGet]
        [Route("api/ingredient/Get/{id}")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                Ingredient ingredient2Retun = Ingredient.GetIngredientByID(id);
                if (ingredient2Retun != null)
                {
                    return Ok(ingredient2Retun);
                }
                return Content(HttpStatusCode.NotFound, $"ingredient with id = {id} was not found in Get by name!!!");
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.BadRequest, ex);
            }
        }

        public IHttpActionResult Post([FromBody] Ingredient value)
        {
            try
            {
                value.Insert();
                //IngredientDBMock.ingredients.Add(value);
                return Created(new Uri(Request.RequestUri.AbsoluteUri + value.Name), value);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[HttpPut]
        //[Route("api/ingredient/Put/{name}")]
        //public IHttpActionResult Put(string name, [FromBody] Ingredient value)//DOSENT WORK
        //{
        //    try
        //    {
        //        Ingredient ing2Update = IngredientDBMock.ingredients.FirstOrDefault(ing => ing.Name == name);
        //        if (ing2Update != null)
        //        {
        //            ing2Update.Name = value.Name;
        //            ing2Update.Image = value.Image;
        //            ing2Update.Calories = value.Calories;

        //            return Ok();
        //        }
        //        return Content(HttpStatusCode.NotFound, $"ingredient with name = {name} was not found in PUT!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        //[HttpDelete]
        //[Route("api/ingredient/Delete/{name}")]

        //public IHttpActionResult Delete(string name)
        //{
        //    try
        //    {
        //        Ingredient ing2Delete = IngredientDBMock.ingredients.FirstOrDefault(ing => ing.Name == name);
        //        if (ing2Delete != null)
        //        {
        //            IngredientDBMock.ingredients.Remove(ing2Delete);
        //            return Ok();
        //        }
        //        return Content(HttpStatusCode.NotFound, $"ingredient with name = {name} was not found in DELETE!!!");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //        throw;
        //    }
        //}
    }

}
