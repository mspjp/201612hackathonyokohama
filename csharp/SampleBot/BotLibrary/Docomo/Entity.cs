using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
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
    /// 固有表現抽出のAPIを呼び出すクラス
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 固有表現の種類
        /// </summary>
        [Flags]
        public enum ClassType
        {
            /// <summary>
            /// 人工物名
            /// </summary>
            ART = 0x01,
            /// <summary>
            /// 組織名
            /// </summary>
            ORG =0x02,
            /// <summary>
            /// 人名
            /// </summary>
            PSN = 0x04,
            /// <summary>
            /// 地名
            /// </summary>
            LOC = 0x08,
            /// <summary>
            /// 日付表現
            /// </summary>
            DAT = 0x10,
            /// <summary>
            /// 時刻表現
            /// </summary>
            TIM = 0x20,
            ALL = ART | ORG | PSN | LOC | DAT | TIM
        }

        /// <summary>
        /// 固有表現抽出を行った結果を格納する構造体
        /// </summary>
        public struct EntityResultSet
        {
            /// <summary>
            /// 単語
            /// </summary>
            public string Entity;
            /// <summary>
            /// 単語の種類
            /// </summary>
            [JsonConverter(typeof(StringEnumConverter))]
            public ClassType Type;
        }

        private string APIKey;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="key">Docomo API のキー</param>
        public Entity(string key)
        {
            APIKey = key;
        }

        /// <summary>
        /// 固有表現抽出を行う
        /// </summary>
        /// <param name="sentence">抽出を行う文章</param>
        /// <param name="classFilter">抽出を行う固有表現の種類 複数指定可 未指定の場合は全部</param>
        /// <returns>単語と固有表現の種類を含む構造体のリスト</returns>
        public async Task<List<EntityResultSet>> ExecAsync(String sentence, ClassType classFilter = ClassType.ALL)
        {
            var client = new HttpClient();

            //固有表現抽出のエンドポイントURL
            var uri = "https://api.apigw.smt.docomo.ne.jp/gooLanguageAnalysis/v1/entity?APIKEY=" + APIKey;

            //固有表現種類のフィルターのパラメータを生成
            string class_filter = "";
            if (classFilter.HasFlag(ClassType.ART)) class_filter += "ART|";
            if (classFilter.HasFlag(ClassType.DAT)) class_filter += "DAT|";
            if (classFilter.HasFlag(ClassType.LOC)) class_filter += "LOC|";
            if (classFilter.HasFlag(ClassType.ORG)) class_filter += "ORG|";
            if (classFilter.HasFlag(ClassType.PSN)) class_filter += "PSN|";
            if (classFilter.HasFlag(ClassType.TIM)) class_filter += "TIM|";
            class_filter.TrimEnd('|');

            //要求するメッセージを作成
            dynamic request = new
            {
                sentence = sentence,
                class_filter = class_filter
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
            JObject result_object = JObject.Parse(response_json);
            dynamic result = result_object["ne_list"];
            var return_value = new List<EntityResultSet>();

            //変換結果を取り出す
            foreach (var r in result)
            {
                EntityResultSet e = new EntityResultSet();
                e.Entity = r[0];
                e.Type = (ClassType)Enum.Parse(typeof(ClassType), (string)r[1]);
                return_value.Add(e);
            }
            
            return return_value;
        }

    }
}
