using System;
using System.Net;

namespace Sem3ReWrite
{
    public abstract class HttpServer
    {
        private static HttpListener listener;

        public static void Main(string[] args)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8000/");
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", "http://localhost:8000/");
            
            var listenTask = HttpHandler.HandleResponse(listener);
            listenTask.GetAwaiter().GetResult();
            
            listener.Close();
        }
    }
}