using Microsoft.AspNetCore.Http.HttpResults;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services;
public partial class UpdateHandler
{
    public async Task HandleCallBackQuery(Update update)
    {
        var message = update.CallbackQuery.Data.Split(' ');
        var task = message[0] switch
        {
            "Ha" => RequestAccepted(update, message[1]),

            _ => GetRequestForChangingData(update)
        };

        await task;
    }

    private async Task RequestAccepted(Update update, string chatId)
    {

    }

    private async Task GetRequestForChangingData(Update update)
    {
        ReadResource();

        resource.DataName = update.CallbackQuery.Data;
        WriteResource();

        await client.SendTextMessageAsync(
            chatId: resource.Admin.Id,
            text: "Ma'lumotni jo'nating");
    }
}
