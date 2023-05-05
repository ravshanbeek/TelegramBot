using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private async Task HandleMessageContactAsync(Message message)
    {
        Read();
        users[message.Chat.Id].PhoneNumber= message.Contact.PhoneNumber;
        Write();
        var user = users[message.Chat.Id];
        client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: user.Language == 1 ? "Menyu" : "Меню",
            replyMarkup: user.Language == 1 ? GenerateMainMenuUz() : GenerateMainMenuRu());
    }
}