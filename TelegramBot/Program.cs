using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;


namespace TelegramBot
{
    class Program
    {
        static ITelegramBotClient botClient;

        static void Main()
        {
            botClient = new TelegramBotClient("");

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
              $"My User {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
            Thread.Sleep(int.MaxValue);
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                DateTime lastDayYear = new DateTime(DateTime.Now.Year, 12, 31);
                var dayYear = DateTime.Now.DayOfYear;

                var percentage = (dayYear * 100) / lastDayYear.DayOfYear;

                await botClient.SendTextMessageAsync(
                  chatId: e.Message.Chat,
                  text: "Has passed " + percentage + "% of this year"
                );
            }
        }
    }
}

