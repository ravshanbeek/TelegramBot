using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private Task HandleMessageDocumentAsync(Message message)
    {
        ReadResource();
        if (message.Chat.Id != resource.Admin.Id)
            return Task.CompletedTask;

        if (resource.DataName != "Category")
            return Task.CompletedTask;

        resource.Category = message.Document.FileId;

        resource.DataName = null;

        WriteResource();

        client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Amaliyot muvofaqqiyatli tugatildi");


        return Task.CompletedTask;
    }
}
