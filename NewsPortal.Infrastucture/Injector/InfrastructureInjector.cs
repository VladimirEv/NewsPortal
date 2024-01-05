using System.Net;

namespace NewsPortal.Infrastucture.Injector
{
    public static class InfrastructureInjector
    {
        /// <summary>
        /// 1. services.AddSingleton<IResponseFactory, ResponseFactory>();: 
        /// Здесь регистрируется сервис ResponseFactory как реализация интерфейса IResponseFactory. 
        /// Это означает, что при запросе сервиса типа IResponseFactory, контейнер внедрения 
        /// зависимостей вернет экземпляр класса ResponseFactory.
        ///2. services.AddStackExchangeRedisCache(options => {...});: Эта строка кода добавляет кэширование на основе Redis 
        ///в сервисы.Он использует пакет StackExchange.Redis.Extensions.Microsoft.DependencyInjection для интеграции 
        ///с ASP.NET Core. В блоке options => { ...}
        ///происходит конфигурация параметров для подключения к Redis.
        ///options.ConfigurationOptions = new ConfigurationOptions { ... }: Устанавливает параметры конфигурации для 
        ///подключения к серверу Redis, такие как AbortOnConnectFail и EndPoints.
        ///options.InstanceName = ConfigurationConstants.RedisInstanceName;: Устанавливает имя экземпляра Redis для использования.
        /// </summary>

        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IResponseFactory, ResponseFactory>(); 
            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = new ConfigurationOptions
                {
                    AbortOnConnectFail = false,
                    EndPoints = { configuration.GetConnectionString(ConfigurationConstants.RedisConnection) ?? string.Empty }
                };
                options.InstanceName = ConfigurationConstants.RedisInstanceName;
            });
        }
    }
}
