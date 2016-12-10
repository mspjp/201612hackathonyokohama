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
using System.Collections.Generic;
using BotLibrary;

namespace SampleBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        
        private async Task OnMessageAsync(ConnectorClient connector,Activity message)
        {
            var responses = RuleManager.Instance.SearchResponses(message.Text);
            foreach(var res in responses)
            {
                if (res.Contains("{") && res.Contains("}"))
                {
                    string commandName = res.Replace("{","").Replace("}","");
                    await OnCommandAsync(connector,message,commandName);
                }else
                {
                    var reply = message.CreateReply(res);
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
            }

            if(responses.Count == 0)
            {
                var reply = message.CreateReply("ルールにヒットしませんでした");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
        }

        private async Task OnCommandAsync(ConnectorClient connector, Activity message, string command)
        {
            if (command == "command1")
            {
                var reply = message.CreateReply("execute " + command);
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
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
                WriteLog(e.Message);
                connector.Conversations.ReplyToActivity(activity.CreateReply("エラーが発生しました: "+e.Message));
                
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        public HttpResponseMessage Get()
        {
            var viewStr ="*** errors *** \n\n\n" + string.Join("\n",_logList);
            var message = Request.CreateResponse(HttpStatusCode.OK);
            message.Content = new StringContent(viewStr);
            return message;
        }

        private static List<string> _logList = new List<string>();

        public void WriteLog(string log)
        {
            var logStr = string.Format("[{0}] {1}", DateTime.Now.ToString("MM/dd/ HH:mm:ss"), log);
            _logList.Add(logStr);
            
        }
        
    }
}