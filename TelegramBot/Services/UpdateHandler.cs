using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private readonly ITelegramBotClient client;
    private Dictionary<long,User> users = new Dictionary<long, User>();
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
            case UpdateType.CallbackQuery:await HandleCallBackQuery(update); break;
            case UpdateType.Message:await HandleMessage(update.Message); break;
            
        }
    }
    private async Task HandleNotAvailableCommandAsync(Message message)
    {
        if (message is null) 
        {
            return;
        }

        await this.client.SendTextMessageAsync(
            chatId: message.From.Id,
            text: "Mavjud bo'lmagan komanda kiritildi. " +
                  "Tekshirib ko'ring.");
    }

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
}
