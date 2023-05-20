using Microsoft.AspNetCore.Http.HttpResults;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Services;
public partial class UpdateHandler
{
    public async Task HandleCallBackQuery(Update update)
    {
        var message = update.CallbackQuery.Data.Split(' ');
        var task = message[0] switch
        {
            "Yes" => RequestAccepted(update, message[1]),
            "No" => RequestNotAccepted(update, message[1]),

            _ => GetRequestForChangingData(update)
        };

        await task;
    }

    private async Task RequestAccepted(Update update, string chatId)
    {
        Read();

        var user = users[long.Parse(chatId)];

        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: user.Language == 1 ? "№1 buyurtmangiz qabul qilindi✅ Tez orada siz bilan bog'lanamiz"
            : "ваш заказ #1 принят✅ Мы скоро с вами свяжемся");

        user.Order.Status = 4;

        if(user.Order.Service == "MobilGrafiya")
        {
            await client.SendTextMessageAsync(
                chatId: user.Id,
                text: user.Language == 1 ? "Fotosessiya uchun sizga maqul kunni kiriting"
                : "Укажите удобный для вас день фотосессии");
        }

        Write();
    }

    private async Task RequestNotAccepted(Update update, string chatId)
    {
        Read();

        var user = users[long.Parse(chatId)];

        await client.SendTextMessageAsync(
            chatId: long.Parse(chatId),
            text: user.Language == 1 ? "Sizning so'rovingizni admin maqul topmadi "
            : "Ваш запрос не одобрен администратором");

        user.Order = null;

        Write();
    }

    private async Task GetRequestForChangingData(Update update)
    {
        ReadResource();

        resource.DataName = update.CallbackQuery.Data;
        WriteResource();

        await client.SendTextMessageAsync(
            chatId: resource.Admin.Id,
            text: "Ma'lumotni jo'nating");
    }
}
