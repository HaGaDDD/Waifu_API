using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Waifu_API
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"https://api.waifu.pics/";
            

            Console.WriteLine("sfw or nsfw?");

            string type = Console.ReadLine();

            List<string> catSFW = new List<string> { "waifu", "neko","shinobu", "megumin", "bully", "cuddle", "cry",
                                                     "hug", "awoo", "kiss", "lick", "pat", "smug", "bonk", "yeet", 
                                                     "blush", "smile", "wave", "highfive", "handhold", "nom", "bite",
                                                     "glomp", "slap", "kill", "happy", "wink", "poke", "dance", "cringe"};
            List<string> catNSFW = new List<string> { "waifu", "neko", "trap", "blowjob"  };

            if (type == "sfw")
            {
                foreach (var c in catSFW)
                    Console.Write(c + ", ");
            }

            else
            {
                foreach (var c in catNSFW)
                    Console.Write(c + ", ");
            }

            string cat = Console.ReadLine();

            var request = WebRequest.Create($"{url}{type}/{cat}");


            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
                return;
            }

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                var weatherForecast = JsonConvert.DeserializeObject<Root>(result);
                Console.WriteLine(weatherForecast.url);
            }
        }
    }
}
