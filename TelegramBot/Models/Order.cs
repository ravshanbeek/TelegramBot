namespace TelegramBot.Models;

public class Order
{
    public string? Service { get; set; }
    public string? InstagramUrl { get; set; }
    public string? Text { get; set; }
    public int? Status { get; set; }
    public bool? IsAccepted { get; set; }

    public Order()
    {
    }

    public Order(string? service,
        string? instagramUrl,
        string? text,
        int? status,
        bool? isAccepted)
    {
        Service = service;
        InstagramUrl = instagramUrl;
        Text = text;
        Status = status;
        IsAccepted = isAccepted;
    }
}
