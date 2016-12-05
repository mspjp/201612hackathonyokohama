using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BotLibrary.Docomo
{
    /// <summary>
    /// 形態素解析を行うAPIを呼び出すクラス
    /// </summary>
    public class Morph
    {
        /// <summary>
        /// 解析対象を示す列挙型
        /// </summary>
        [Flags]
        public enum InfoFilter
        {
            /// <summary>
            /// 表記
            /// </summary>
            FORM = 0x01,
            /// <summary>
            /// 形態素
            /// </summary>
            POS = 0x02,
            /// <summary>
            /// 読み
            /// </summary>
            READ = 0x04
        }

        /// <summary>
        /// 解析結果を含む構造体
        /// </summary>
        public struct MorphResultSet
        {
            /// <summary>
            /// 表記
            /// </summary>
            public string Form;
            /// <summary>
            /// 形態素
            /// </summary>
            public string Pos;
            /// <summary>
            /// 読み
            /// </summary>
            public string Read;
        }

        private string APIKey;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="key">Docomo API のキー</param>
        public Morph(string key)
        {
            APIKey = key;
        }

        /// <summary>
        /// 形態素解析を行う
        /// </summary>
        /// <param name="sentence">解析を行う文章（複数の文章を含んでもOK）</param>
        /// <param name="intoFilter">形態素情報フィルタ　取得する情報の種類（複数指定可）</param>
        /// <param name="posFilter">形態素品詞フィルタ（指定したものだけ検出　何も指定しない場合は全種類）</param>
        /// <returns>1文に含まれる解析結果を格納したリストのリスト</returns>
        public async Task<List<List<MorphResultSet>>> ExecAsync(String sentence, 
            InfoFilter intoFilter = InfoFilter.FORM | InfoFilter.POS | InfoFilter.READ,
            string[] posFilter = null)
        {
            var client = new HttpClient();

            //形態素解析のエンドポイントURL
            var uri = "https://api.apigw.smt.docomo.ne.jp/gooLanguageAnalysis/v1/morph?APIKEY=" + APIKey;

            //形態素情報フィルタのパラメータ作成
            string info_filter = "";
            if(intoFilter.HasFlag(InfoFilter.FORM))
            {
                info_filter += "form|";
            }
            if (intoFilter.HasFlag(InfoFilter.POS))
            {
                info_filter += "pos|";
            }
            if (intoFilter.HasFlag(InfoFilter.READ))
            {
                info_filter += "read|";
            }
            info_filter.TrimEnd('|');

            //要求するメッセージを作成
            var request = new Dictionary<string, string>();
            request.Add("sentence", sentence);
            request.Add("info_filter", info_filter);

            //形態素品詞フィルタのパラメーターを作成(|区切り)
            if(posFilter != null)
            {
                var pos_filter = "";
                foreach(var s in posFilter)
                {
                    if (string.IsNullOrWhiteSpace(s))
                        continue;
                    pos_filter += s + "|";
                }
                pos_filter.TrimEnd('|');

                if (!string.IsNullOrWhiteSpace(pos_filter))
                    request.Add("pos_filter", pos_filter);
            }


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
            JObject result_object = JObject.Parse(response_json);
            dynamic result = result_object["word_list"];

            //応答から配列に変換
            var return_value = new List<List<MorphResultSet>>();
            foreach(var w in result)
            {
                var morph_list = new List<MorphResultSet>();
                foreach(var l in w)
                {
                    MorphResultSet e = new MorphResultSet();
                    int i = 0;
                    if (intoFilter.HasFlag(InfoFilter.FORM))
                    {
                        e.Form = l[i];
                        i++;
                    }
                    if (intoFilter.HasFlag(InfoFilter.POS))
                    {
                        e.Pos = l[i];
                        i++;
                    }
                    if (intoFilter.HasFlag(InfoFilter.READ))
                    {
                        e.Read = l[i];
                        i++;
                    }
                    morph_list.Add(e);
                }
                return_value.Add(morph_list);
            }
            
            return return_value;
        }

    }
}
