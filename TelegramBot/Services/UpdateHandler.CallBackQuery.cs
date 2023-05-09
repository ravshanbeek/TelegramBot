using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;
public partial class UpdateHandler
{
    public async Task HandleCallBackQuery(Update update)
    {
        var data = update.CallbackQuery.Data.Split(' ').First();

        var task = data switch
        {
            "SMM" => ShareCMM(update),
            _ => ShareCMM(update)
        };
    }
    private async Task ShareCMM(Update update)
    {
        var user = update.CallbackQuery.From;
        var doc = new InputOnlineFile("BQACAgIAAxkBAAIBumRTsxpGzCCWhwmUruPb5L7xxjmgAAJQLQACX0OhSgl36cBOeFFaLwQ");

        await client.SendDocumentAsync(user.Id, doc);

    }
    private ReplyKeyboardMarkup GenerateMainMenuUz()
    {
        var buttons = new ReplyKeyboardMarkup(new[] {
            new[]
            {
                new KeyboardButton("Zakaz berish"),
                new KeyboardButton("Admin bilan aloqa")
            },

            new[]
            {
                new KeyboardButton("Прайс лист")
            },
            new[]
            {
                new KeyboardButton("Mobilografiya"),
                new KeyboardButton("Grafik dizayn")
            },
            new[]
            {
                new KeyboardButton("Video Montaj"),
            },

            new[]
            {
                new KeyboardButton("Kopywriting"),
            }
        });

        buttons.ResizeKeyboard = true;

        return buttons;
    }

    private ReplyKeyboardMarkup GenerateMainMenuRu()
    {
        var buttons = new ReplyKeyboardMarkup(new[] {
            new[]{
                new KeyboardButton("Заказать"),
                new KeyboardButton("Для связи с нами")
            },
            new[]{
                new KeyboardButton("Прайс лист")
            },
            new[]{
                new KeyboardButton("Мобилография"),
                new KeyboardButton("Графический дизайнер")
            },
            new[]{
                new KeyboardButton("Видео монтаж")
            },
            new[]{
                new KeyboardButton("Копирайтинг")
            }
        });

        buttons.ResizeKeyboard = true;

        return buttons;
    }
}
