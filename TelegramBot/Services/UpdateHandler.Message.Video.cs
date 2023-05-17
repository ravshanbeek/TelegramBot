using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private async Task HandleMessageVideoAsync(Message message)
    {
        ReadResource();

        if(message.Chat.Id != resource.Admin.Id)
            return;

        switch(resource.DataName)
        {
            case "MobileGrafic": resource.MobileGrafic = message.Video.FileId; break;
            case "VideoMontage": resource.VideoMontage = message.Video.FileId; break;
        }

        resource.DataName = null;

        WriteResource();

        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Amaliyot muvofaqqiyatli tugatildi");
    }
}
