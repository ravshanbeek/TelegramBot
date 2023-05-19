using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private readonly ITelegramBotClient client;
    private Dictionary<long, User> users = new Dictionary<long, User>();
    private readonly string db = @"C:\Users\Ravshan\Desktop\Projects\TelegramBot\TelegramBot\Models\basa.txt";
    private readonly string resourceDb = @"C:\Users\Ravshan\Desktop\Projects\TelegramBot\TelegramBot\Models\Resource.txt";
    private Resource resource = new Resource();

    public UpdateHandler(ITelegramBotClient client)
    {
        this.client = client;
    }

    public async Task HandleUpdateAsync(Update update)
    {
        switch (update.Type)
        {
            case UpdateType.CallbackQuery: await HandleCallBackQuery(update); break;
            case UpdateType.Message: await HandleMessage(update.Message); break;
        }
    }
    private async Task HandleNotAvailableCommandAsync(Message message)
    {
        if (message is null)
            return;

        Read();
        ReadResource();
        var u = message.From ?? throw new ArgumentNullException(nameof(message));

        if (resource.Admin.Id == u.Id)
        {
            switch (resource.DataName)
            {
                case "Admin": resource.ContactWithAdmin = message.Text; break;
                case "Kopywriting": resource.CopyWriting = message.Text; break;
            }

            resource.DataName = null;

            WriteResource();

            await client.SendTextMessageAsync(
                chatId: u.Id,
                text: "Amaliyot muvofaqqiyatli tugatildi");
            return;
        }

        var user = users[u.Id];

        if (users[u.Id].Order?.Status == 1)
        {
            users[user.Id].Order.InstagramUrl = message.Text;
            users[user.Id].Order.Status = 2;

            Write();

            await client.SendTextMessageAsync(
                chatId: user.Id,
                text: users[u.Id].Language == 1 ? "TZ ni yuboring" : "Отправить ТЗ");

            return;
        }

        if (users[user.Id].Order.Status == 2)
        {
            users[user.Id].Order.Text = message.Text;
            users[user.Id].Order.Status = 3;
            
            var requestLocation = new ReplyKeyboardMarkup(
            new[] { new KeyboardButton("Share Location") { RequestLocation = true}});

            requestLocation.ResizeKeyboard = true;
            requestLocation.OneTimeKeyboard = true;
            
            if(user.Order.Service == "MobilGrafiya")
            {
                await client.SendTextMessageAsync(
                    chatId: user.Id,
                    text: user.Language == 1 ? "Iltimos Lokatsiya yuboring" :
                    "пожалуйста, пришлите мне ваше местоположение",
                    replyMarkup: requestLocation);

                Write();
                return;
            }

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
            return;
        }

        if(users[user.Id].Order.Status == 4)
        {
            await client.SendTextMessageAsync(
                chatId: resource.Admin.Id,
                text: "Mijoz uchun maqul vaqt\n" +
                    $"{message.Text}");
            return;
        }
        await this.client.SendTextMessageAsync(
            chatId: user.Id,
            text: "Mavjud bo'lmagan komanda kiritildi. " +
                  "Tekshirib ko'ring.");
    }

    #region Read and Write users
    private void Write()
    {
        using StreamWriter wr = new StreamWriter(db);
        wr.Write(JsonSerializer.Serialize(users));
    }

    private void Read()
    {
        using StreamReader sr = new StreamReader(db);
        var text = sr.ReadToEnd();
        if (!String.IsNullOrEmpty(text))
        {
            this.users = JsonSerializer.Deserialize<Dictionary<long, User>>(text);
        }
    }
    #endregion
    #region Read and Write resourse database
    private void WriteResource()
    {
        using StreamWriter wr = new StreamWriter(resourceDb);
        wr.Write(JsonSerializer.Serialize(resource));
    }

    private void ReadResource()
    {
        using StreamReader sr = new StreamReader(resourceDb);
        var text = sr.ReadToEnd();
        if (!String.IsNullOrEmpty(text))
        {
            this.resource = JsonSerializer.Deserialize<Resource>(text);
        }
    }
    #endregion

}
