using Telegram.Bot.Types;
using Telegram.Bot;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private async Task HandleMessagePhotoAsync(Message message)
    {
        ReadResource();
        if (message.Chat.Id != resource.Admin.Id)
            return;

        if (resource.DataName != "GraficDisign")
            return;

        resource.GraficDisign = message.Photo[0].FileId;

        resource.DataName = null;

        WriteResource();

        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Amaliyot muvofaqqiyatli tugatildi");
    }
}
