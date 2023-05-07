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
                "Mobilografiya" => MobileGrafic(message),
                "Grafik dizayn" => GraphicDisign(message),
                "Video Montaj" => MontageVideo(message),
                "Kopywriting" => CopyWriting(message),
                "Admin bilan aloqa" => HandleContactWithAdminAsync(message),
                "Zakaz berish" => HandleOrderAsync(message),


                "Прайс лист" => CategoryOfPrice(message),
                "Мобилография" => MobileGrafic(message),
                "Графический дизайнер" => GraphicDisign(message),
                "Видео монтаж" => MontageVideo(message),
                "Копирайтинг" => CopyWriting(message),
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

    private Task CopyWriting(Message message)
    {
        var user = message.From;
        var file = new InputOnlineFile("BAACAgIAAxkBAAIDbWRXRTqbScNMqN69IN-E3YargnjVAAIiKwACnbsISnnquB8zs6x_LwQ");

        client.SendDocumentAsync(user.Id, file);

        return Task.CompletedTask;
    }

    private Task MontageVideo(Message message)
    {
        var user = message.From;
        var file = new InputOnlineFile("BAACAgIAAxkBAAIDbWRXRTqbScNMqN69IN-E3YargnjVAAIiKwACnbsISnnquB8zs6x_LwQ");

        client.SendVideoAsync(user.Id, file);

        return Task.CompletedTask;
    }

    private Task GraphicDisign(Message message)
    {
        var user = message.From;
        var file = new InputOnlineFile("AgACAgIAAxkBAAIDY2RXRCrmIB08ZGckzxRcYoivHuj9AALJxjEbnbsIStIr_e9NqfnaAQADAgADeQADLwQ");

        client.SendPhotoAsync(user.Id, file);

        return Task.CompletedTask;
    }

    private Task MobileGrafic(Message message)
    {
        var user = message.From;
        var file = new InputOnlineFile("BAACAgIAAxkBAAIDXWRXQtUM2TUrw_PVzr00kWOdLSm1AAIKKwACnbsISrXmoIgXiHhULwQ");

        client.SendVideoAsync(user.Id, file);

        return Task.CompletedTask;
    }

    private async Task HandleOrderAsync(Message message)
    {
        var user = message.From;
        var requestOrderuz = new ReplyKeyboardMarkup(new[] {
            new[]
            { 
                new KeyboardButton("SMM"),
                new KeyboardButton("Grafik Dizayn"),
                new KeyboardButton("MobilGrafiya")},
            new[]
            {
                new KeyboardButton("Orqaga")
            }
        });

        var requestOrderru = new ReplyKeyboardMarkup(new[] {
            new[]
            {
                new KeyboardButton("СММ"),
                new KeyboardButton("МобилоГрафия"),
                new KeyboardButton("Графический Дизайнер")},
            new[]
            {
                new KeyboardButton("Назад")
            }
        });

        requestOrderuz.ResizeKeyboard = true;
        requestOrderuz.OneTimeKeyboard = true;
        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: "Xizmatlar",
            replyMarkup: requestOrderuz);
    }

    private async Task HandleContactWithAdminAsync(Message message)
    {
        var user = message.From;
        Read();
        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: $"@{users[user.Id].UserName}");
    }

    private Task CategoryOfPrice(Message message)
    {
        var user = message.From;
        var doc = new InputOnlineFile("BQACAgIAAxkBAAIDQWRXQLaRuiPVcWHuxV7s-RUNx4C7AAL7JAACnPDAShfMlesofOtTLwQ");

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
