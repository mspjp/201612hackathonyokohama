using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.IO;
using System.Web.Http.Results;

namespace SampleBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        private async Task OnMessageAsync(ConnectorClient connector,Activity message)
        {
            Activity reply = message.CreateReply($"You sent {message.Text} ");
            throw new Exception("hogehog");
            await connector.Conversations.ReplyToActivityAsync(reply);
        }
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            try
            {
                await OnMessageAsync(connector, activity);
            }catch(Exception e)
            {
                connector.Conversations.ReplyToActivity(activity.CreateReply(e.Message));
                WriteLog(e.Message);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private void WriteLog(string log)
        {
            using (var writer = new StreamWriter("../default.htm",true))
            {
                var logStr = string.Format("[{0}] {1}", DateTime.Now.ToString("MM/dd/ HH:mm:ss"), log);
                writer.WriteAsync(logStr).Wait();
            }
        }
        
    }
}