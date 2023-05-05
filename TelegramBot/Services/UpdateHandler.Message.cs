using System.Text.Json;
using Telegram.Bot;
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
            _ => HandleNotAvailableCommandAsync(message)
        };

        await handler;
    }
}
