using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private async Task HandleMessageLocationAsync(Message message)
    {
        Read();
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

        Write();

        if (user.Order.Status != 3)
            return;

        await client.SendTextMessageAsync(
                chatId: resource.Admin.Id,
                text: $"sizga {user.FirstName} {user.LastName}\n" +
                    $"\t@{user.UserName} {user.PhoneNumber} sizga buyurtma berdi\n" +
                    $"Xizmat: {user.Order.Service}\n" +
                    $"Instagram: {user.Order.InstagramUrl}\n" +
                    $"TZ: {user.Order.Text} \n" +
                    $"address link: " +
                    $"https://maps.google.com/maps?q={user.Address.Latitude},{user.Address.Longitude}&ll={user.Address.Latitude},{user.Address.Longitude}&z=16",
                replyMarkup: new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData("Ha", $"Ha {user.Id}"),
                        InlineKeyboardButton.WithCallbackData("Yo'q", $"Yo'q {user.Id}")
                    }
                }));

        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: user.Language == 1 ? "Buyurtmangiz adminga yuborildi tez orada sizga murojat qilishadi"
            : "Ваш заказ отправлен администратору, они свяжутся с вами в ближайшее время");
    }
}

