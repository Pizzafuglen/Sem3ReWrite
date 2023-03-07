namespace Sem3ReWrite
{
    public class OldIncomingHandler
    {
        /*
        public static async Task HandleIncomingConnections()
        {
            var runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Write the response info
                var disableSubmit = !runServer ? "disabled" : "";
                var data = new byte[] { };

                if (req.Url.AbsolutePath.Contains("helloThere"))
                    data = Encoding.UTF8.GetBytes("tester");
                else if (req.HttpMethod == "POST" && req.Url.AbsolutePath == "/shutdown")
                    runServer = false;
                else
                    data = Encoding.UTF8.GetBytes(string.Format(pageData, pageViews, disableSubmit));

                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }
        */
    }
}