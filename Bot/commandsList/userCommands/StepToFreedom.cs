using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace NihaoTyan.Bot.commandsList.userCommands
{
    class SteptoFreedom
    {
        private static readonly TelegramBotClient Bot = new TelegramBotClient(Config.Token);
        private static long _userId;
        private static long _chatId;

        public static int xn;
        public static string sonicX;
        public static bool continueCycle = false;

        public static async Task SteptoFreedomStart(Message message, TelegramBotClient botClient)
        {
            _chatId = message.Chat.Id;
            _userId = message.From.Id;
            var photoUrl = "https://sun9-35.userapi.com/impg/d3F87txM3NsX4b6fg-_iICq-bmCwx4vmNvw9pQ/sc4faeaM5aU.jpg?size=1024x1024&quality=95&sign=122617b191efba153ec5591d477f43f0&type=album";

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Запуск", "callback_data_1"),
                    InlineKeyboardButton.WithCallbackData("Отмена", "callback_data_2")
                }
            });

            var msgTodelete = await botClient.SendPhotoAsync(_chatId, photoUrl,
                "Привет товарищ mi6gun, ты запустил особый режим\r\nРежим ПРОГУЛКИ подразумевает концентрацию и усидчевость\r\nКак будешь готов дай мне знать",
                replyMarkup: keyboard);
        }

        public static async Task SteptoFreedomEnd(Message message, TelegramBotClient botClient)
        {
            _chatId = message.Chat.Id;
            _userId = message.From.Id;
            var photoUrl = "https://sun9-1.userapi.com/impg/dnvKms_lz49AsprMp7u2WORn8iBtyAShpRcQKg/WincNLcilWA.jpg?size=1170x1170&quality=95&sign=c5ebdc06d298cdd8d3812611b1f8ae56&type=album";

            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Запуск", "callback_data_1"),
                    InlineKeyboardButton.WithCallbackData("Отмена", "callback_data_2")
                }
            });

            var msgTodelete = await botClient.SendPhotoAsync(_chatId, photoUrl,
                "Вы МОШЫНА товарищ\nМожем повторить?",
                replyMarkup: keyboard);

            // Wait for user action
            while (!continueCycle)
            {
                await Task.Delay(1000);
            }
        }

        public async Task HandleCallbackQuery(CallbackQuery callbackQuery, Message message, TelegramBotClient botClient)
        {
            var chatId = callbackQuery.Message.Chat.Id;
            var messageId = callbackQuery.Message.MessageId;

            if (callbackQuery.Data == "callback_data_1")
            {
                continueCycle = true;
                await botClient.DeleteMessageAsync(chatId, messageId);
                await STFhelper(message, botClient);
            }
            else if (callbackQuery.Data == "callback_data_2")
            {
                continueCycle = false;
                await botClient.DeleteMessageAsync(chatId, messageId);
            }
        }

        public static async Task STFhelper(Message message, TelegramBotClient botClient)
        {
            if (xn == 2)
            {
                xn = 1;
                await STF(message, botClient);
            }
            else if (xn == 1)
            {
                xn = 7;
                await SteptoFreedomEnd(message, botClient);
            }
            else
            {
                xn = 0;
                await STF(message, botClient);
            }
        }

        public static async Task STF(Message message, TelegramBotClient botClient)
        {
            var chatId = message.Chat.Id;
            if (xn == 0)
            {
                sonicX = "Впахивай как стахановец следующие";
                xn = 2;
            }
            else if (xn == 1)
            {
                sonicX = "Отдыхай пролетарий следующие";
            }
            int totalSeconds = xn * 60; // Общее количество секунд (30 минут)
            int remainingSeconds = totalSeconds;

            Message timerMessage = null;

            while (remainingSeconds > 0)
            {
                string timeString = $"{sonicX} {remainingSeconds / 60:00}:{remainingSeconds % 60:00}";

                if (timerMessage == null)
                {
                    timerMessage = await botClient.SendTextMessageAsync(chatId, timeString);
                }
                else
                {
                    await botClient.EditMessageTextAsync(chatId, timerMessage.MessageId, timeString);
                }

                await Task.Delay(1000);
                remainingSeconds--;
            }
            await STFhelper(message, botClient);
        }
    }
}
