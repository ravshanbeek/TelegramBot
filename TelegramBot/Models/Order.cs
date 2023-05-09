namespace TelegramBot.Models;

public class Order
{
    public long UserId { get; set; }
    public string OrderName { get; set; }
    public int OrderStatus { get; set; }
}
