using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot
{
    public class Program
    {
        private static TelegramBotClient bot { get; set; }
        static async Task Main(string[] args)
        {
            bot = new TelegramBotClient("7736154319:AAEHu8fE4g8l2bAs_DE1RdeGzrFqSfTlQh8");
            bot.OnMessage += Bot_OnMessage;

            Console.ReadLine();
        }

        private static async Task Bot_OnMessage(Message message, UpdateType type)
        {
            Console.WriteLine($"{message.Contact?.UserId}: {message.Text}");

            if (message.Text == "/start")
            {
                await bot.SendMessage(
                    chatId: message.Chat.Id,
                    text: "Запустити WebApp:",
                    replyMarkup: new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithWebApp("Відкрити", new WebAppInfo("https://ja5676.github.io/course_01-Marryweater/"))
                    )
                );
            }
        }
    }
}

