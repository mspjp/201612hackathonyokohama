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
using Microsoft.ProjectOxford.Face;

namespace SampleBot
{
    /**************************************************************
     * Botを操作するときはこのファイルを編集します。
     * 詳しくはTutorialを参照 https://github.com/mspjp/201612hackathonyokohama/blob/master/csharp/README.md
     **************************************************************/

    [BotAuthentication]
    public class MessagesController : ApiController
    {
        //Botにメッセージがくるとここが呼び出される
        //  第一引数 connector: 発話を返す時に使うもの
        //  第二引数 message: 発話の内容が入っている
        private async Task OnMessageAsync(ConnectorClient connector,Activity message)
        {
            //rule.csvに書いてあるルールとマッチングを行う
            //マッチしたルールが2つ以上あると最初のルールが選択される
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
                break;
            }

            //発話のテキスト
            var text = message.Text;

            /**************************************************************
             * コピペゾーン1: 発話が来たときに通るゾーン
             **************************************************************/

             //マッチするルールがないなら
            if (responses.Count == 0)
            {
                /**************************************************************
                 * コピペゾーン2: 用意したルールに何もマッチングしなかった発話が来た時に通るゾーン
                 **************************************************************/

                var reply = message.CreateReply("今日はいい天気ですね！");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }

            //アタッチメント(画像など)が添付されているとこの中が実行される
            if (message.Attachments != null && message.Attachments.Count > 0)
            {
                //アタッチメントのURL
                var image = message.Attachments.First().ContentUrl;

                /**************************************************************
                 * コピペゾーン3: 発話がきて、アタッチメント(画像)が添付されていた時に通るゾーン
                 **************************************************************/

                //Face APIを呼び出す
                var client = new FaceServiceClient(ApiKey.FACE_APIKEY);
                var faces = await client.DetectAsync(image, true, false, new List<FaceAttributeType>()
                {
                    FaceAttributeType.Age,
                    FaceAttributeType.Smile
                });

                if (faces.Count() == 0)
                {
                    var reply = message.CreateReply("顔が検出できませんでした");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
                else
                {
                    var reply = message.CreateReply("素敵なお顔ですね！ " + faces.First().FaceAttributes.Age + "歳ですか？");
                    await connector.Conversations.ReplyToActivityAsync(reply);
                }
            }

            
        }

        //ルールに{command}が入っていればここが呼び出される
        //  第一引数 connector: 発話を返す時に使うもの
        //  第二引数 message: 発話の内容が入っている
        //  第三引数 command: コマンドの名前が入っている  
        private async Task OnCommandAsync(ConnectorClient connector, Activity message, string command)
        {
            /**************************************************************
             * コピペゾーン4: 発話がルールにマッチして、ルールに{command}が書いてあった時に通るゾーン
             **************************************************************/

            if (command == "command1")
            {
                var reply = message.CreateReply("execute " + command);
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
        }


        /**************************************************************
         * ここから下は特に気にしなくても大丈夫です
         **************************************************************/


        //Bot Connectorからここが呼び出される
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            try
            {
                if (activity.Type == "message")
                {
                    await OnMessageAsync(connector, activity);
                }
            }catch(Exception e)
            {
                //何かエラーが発生するとログに記録してエラー文を出す
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