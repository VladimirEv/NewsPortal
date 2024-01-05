namespace NewsPortal.Infrastucture.Common.Response.Interfaces
{
    public interface IResponseFactory
    {
        BadResponse BadResponse<T>(T? data, string message);
        NotFoundResponse NotFoundResponse<T>(T? data, string predicate, string predicateEntity);
        UnauthorizedResponse UnauthorizedResponse(string? message = ResponseFactoryConstants.YouAreNotAuthorized);
        SuccessResponse SuccessResponse<T>(T? data, string? message = null);
    }
}
