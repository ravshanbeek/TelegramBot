using System.Text.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;

public static class ServiceHelper
{
    public static async Task<List<string>> GetAddressAsync(double lat, double lng)
    {
        var apiKey = "AIzaSyBq0twb915arXgAKwJXr3XDocU_jt2TgzU";
        var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={apiKey}";

        using var client = new HttpClient();

        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var address = await response.Content.ReadAsStringAsync();

        var root = JsonSerializer.Deserialize<Root>(address);

        return root.results
            .Select(res => res.formatted_address)
            .Take(3).ToList();
    }

    public static InlineKeyboardMarkup GenerateButtons(int page)
    {
        var buttons = new List<List<InlineKeyboardButton>>();

        for (int index = 0; index <= page; index++)
        {
            if (index % 3 == 0)
                buttons.Add(new List<InlineKeyboardButton>());

            buttons[index / 3].Add(
                new InlineKeyboardButton($"{index + 1}")
                {
                    CallbackData = $"{index + 1}"
                }
            );
        }

        return new InlineKeyboardMarkup(buttons);
    }
}