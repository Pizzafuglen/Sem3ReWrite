﻿using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sem3ReWrite
{
    internal class Program
    {
        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        public static int pageViews = 0;
        public static int requestCount = 0;

        public static string pageData = 
            "<!DOCTYPE>" +
            "<html>" +
            "  <head>" +
            "    <title>HttpListener Example</title>" +
            "  </head>" +
            "  <body>" +
            "    <p>Page Views: {0}</p>" +
            "    <form method=\"post\" action=\"shutdown\">" +
            "      <input type=\"submit\" value=\"Shutdown\" {1}>" +
            "    </form>" +
            "  </body>" +
            "</html>";

        public static void Main(string[] args)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                // Console.WriteLine("Request #: {0}", ++requestCount);
                // Console.WriteLine(req.Url.ToString());
                // Console.WriteLine(req.HttpMethod);
                // Console.WriteLine(req.UserHostName);
                // Console.WriteLine(req.UserAgent);
                // Console.WriteLine();

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = new byte[] { };
                
                if (req.Url.AbsolutePath.Contains("helloThere"))
                {
                    data = Encoding.UTF8.GetBytes("tester");
                }
                else if (req.HttpMethod == "POST" && req.Url.AbsolutePath == "/shutdown")
                {
                    runServer = false;
                }
                else 
                {
                    data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                }
                
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }
    }
}