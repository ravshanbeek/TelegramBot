using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    public async Task HandleMessage(Message message)
    {
        var handler = message.Type switch
        {
            MessageType.Text => HandleMessageTextAsync(message),
            MessageType.Contact => HandleMessageContactAsync(message),
            MessageType.Video => HandleMessageVideoAsync(message),

            _ => HandleNotAvailableCommandAsync(message)
        };

        await handler;
    }
}
