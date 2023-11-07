namespace NewsPortal.Infrastucture.Common.Response.ResponseModels
{
    public class NotFoundResponse : BaseResponse
    {
        public NotFoundResponse(object? data, string predicate, string predicateEntity)
        {
            IsSuccess = false;
            StatusCode = HttpStatusCode.NotFound;
            Data = data;
            Message = ResponseFactoryConstants.NotFound(predicate, predicateEntity);
        }
    }
}
