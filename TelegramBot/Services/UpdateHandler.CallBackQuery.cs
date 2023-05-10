using Telegram.Bot.Types;

namespace TelegramBot.Services;
public partial class UpdateHandler
{
    public async Task HandleCallBackQuery(Update update)
    {
        var task = update.CallbackQuery.Data switch
        {
            _ => ShareCMM(update)
        };
    }
    private async Task ShareCMM(Update update)
    {
        ReadResource();
        resource.DataName = update.CallbackQuery.Data;
        WriteResource();
    }
}
