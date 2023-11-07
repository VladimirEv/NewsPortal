namespace NewsPortal.Infrastucture.Constants
{
    public static class ResponseFactoryConstants
    {
        public const string YouAreNotAuthorized = "You are not authorized";

        public static string NotFound(string predicate, string predicateEntity) =>
            $"Not found by {predicate} with {predicate} = {predicateEntity}";
    }
}
