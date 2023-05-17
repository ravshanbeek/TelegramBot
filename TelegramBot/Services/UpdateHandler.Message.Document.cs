using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private async Task HandleMessageDocumentAsync(Message message)
    {
        ReadResource();
        if (message.Chat.Id != resource.Admin.Id)
            return;

        if (resource.DataName != "Category")
            return;

        resource.Category = message.Document.FileId;

        resource.DataName = null;

        WriteResource();

        await client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Amaliyot muvofaqqiyatli tugatildi");
    }
}
