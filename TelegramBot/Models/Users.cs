using TelegramBot.Models;

public class User
{
    public long Id { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? ActualPhoneNumber { get; set; }
    public Order Order { get; set; }
    public int Language { get; set; }

    public User(long id,
        string? userName = null,
        string? firstName = null,
        string? lastName = null,
        int language = 0,
        string? phoneNumber = null,
        string? actualPhoneNumber = null,
        Order? order = null)
    {
        Id = id;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        ActualPhoneNumber = actualPhoneNumber;
        Language = language;
        Order = order ?? new Order();
    }

}
