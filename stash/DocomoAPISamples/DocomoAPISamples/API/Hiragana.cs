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
    /// ひらがな、カタカナ化に変換するAPIを呼び出すクラス
    /// </summary>
    public class Hiragana
    {
        /// <summary>
        /// 出力形式
        /// </summary>
        public enum OutputType
        {
            /// <summary>
            /// ひらがな
            /// </summary>
            HIRAGANA,
            /// <summary>
            /// カタカナ
            /// </summary>
            KATAKANA
        }

        private string APIKey;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="key">Docomo API のキー</param>
        public Hiragana(string key)
        {
            APIKey = key;
        }

        /// <summary>
        /// ひらがな、カタカナ化を行う
        /// </summary>
        /// <param name="sentence">漢字混ざりの文字列</param>
        /// <param name="mode">出力形式</param>
        /// <returns>変換後の文字列（ひらがな、もしくはカタカナ）</returns>
        public async Task<string> ExecAsync(String sentence, OutputType mode)
        {
            var client = new HttpClient();

            //ひらがな、カタカナ化のエンドポイントURL
            var uri = "https://api.apigw.smt.docomo.ne.jp/gooLanguageAnalysis/v1/hiragana?APIKEY="+APIKey;

            string output_type = "hiragana";
            if (mode == OutputType.KATAKANA)
                output_type = "katakana";


            //要求するメッセージを作成
            dynamic request = new
            {
                sentence = sentence,
                output_type =  output_type
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

            //変換結果を取り出す（convertedに変換結果が入っている）
            return result["converted"];
        }

    }
}
