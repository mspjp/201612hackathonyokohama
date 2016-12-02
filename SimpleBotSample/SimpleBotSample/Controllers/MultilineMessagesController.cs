using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System.Web.Services.Description;

namespace SimpleBotSample
{
    [BotAuthentication]
    public class MultilineMessagesController : ApiController
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

        public async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            // 複数も可能?
            await connector.Conversations.ReplyToActivityAsync(activity.CreateReply("u can use "));   
            await connector.Conversations.ReplyToActivityAsync(activity.CreateReply("multi line sentence."));
            await
                connector.Conversations.ReplyToActivityAsync(
                    activity.CreateReply("[poptepipic](https://pbs.twimg.com/media/CTBnrdoUcAACGqL.jpg)"));

            var response = Request.CreateResponse(HttpStatusCode.OK);          //   なくしたければTaskの引数を消すこと
            return response;
        }
        
                                                                                   
    }
}