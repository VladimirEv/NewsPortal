namespace NewsPortal.Infrastucture.Common.Response.ResponseModels
{
    public class BadResponse : BaseResponse
    {
        public BadResponse(object? data, string message)
        {
            IsSuccess = false;
            StatusCode = HttpStatusCode.BadRequest;
            Data = data;
            Message = message;
        }
    }
}
