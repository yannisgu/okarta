using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Hosting.Self;

namespace Okarta.Web.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:1235";
            Console.Out.WriteLine("Start server on " + url);
            using (var host = new NancyHost(new Uri(url)))
            {
                host.Start();
                Console.Out.WriteLine("Started server. Open " + url);
                Console.ReadLine();
            }
        }
    }
}
