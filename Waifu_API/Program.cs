using System;
using System.Collections.Generic;
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


            List<string> types = new List<string> { "sfw", "nsfw" };
            Console.WriteLine(WriteList(types));
            string type = Console.ReadLine();

            List<string> catSFW = new List<string> { "waifu", "neko","shinobu", "megumin", "bully", "cuddle", "cry",
                                                     "hug", "awoo", "kiss", "lick", "pat", "smug", "bonk", "yeet", 
                                                     "blush", "smile", "wave", "highfive", "handhold", "nom", "bite",
                                                     "glomp", "slap", "kill", "happy", "wink", "poke", "dance", "cringe"};
            List<string> catNSFW = new List<string> { "waifu", "neko", "trap", "blowjob"  };

            List<string> categ = type == "sfw" ? catSFW : catNSFW;
            Console.WriteLine(WriteList(categ)); 
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
                var picURL = JsonConvert.DeserializeObject<Root>(result);
                System.Diagnostics.Process.Start(picURL.url);
            }
        }

        static string WriteList(List<string> list)
        {
            string resultString = null;
            foreach(var el in list)
            {
                resultString += $"{el} ";
            }

            return resultString;
        }
    }
}
