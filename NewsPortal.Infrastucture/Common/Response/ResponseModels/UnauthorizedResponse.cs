namespace NewsPortal.Infrastucture.Common.Response.ResponseModels
{
    public class UnauthorizedResponse : BaseResponse
    {
        public UnauthorizedResponse(string? message)
        {
            IsSuccess = false;
            StatusCode = HttpStatusCode.Unauthorized;
            Message = message;
        }
    }
}
