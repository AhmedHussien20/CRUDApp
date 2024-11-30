using Microsoft.AspNetCore.Mvc;
namespace CRUDApp.BaseResponse
{
  
    public abstract class BaseResponseHandler : ControllerBase
    {
        protected IActionResult CreateSuccessResponse<T>(T data, string message = "Operation successful")
        {
            return Ok(new
            {
                success = true,
                message,
                data
            });
        }

        protected IActionResult CreateErrorResponse(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, new
            {
                success = false,
                message,
                data = (object)null
            });
        }

        protected IActionResult CreateNotFoundResponse(string message = "Resource not found")
        {
            return NotFound(new
            {
                success = false,
                message,
                data = (object)null
            });
        }
    }


}
