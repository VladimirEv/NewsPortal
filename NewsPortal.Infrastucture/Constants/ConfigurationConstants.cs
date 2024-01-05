namespace NewsPortal.Infrastucture.Constants
{
    public static class ConfigurationConstants
    {
        public const string JwtExpirationInMinutes = "JWT:ExpirationInMinutes";
        public const string JwtSecret = "JWT:Secret";
        public const string JwtValidAudience = "JWT:ValidAudience";
        public const string JwtValidIssuer = "JWT:ValidIssuer";
        public const string RedisConnection = "RedisConnection";
        public const string RedisInstanceName = "Auth";
        public const string PostgresConnection = "PostgresConnection";
    }
}
