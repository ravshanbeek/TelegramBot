using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private async Task HandleMessageLocationAsync(Message message)
    {
        Read();
        ReadResource();
        var location = message.Location;

        var user = users[message.From.Id];

        var addressDatas = await ServiceHelper
            .GetAddressAsync(location.Latitude, location.Longitude);

        if (user.AddressId == null)
        {
            user.Address = new Models.Address
            {
                Longitude = location.Longitude,
                Latitude = location.Latitude,
                AddressData = String.Join(",\n", addressDatas)
            };
        }
        {
            user.Address.Longitude = location.Longitude;
            user.Address.Latitude = location.Latitude;
            user.Address.AddressData = String.Join(",\n", addressDatas);
        }

        if (user.Order.Status != 3)
            return;
        user.Order.Status = 4;

        await client.SendLocationAsync(
            resource.Admin.Id,
            (double)user.Address.Latitude,
            (double)user.Address.Longitude);

        await client.SendTextMessageAsync(
                chatId: resource.Admin.Id,
                text: $"sizga {user.FirstName} {user.LastName}\n" +
                    $"\t@{user.UserName} {user.PhoneNumber} sizga buyurtma berdi\n" +
                    $"Xizmat: {user.Order.Service}\n" +
                    $"Instagram: {user.Order.InstagramUrl}\n" +
                    $"TZ: {user.Order.Text} \n",
                replyMarkup: new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Ha", $"Yes {user.Id}"),
                        InlineKeyboardButton.WithCallbackData("Yo'q", $"No {user.Id}")
                    }
                }));



        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: user.Language == 1 ? "Buyurtmangiz adminga yuborildi tez orada sizga murojat qilishadi"
            : "Ваш заказ отправлен администратору, они свяжутся с вами в ближайшее время");
        Write();
    }
}

