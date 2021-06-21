using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Telegram.Bot;

namespace Telegram_Bot
{
    class Program
    {
        [Obsolete]
        static void Main(string[] args)
        {

            TelegramBotClient bot = new TelegramBotClient(File.ReadAllText(@"..\..\..\token.txt"));

            bot.OnMessage += (s, arg) =>
            {
                try
                {
                    var path = arg.Message.Text.Split(' ');
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, Waifu(path[0], path[1]));
                }
                catch
                {
                    bot.SendTextMessageAsync(arg.Message.Chat.Id, "Invalid query");
                }
            };

            bot.StartReceiving();




            Console.ReadKey(); //Строка не нужна, она для инвалидов
        }

        static string Waifu(string type, string cat)
        {
            string url = @"https://api.waifu.pics/";

            var request = WebRequest.Create($"{url}{type}/{cat}");
            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return "Error";
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                var picURL = JsonConvert.DeserializeObject<Root>(result);
                return picURL.url;
            }
        }
    }
}