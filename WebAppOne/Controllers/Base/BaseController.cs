namespace NewsPortal.Web.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        protected IActionResult GetResponse(BaseResponse response)
        {
            return response.StatusCode switch
            {
                HttpStatusCode.OK => Ok(response),
                HttpStatusCode.NotFound => NotFound(response),
                HttpStatusCode.Unauthorized => BadRequest(response),
                HttpStatusCode.BadRequest => BadRequest(response),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
