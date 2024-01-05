namespace NewsPortal.Infrastucture.Common.Response.ResponseModels
{
    public class SuccessResponse : BaseResponse
    {
        public SuccessResponse(object? data, string? message = null)
        {
            IsSuccess = true;
            StatusCode = HttpStatusCode.OK;
            Data = data;
            Message = message;
        }
    }
}
