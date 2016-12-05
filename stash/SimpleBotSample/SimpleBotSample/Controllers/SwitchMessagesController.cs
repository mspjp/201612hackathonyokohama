using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;

namespace SimpleBotSample
{
    [BotAuthentication]
    public class SwitchMessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>

        // 忘れてはいけないこと
        // 1:メソッドは基本Postとすること(ここを見てくれる)
        // 2:Post時の処理を変えたいならコントローラをもう1個作ってエンドポイントとすること
        // 2.1:その時はこのコントロラーの中身をCopy & Pasteすると楽
        // 3:Azureに乗っけるときはWeb.Configの3つを埋めること(BotId = BotHandle)
        

        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                var replyText = "";

                if (activity.Text.Contains("hoge"))
                    replyText += $"fuga\n";
                else if (activity.Text.Contains("眠い"))
                    replyText += $"本当に眠いよねー\n";
                else if (activity.Text.Contains("復唱して"))
                    replyText = "はい " + activity.Text;
                else
                    replyText = "![poptepipic](https://pbs.twimg.com/media/CTBnrdoUcAACGqL.jpg)";

                // 返信データの作成(replyTextの中身(string)を送る準備)
                var reply = activity.CreateReply(replyText);

                // 送信
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}