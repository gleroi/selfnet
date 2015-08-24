using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            Program.Run().Wait();
        }

        private static async Task Run()
        {
            var api = new SelfossApi(new ConnectionOptions
            {
                Host = "host.com",
                Base = "selfoss",
                Username = "",
                Password = ""
            });

            while (Console.ReadKey().KeyChar != 'q')
            {
                try
                {
                    var source = new Source
                    {
                        Id = 25,
                        Title = "Ayende @ Rahien",
                        Tags = new List<string> {".net", "raven", "new"},
                        Spout = @"spouts\rss\feed",
                        Params = new Dictionary<string, string>
                        {
                            ["url"] = "http://feeds.feedburner.com/AyendeRahien"
                        }
                    };
                    var result = await api.Sources.Save(source);
                    if (result)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
