using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController:BaseApiController
    {
        public StoreContext _Context { get; }
        public BuggyController(StoreContext context)
        {
            this._Context = context;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing=_Context.Products.Find(43);
            if(thing==null)
            {
            return NotFound(new ApiResponse(404));
            } 
            return Ok();
        }




        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
              var thing=_Context.Products.Find(43);

              var thingToReturn =thing.ToString();

            return Ok();
        }





        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
        return BadRequest(new ApiResponse(400));
        }



        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
           return BadRequest();
        }




    }
}