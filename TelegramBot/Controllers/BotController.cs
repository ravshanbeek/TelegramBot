using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TelegramBot.Services;

namespace TelegramBot.Controllers
{
    [Route("bot")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update,
            [FromServices] UpdateHandler updateHandler)
        {
            await updateHandler.HandleUpdateAsync(update);

            return Ok();
        }
    }
}