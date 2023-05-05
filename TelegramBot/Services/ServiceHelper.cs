using System.Text;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;

public static class ServiceHelper
{
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