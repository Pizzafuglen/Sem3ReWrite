using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sem3ReWrite
{
    public abstract class HttpHandler
    {
        private static bool runServer = true;

        public static async Task HandleResponse(HttpListener listener)
        {
            while (runServer)
            {
                var ctx = await listener.GetContextAsync();
                var req = ctx.Request;
                var resp = ctx.Response;

                var data = RequestData(req);

                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }

        private static byte[] RequestData(HttpListenerRequest req)
        {
            var data = new byte[] { };

            if (req.HttpMethod == "GET")
            {
                switch (req.Url.AbsolutePath)
                {
                    case "/statecurrent":
                        data = Encoding.UTF8.GetBytes(OpcUaRead.ReadNodeValue("ns=6;s=::Program:Cube.Status.StateCurrent"));
                        break;
                    case "/currentbatchid":
                        data = Encoding.UTF8.GetBytes(OpcUaRead.ReadNodeValue("ns=6;s=::Program:Cube.Status.Parameter[0].Value"));
                        break;
                    case "/shutdown":
                        runServer = false;
                        break;
                    default:
                        data = Encoding.UTF8.GetBytes("URL not found");
                        break;
                }
            }
            else
            {
                switch (req.Url.AbsolutePath)
                {
                    default:
                        data = Encoding.UTF8.GetBytes("URL not found");
                        break;
                }
            }
            return data;
        }
    }
}