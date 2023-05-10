using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private Task HandleMessageVideoAsync(Message message)
    {
        ReadResource();

        if(message.Chat.Id != resource.Admin.Id)
            return Task.CompletedTask;

        switch(resource.DataName)
        {
            case "MobileGrafic": resource.MobileGrafic = message.Video.FileId; break;
            case "GraficDisign": resource.GraficDisign = message.Video.FileId; break;
            case "VideoMontage": resource.VideoMontage = message.Video.FileId; break;
        }

        resource.DataName = null;

        WriteResource();
        client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Videoni o'nating");

        return Task.CompletedTask;
    }
}
