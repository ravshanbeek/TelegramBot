using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private async Task HandleMessageTextAsync(Message message)
    {
        var command = message.Text;

        try
        {
            var task = command switch
            {
                "/start" => HandleStartCommandAsync(message),
                "uz" => HandleLanguageCommandAsync(message),
                "ru" => HandleLanguageCommandAsync(message),
                "SMM" => HandleCMMCommandAsync(message),
                "CopyWriting" => HandleCMMCommandAsync(message),
                "Design" => HandleCMMCommandAsync(message),
                "Resume" => HandleCMMCommandAsync(message),
                "Portfolio" => HandleCMMCommandAsync(message),

                "СММ" => HandleCMMCommandAsync(message),
                "Копирайтинг" => HandleCMMCommandAsync(message),
                "Дизайн" => HandleCMMCommandAsync(message),
                "Резюме" => HandleCMMCommandAsync(message),
                "портфолио" => HandleCMMCommandAsync(message),

                "Admin bilan aloqa" => HandleContactWithAdminAsync(message),
                "Zakaz berish" => HandleOrderAsync(message),

                "Связаться с администратором" => HandleContactWithAdminAsync(message),
                "Разместить заказ" => HandleOrderAsync(message),
                _ => HandleNotAvailableCommandAsync(message)
            };

            await task;
        }
        catch (Exception)
        {
            await this.client.SendTextMessageAsync(
                chatId: message.From.Id,
                text: "Failed to handle your request. Please try again");
        }
    }

    private async Task HandleOrderAsync(Message message)
    {
        var user = message.From;
        var requestOrder = new ReplyKeyboardMarkup(new[] {
            new[] { new KeyboardButton("Logotip yasash"),
                    new KeyboardButton("Foto vs Video rolik")}
        });

        requestOrder.ResizeKeyboard = true;
        requestOrder.OneTimeKeyboard = true;
        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: "Xizmatlar",
            replyMarkup: requestOrder);
    }

    private async Task HandleContactWithAdminAsync(Message message)
    {
        var user = message.From;
        Read();
        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: $"@{users[user.Id].UserName}");
    }

    private Task HandleCMMCommandAsync(Message message)
    {
        var user = message.From;
        var doc = new InputOnlineFile("BQACAgIAAxkBAAIBumRTsxpGzCCWhwmUruPb5L7xxjmgAAJQLQACX0OhSgl36cBOeFFaLwQ");

        client.SendDocumentAsync(user.Id, doc);

        return Task.CompletedTask;
    }

    private async Task HandleLanguageCommandAsync(Message message)
    {
        var user = message.From;
        Read();
        if (!users.ContainsKey(user.Id))
        {
            users.Add(user.Id, new User(user.Id, user.Username, user.FirstName, user.LastName));
        }
        int lan = message.Text.Contains("uz") ? 1 : 2;
        users[user.Id].Language = lan;

        Write();

        var requestContact = new ReplyKeyboardMarkup(new[] {
            new[] { new KeyboardButton("Share contact") { RequestContact = true} }
        });

        requestContact.ResizeKeyboard = true;
        requestContact.OneTimeKeyboard = true;

        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: lan == 1 ? "Iltimos raqamingizni biz bilan ulashing" :
                "Пожалуйста, поделитесь с нами своим номером",
            replyMarkup: requestContact);
    }
    private async Task HandleStartCommandAsync
        (Message message)
    {
        Read();
        var user = message.From;
        if (!users.ContainsKey(message.Chat.Id))
        {
            users.Add(user.Id, new User(user.Id, user.Username, user.FirstName, user.LastName));
            Write();

            return;
        }

        var requestLanguage = new ReplyKeyboardMarkup(new[] {
            new[] { new KeyboardButton("uz"),
                    new KeyboardButton("ru")}
        });

        requestLanguage.ResizeKeyboard = true;
        requestLanguage.OneTimeKeyboard = true;

        await this.client.SendTextMessageAsync(
                chatId: message.From.Id,
                text: "Tilni tanlang\nВыберите язык",
                replyMarkup: requestLanguage);
    }

    private string MapLink(double latitude, double longitude)
    {
        string link = $"https://maps.google.com/maps?q={longitude},{latitude}&ll={longitude},{latitude}&z=16";

        return link;
    }

}
