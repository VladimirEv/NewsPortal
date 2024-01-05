namespace NewsPortal.Infrastucture.Common.Response
{
    public class ResponseFactory : IResponseFactory
    {
        public BadResponse BadResponse<T>(T? data, string message)
        {
            return new BadResponse(data, message);
        }

        public NotFoundResponse NotFoundResponse<T>(T? data, string predicate, string predicateEntity)
        {
            return new NotFoundResponse(data, predicate, predicateEntity);
        }

        public UnauthorizedResponse UnauthorizedResponse(string? message = ResponseFactoryConstants.YouAreNotAuthorized)
        {
            return new UnauthorizedResponse(message);
        }

        public SuccessResponse SuccessResponse<T>(T? data, string? message = null)
        {
            return new SuccessResponse(data, message);
        }
    }
}
