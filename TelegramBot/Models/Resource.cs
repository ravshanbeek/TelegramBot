namespace TelegramBot.Models;

public class Resource
{
    public User Admin { get; set; }
    public string? ContactWithAdmin { get; set; }
    public string? Category { get; set; }
    public string? MobileGrafic { get; set; }
    public string? GraficDisign { get; set; }
    public string? VideoMontage { get; set; }
    public string? DataName { get; set; }
    public string? CopyWriting { get; set; }

    public Resource()
    {   }
    public Resource(User admin,
        string? contactWithAdmin = null,
        string? category = null,
        string? mobileGrafic = null,
        string? graficDisign = null,
        string? vedioMontage = null,
        string? dataName = null,
        string? copyWriting = null)
    {
        Admin = admin;
        Category = category;
        DataName = dataName;
        CopyWriting = copyWriting;
        MobileGrafic = mobileGrafic;
        GraficDisign = graficDisign;
        VideoMontage = vedioMontage;
        ContactWithAdmin = contactWithAdmin;
    }
}
