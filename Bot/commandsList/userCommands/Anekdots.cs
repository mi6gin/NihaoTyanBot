using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NihaoTyan.Bot.commandsList.userCommands
{
    public static class Anecdote
    {
        public static async Task Send(Message message, TelegramBotClient botClient)
        {
            Random _random = new Random();

            string url = "https://vk.com/doc464597842_661379755?hash=7jyz9yZBhbfZuaZN2ZVRRBG8BmPm6AMNtq7JIaz7Rng&dl=zczzaOW5BtO5IeIzlCXmSScOdD2QF82XmdAxk1ifMhw";

            using (HttpClient httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                var stream = await response.Content.ReadAsStreamAsync();
                using (StreamReader reader = new StreamReader(stream))
                {
                    string content = await reader.ReadToEndAsync();
                    var anecdotes = content.Split(new[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    var randomAnecdote = anecdotes[_random.Next(anecdotes.Length)];
                    await botClient.SendTextMessageAsync(message.Chat.Id, randomAnecdote);
                }
            }
        }
    }
}
