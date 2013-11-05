using System;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;
using SignalRHostWithUnity.Dto;
using SignalRHostWithUnity.Unity;

namespace SignalRHostWithUnity
{
    class Program
    {
	    private static  IHubContext _hubContext;

        static void Main(string[] args)
        {
		    GlobalHost.DependencyResolver.Register(typeof(IHubActivator), () => new UnityHubActivator(UnityConfiguration.GetConfiguredContainer()));

            string url = "http://localhost:8089";
					
            using (WebApp.Start(url))
            {
			    _hubContext = GlobalHost.ConnectionManager.GetHubContext<MyHub>();

                Console.WriteLine("Server running on {0}", url);
				
                while (true)
                {
                    string key = Console.ReadLine();
                    if (key.ToUpper() == "W")
                    {
                        _hubContext.Clients.All.addMessage("server", "ServerMessage");
                        Console.WriteLine("Server Sending addMessage\n");
                    }
                    if (key.ToUpper() == "E")
                    {
                        _hubContext.Clients.All.heartbeat();
                        Console.WriteLine("Server Sending heartbeat\n");
                    }
                    if (key.ToUpper() == "R")
                    {
                        var helloModel = new HelloModel {Age = 37, Molly = "pushed direct from Server "};
                        _hubContext.Clients.All.sendHelloObject(helloModel);
                        Console.WriteLine("Server Sending sendHelloObject\n");
                    }
                    if (key.ToUpper() == "C")
                    {
                        break;
                    }
                }

                Console.ReadLine();
            }
        }
    }
}
