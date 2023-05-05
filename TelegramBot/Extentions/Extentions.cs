using Telegram.Bot;

namespace TelegramBot.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTelegramBotClient(
            this IServiceCollection services,
            IConfiguration configuration)
        {

            string botApiKey = configuration
                .GetSection("TelegramBot:ApiKey").Value;

            services.AddSingleton<ITelegramBotClient, TelegramBotClient>(x => new TelegramBotClient(botApiKey));

            return services;
        }

        public static IServiceCollection AddUpdateHandler(
            this IServiceCollection services)
        {
            services.AddTransient<UpdateHandler>();

            return services;
        }
    }
}