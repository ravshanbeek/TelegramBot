namespace TelegramBot.Models;

public class Order
{
    public string? Service { get; set; }
    public string? InstagramUrl { get; set; }
    public string? InstagramParol { get; set; }
    public string? Text { get; set; }
    public int? Status { get; set; }

    public Order()
    {
    }

    public Order(string? service,
        string? instagramUrl,
        string? instagramParol,
        string? text,
        int? status)
    {
        Service = service;
        InstagramUrl = instagramUrl;
        InstagramParol = instagramParol;
        Text = text;
        Status = status;
    }
}
