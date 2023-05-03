using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message=null) 
        {
            this.StatusCode = statusCode;
            this.Message = message??GetDefaultMessageForStatusCode(statusCode);
   
        }



        public int? StatusCode { get; set; }

        public string Message { get; set; }


       private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
                {
                    400=>"A Abd Request, you Have Made",
                    401=>"Autherized, you are not",
                    404=>"Resource found,it was not",
                    500=>"error are the path to the dark side,error lead to anger,anger leads to hate, Hate leads to career change ",
                    _ =>null
                };
        }
    }
}