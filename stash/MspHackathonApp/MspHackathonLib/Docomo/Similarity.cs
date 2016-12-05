using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DocomoAPISamples.API
{
    /// <summary>
    /// 類似度検出を行うAPIを呼び出すクラス
    /// </summary>
    public class Similarity
    {
        private string APIKey;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="key">Docomo API のキー</param>
        public Similarity(string key)
        {
            APIKey = key;
        }

        /// <summary>
        /// 語句間の類似度を検出する
        /// </summary>
        /// <param name="sentence1">語句1</param>
        /// <param name="sentence2">語句2</param>
        /// <returns>類似度(0~1)</returns>
        public async Task<double> ExecAsync(String sentence1, String sentence2)
        {
            var client = new HttpClient();

            //語句類似度のエンドポイントURL
            var uri = "https://api.apigw.smt.docomo.ne.jp/gooLanguageAnalysis/v1/similarity?APIKEY=" + APIKey;


            //要求するメッセージを作成
            dynamic request = new
            {
                query_pair = new string[] {
                    sentence1, sentence2
                }
            };

            //JSONへ変換（Newtonsoft Json.NETライブラリ使用）
            string json = JsonConvert.SerializeObject(request);

            //要求を送信
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

            //エラー確認
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                string err_resp = await response.Content.ReadAsStringAsync();
                throw new Exception("ERROR" + err_resp);
            }

            //サーバーからの応答(Json)を文字列として取り出す
            string response_json = await response.Content.ReadAsStringAsync();

            //jsonをパース（Newtonsoft Json.NETライブラリ使用）
            var result = JsonConvert.DeserializeObject<dynamic>(response_json);

            //類似度を取り出す
            return result["score"];
        }
    }
}
