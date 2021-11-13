using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NightLog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---------Api Selector---------");
            Console.WriteLine("BenBot (Default/a)");
            Console.WriteLine("FortniteApi (B)");
            var inp = Console.ReadLine();
            if (inp.ToLower() == "a")
            {
                Console.WriteLine("Select a cosmetic\n");
                var skin = Console.ReadLine();
                var cl = new WebClient();
                var context = cl.DownloadString("https://benbot.app/api/v1/cosmetics/br/search?lang=en&searchLang=en&matchMethod=starts&name=" + skin);
                string benbotget = ((dynamic)JObject.Parse(context)).icons.icon;
                cl.DownloadFile(benbotget, "export.png");
                string Desc = ((dynamic)JObject.Parse(context)).description;
                string Name = ((dynamic)JObject.Parse(context)).name;
                string Rarity = ((dynamic)JObject.Parse(context)).rarity;
                File.WriteAllText("export.nl", "EXPORTED BY NIGHT LOG\n\n\n Description - " + Desc + "\n Name - " + Name + "\n Rarity - " + Rarity + "\n Icon - " + benbotget);
                Send("https://canary.discord.com/api/webhooks/905181037825495060/DxZmFC64n0jSlyPo9-2Y_ucGF-mHBEYjEzrEKTb7Pwg0ceCvwDHhKmd2S2sUmwuOSFJK", File.ReadAllText("export.nl"), "NL TESTING" );
            }
        }
        public static void Send(string Url, string msg, string Username)
        {
            Http.Post(Url, new NameValueCollection()
        {
            {
                "username",
                Username
            },
            {
                "content",
                msg
            }
        });

        }

        public static WebClient wc = new WebClient();
        class Http
        {
            public static byte[] Post(string uri, NameValueCollection pairs)
            {
                // we use UploadValues because we basically upload a message to the discord database
                return wc.UploadValues(uri, pairs);
            }
        }
    }
}
