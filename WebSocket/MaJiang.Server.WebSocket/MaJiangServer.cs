using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MaJiang.Core;
using MaJiang.WebSocket.Core.Server;
using MaJiang.WebSocket.Core;

namespace MaJiang.Server.WebSocket
{
    public class MaJiangServer
    {
        private HttpServer _httpServer;

        private Dictionary<string, Game> _games;

        public Dictionary<string, Game> Games
        {
            get
            {
                if (_games == null)
                {
                    _games = new Dictionary<string, Game>();
                }
                return _games;
            }
        } 

        public HttpServer HttpServer
        {
            get
            {
                if (_httpServer == null)
                {
                    _httpServer = new HttpServer(ConfigurationManager.AppSettings["ServerUrl"]);

#if DEBUG
                    _httpServer.Log.Level = LogLevel.Trace;

                    // To change the wait time for the response to the WebSocket Ping or Close.
                    //httpsv.WaitTime = TimeSpan.FromSeconds (2);

                    // Not to remove the inactive WebSocket sessions periodically.
                    //httpsv.KeepClean = false;
#endif

                    _httpServer.RootPath = ConfigurationManager.AppSettings["RootPath"];
                    _httpServer.OnGet += HttpServerOnOnGet;
                    _httpServer.OnConnect += HttpServerOnConnect;
                    _httpServer.AddWebSocketService<MaJiangGame>("/MaJiangGame", Initializer);
                }
                return _httpServer;
            }
        }

        private MaJiangGame Initializer()
        {
            var output = new MaJiangGame();
            output.Games = Games;
            return output;
        }

        private void HttpServerOnConnect(object sender, HttpRequestEventArgs e)
        {
            //HttpServer.Log.Debug(e.Request.UserAgent);
        }

        public MaJiangServer()
        {
            
        }

        public void Start()
        {
            HttpServer.Start();
        }

        public void Stop()
        {
            HttpServer.Stop();
        }

        private void HttpServerOnOnGet(object sender, HttpRequestEventArgs e)
        {
            var req = e.Request;
            var res = e.Response;

            var path = req.RawUrl;
            if (path == "/")
                path += "index.html";

            var content = HttpServer.GetFile(path);
            if (content == null)
            {
                res.StatusCode = (int)HttpStatusCode.NotFound;
                return;
            }

            if (path.EndsWith(".html"))
            {
                res.ContentType = "text/html";
                res.ContentEncoding = Encoding.UTF8;
            }
            else if (path.EndsWith(".js"))
            {
                res.ContentType = "application/javascript";
                res.ContentEncoding = Encoding.UTF8;
            }

            res.WriteContent(content);
        }


    }
}
