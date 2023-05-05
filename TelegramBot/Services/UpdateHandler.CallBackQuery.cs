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
                new KeyboardButton("SMM"),
                new KeyboardButton("CopyWriting"),
                new KeyboardButton("Design")
            },
            new[]
            {
                new KeyboardButton("Resume"),
                new KeyboardButton("Portfolio")
            },
            new[]
            {
                new KeyboardButton("Admin bilan aloqa"),
                new KeyboardButton("Zakaz berish")
            }
        });

        buttons.ResizeKeyboard = true;
        buttons.OneTimeKeyboard = true;

        return buttons;
    }

    private ReplyKeyboardMarkup GenerateMainMenuRu()
    {
        var buttons = new ReplyKeyboardMarkup(new[] {
            new[]{ 
                new KeyboardButton("CMM"),
                new KeyboardButton("Kопирайтинг"),
                new KeyboardButton("Дизайн")
            },
            new[]{
                new KeyboardButton("Резюме"),
                new KeyboardButton("портфолио")
            },
            new[]{
                new KeyboardButton("Связаться с администратором"),
                new KeyboardButton("Разместить заказ")
            }
        });

        buttons.ResizeKeyboard = true;
        buttons.OneTimeKeyboard = true;

        return buttons;
    }
}
