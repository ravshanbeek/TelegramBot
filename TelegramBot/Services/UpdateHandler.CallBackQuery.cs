using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services;
public partial class UpdateHandler
{
    public async Task HandleCallBackQuery(Update update)
    {
        var task = update.CallbackQuery.Data switch
        {
            _ => GetRequestForChangingData(update)
        };
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
