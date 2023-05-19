using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Yandex.Geocoder;

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

        var re = new ReverseGeocoderRequest()
        {
            Latitude = location.Latitude,
            Longitude = location.Longitude
        };

        Write();

        await client.SendTextMessageAsync(
            chatId: message.From.Id,
            text: "Ism familiyangizni quyidagi formatda yuboring.\n" +
            "/register Palonchiyev Pistonchi",
            replyMarkup: new ReplyKeyboardRemove());
    }
}

