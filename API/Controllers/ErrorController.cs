using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult Error(int Code)
        {
        return  new ObjectResult(new ApiResponse(Code));
        }
 
    }
}