﻿using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Services;

public partial class UpdateHandler
{
    private async Task HandleMessageTextAsync(Message message)
    {
        var command = message.Text;

        try
        {
            var task = command switch
            {
                "/start" => HandleStartCommandAsync(message),
                "uz" => HandleLanguageCommandAsync(message),
                "ru" => HandleLanguageCommandAsync(message),
                "Mobilografiya" => MobileGrafic(message),
                "Grafik dizayn" => GraphicDisign(message),
                "Video Montaj" => MontageVideo(message),
                "Kopywriting" => CopyWriting(message),
                "Admin bilan aloqa" => HandleContactWithAdminAsync(message),
                "Zakaz berish" => HandleOrderAsync(message),
                "SMM" => HandleSMMAsync(message),
                "Grafik Dizayn" => HandleGraficDisign(message),
                "MobilGrafiya" => HandleMobileGrafic(message),
                "Asosiy Menyu" => BackToMain(message),
                "Orqaga" => BackToMain(message),
                "Admin" => AdminMenuAsync(message),

                "Прайс лист" => CategoryOfPrice(message),
                "Мобилография" => MobileGrafic(message),
                "Графический дизайнер" => GraphicDisign(message),
                "Видео монтаж" => MontageVideo(message),
                "Копирайтинг" => CopyWriting(message),
                "Для связи с нами" => HandleContactWithAdminAsync(message),
                "Заказать" => HandleOrderAsync(message),
                "СММ" => HandleSMMAsync(message),
                "МобилоГрафия" => HandleMobileGrafic(message),
                "Графический Дизайнер" => HandleGraficDisign(message),
                "Главное меню" => BackToMain(message),
                "Назад" => BackToMain(message),

                _ => HandleNotAvailableCommandAsync(message)
            };

            await task;
        }
        catch (Exception)
        {
            await this.client.SendTextMessageAsync(
                chatId: message.From.Id,
                text: "Failed to handle your request. Please try again");
        }
    }

    private Task AdminMenuAsync(Message message)
    {
        Read();
        ReadResource();
        resource.Admin = users[5130690942];

        if (message.From.Id != resource.Admin.Id)
            return Task.CompletedTask;

        var inlineKeyboard = new InlineKeyboardMarkup(new[]
        {
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Admin bilan aloqa", "Admin"),
                InlineKeyboardButton.WithCallbackData("Прайс лист", "Category")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Mobilografiya", "MobileGrafic"),
                InlineKeyboardButton.WithCallbackData("Grafik dizayn", "GraficDisign")
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("Video Montaj", "VideoMontage"),
                InlineKeyboardButton.WithCallbackData("Kopywriting", "Kopywriting")
            }
        });

        client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: "Nimani o'zgartirmoqchisiz",
            replyMarkup: inlineKeyboard);

        return Task.CompletedTask;
    }

    private async Task BackToMain(Message message)
    {
        Read();
        var user = users[message.From.Id];

        client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: user.Language == 1 ? "Menyu" : "Меню",
            replyMarkup: user.Language == 1 ? GenerateMainMenuUz() : GenerateMainMenuRu());
    }

    private Task HandleMobileGrafic(Message message)
    {
        throw new NotImplementedException();
    }

    private async Task HandleGraficDisign(Message message)
    {
        Read();
        var user = users[message.Chat.Id];

        var requestGraficDisignuz = new ReplyKeyboardMarkup(new[] {
            new[]
            {
                new KeyboardButton("Logo"),
                new KeyboardButton("Dizayn")},
            new[]
            {
                new KeyboardButton("Asosiy Menyu")
            }
        });
        requestGraficDisignuz.ResizeKeyboard = true;

        var requestGraficDisignru = new ReplyKeyboardMarkup(new[] {
            new[]
            {
                new KeyboardButton("Логотип"),
                new KeyboardButton("Дизайн")},
            new[]
            {
                new KeyboardButton("Главное меню")
            }
        });
        requestGraficDisignru.ResizeKeyboard = true;

        await this.client.SendTextMessageAsync(
            chatId: message.Chat.Id,
            text: user.Language == 1 ? "Xizmatlarimiz" : "Наши сервисы",
            replyMarkup: user.Language == 1 ? requestGraficDisignuz : requestGraficDisignru);

    }

    private Task HandleSMMAsync(Message message)
    {
        throw new NotImplementedException();
    }

    private async Task CopyWriting(Message message)
    {
        var user = message.From;
        ReadResource();

        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: resource.CopyWriting);
    }

    private Task MontageVideo(Message message)
    {
        var user = message.From;
        ReadResource();
        var file = new InputOnlineFile(resource.VideoMontage);

        client.SendVideoAsync(user.Id, file);

        return Task.CompletedTask;
    }

    private Task GraphicDisign(Message message)
    {
        var user = message.From;
        ReadResource();
        var file = new InputOnlineFile(resource.GraficDisign);

        client.SendPhotoAsync(user.Id, file);

        return Task.CompletedTask;
    }

    private Task MobileGrafic(Message message)
    {
        var user = message.From;
        ReadResource();
        var file = new InputOnlineFile(resource.MobileGrafic);

        client.SendVideoAsync(user.Id, file);

        return Task.CompletedTask;
    }

    private async Task HandleOrderAsync(Message message)
    {
        Read();
        var user = users[message.From.Id];

        var requestOrderuz = new ReplyKeyboardMarkup(new[] {
            new[]
            {
                new KeyboardButton("SMM"),
                new KeyboardButton("Grafik Dizayn"),
                new KeyboardButton("MobilGrafiya")},
            new[]
            {
                new KeyboardButton("Orqaga")
            }
        });
        requestOrderuz.ResizeKeyboard = true;

        var requestOrderru = new ReplyKeyboardMarkup(new[] {
            new[]
            {
                new KeyboardButton("СММ"),
                new KeyboardButton("Графический Дизайнер"),
                new KeyboardButton("МобилоГрафия")},
            new[]
            {
                new KeyboardButton("Назад")
            }
        });
        requestOrderru.ResizeKeyboard = true;

        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: user.Language == 1 ? "Xizmatlar" : "Услуги",
            replyMarkup: user.Language == 1 ? requestOrderuz : requestOrderru);
    }

    private async Task HandleContactWithAdminAsync(Message message)
    {
        var user = message.From;
        ReadResource();
        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: resource.ContactWithAdmin);
    }

    private Task CategoryOfPrice(Message message)
    {
        var user = message.From;
        ReadResource();
        var doc = new InputOnlineFile(resource.Category);

        client.SendDocumentAsync(user.Id, doc);

        return Task.CompletedTask;
    }

    private async Task HandleLanguageCommandAsync(Message message)
    {
        var user = message.From;
        Read();
        if (!users.ContainsKey(user.Id))
        {
            users.Add(user.Id, new User(user.Id, user.Username, user.FirstName, user.LastName));
        }
        int lan = message.Text.Contains("uz") ? 1 : 2;
        users[user.Id].Language = lan;

        Write();

        var requestContact = new ReplyKeyboardMarkup(new[] {
            new[] { new KeyboardButton("Share contact") { RequestContact = true} }
        });

        requestContact.ResizeKeyboard = true;
        requestContact.OneTimeKeyboard = true;

        await client.SendTextMessageAsync(
            chatId: user.Id,
            text: lan == 1 ? "Iltimos raqamingizni biz bilan ulashing" :
                "Пожалуйста, поделитесь с нами своим номером",
            replyMarkup: requestContact);
    }
    private async Task HandleStartCommandAsync
        (Message message)
    {
        Read();
        var user = message.From;
        if (!users.ContainsKey(message.Chat.Id))
        {
            users.Add(user.Id, new User(user.Id, user.Username, user.FirstName, user.LastName));
            Write();

            return;
        }

        var requestLanguage = new ReplyKeyboardMarkup(new[] {
            new[] { new KeyboardButton("uz"),
                    new KeyboardButton("ru")}
        });

        requestLanguage.ResizeKeyboard = true;
        requestLanguage.OneTimeKeyboard = true;

        await this.client.SendTextMessageAsync(
                chatId: message.From.Id,
                text: "Tilni tanlang\nВыберите язык",
                replyMarkup: requestLanguage);
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

    private string MapLink(double latitude, double longitude)
    {
        string link = $"https://maps.google.com/maps?q={longitude},{latitude}&ll={longitude},{latitude}&z=16";

        return link;
    }
}
