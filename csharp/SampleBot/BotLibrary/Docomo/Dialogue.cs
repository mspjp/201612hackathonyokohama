using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BotLibrary.Docomo {
    public class Dialogue {
        /// <summary>
        /// 性別
        /// </summary>
        public enum Sex {
            MALE, FEMALE
        }

        /// <summary>
        /// 血液型
        /// DocomoAPIの引数にそのまま使っているので変更しないでください。
        /// </summary>
        public enum BloodType {
            A, B, AB, O
        }

        /// <summary>
        /// 対話モード
        /// DocomoAPIの引数にそのまま使っているので変更しないでください。
        /// </summary>
        public enum DialogueMode {
            /// <summary>
            /// 対話
            /// </summary>
            DIALOG,
            /// <summary>
            /// しりとり
            /// </summary>
            SRTR
        }
        /// <summary>
        /// 星座を示す列挙型
        /// </summary>
        public enum Constellations {
            /// <summary>
            /// 牡羊座
            /// </summary>
            Aries,
            /// <summary>
            /// 牡牛座
            /// </summary>
            Taurus,
            /// <summary>
            /// 双子座
            /// </summary>
            Gemini,
            /// <summary>
            /// 蟹座
            /// </summary>
            Cancer,
            /// <summary>
            /// 獅子座
            /// </summary>
            Leo,
            /// <summary>
            /// 乙女座
            /// </summary>
            Virgo,
            /// <summary>
            /// 天秤座
            /// </summary>
            Libra,
            /// <summary>
            /// 蠍座
            /// </summary>
            Scorpion,
            /// <summary>
            /// 射手座
            /// </summary>
            Sagittarius,
            /// <summary>
            /// 山羊座
            /// </summary>
            Capricorn,
            /// <summary>
            /// 水瓶座
            /// </summary>
            Aquarius,
            /// <summary>
            /// 魚座
            /// </summary>
            Pisces
        }

        /// <summary>
        /// ユーザーの情報を格納するクラス。対話APIを呼び出す際に必要となります。
        /// </summary>
        public class UserInfo {
            /// <summary>
            /// ニックネーム（10文字以下）
            /// </summary>
            public string NickName;
            /// <summary>
            /// ニックネームの読み（全角カタカナ20文字以下）
            /// </summary>
            public string NickNameYomi;
            /// <summary>
            /// 性別
            /// </summary>
            public Sex Sex;
            /// <summary>
            /// 血液型
            /// </summary>
            public BloodType BloodType;
            /// <summary>
            /// 誕生日
            /// </summary>
            public DateTime BirthDay;
            /// <summary>
            /// 年齢
            /// </summary>
            public int Age;
            /// <summary>
            /// 星座
            /// </summary>
            public Constellations Constellations;
            /// <summary>
            /// 現在地（Areasに含まれているもののみ）
            /// <see cref="https://dev.smt.docomo.ne.jp/?p=docs.api.page&api_name=dialogue&p_name=api_1#tag01"/>
            /// </summary>
            public string Place;

            /// <summary>
            /// 誕生日から年齢に変換
            /// </summary>
            public void ConverBirthDayToAge() {
                Age = DateTime.Today.Year - BirthDay.Year;
                if (DateTime.Today.DayOfYear < BirthDay.DayOfYear)
                    Age--;
            }
            /// <summary>
            /// 誕生日から星座に変換
            /// </summary>
            public void ConvertBirthDayToConstellations() {
                var d = BirthDay.Day;
                var m = BirthDay.Month;
                if (m == 3 && d >= 21 || m == 4 && d <= 20) {
                    Constellations = Constellations.Aries;
                } else if (m == 4 && d >= 21 || m == 5 && d <= 20) {
                    Constellations = Constellations.Taurus;
                } else if (m == 5 && d >= 21 || m == 6 && d <= 21) {
                    Constellations = Constellations.Gemini;
                } else if (m == 6 && d >= 22 || m == 7 && d <= 23) {
                    Constellations = Constellations.Cancer;
                } else if (m == 7 && d >= 24 || m == 8 && d <= 23) {
                    Constellations = Constellations.Leo;
                } else if (m == 8 && d >= 24 || m == 9 && d <= 23) {
                    Constellations = Constellations.Virgo;
                } else if (m == 9 && d >= 24 || m == 10 && d <= 23) {
                    Constellations = Constellations.Libra;
                } else if (m == 10 && d >= 24 || m == 11 && d <= 22) {
                    Constellations = Constellations.Scorpion;
                } else if (m == 11 && d >= 23 || m == 12 && d <= 22) {
                    Constellations = Constellations.Sagittarius;
                } else if (m == 12 && d >= 23 || m == 1 && d <= 20) {
                    Constellations = Constellations.Capricorn;
                } else if (m == 1 && d >= 21 || m == 2 && d <= 19) {
                    Constellations = Constellations.Aquarius;
                } else if (m == 2 && d >= 20 || m == 3 && d <= 20) {
                    Constellations = Constellations.Pisces;
                } else {
                    throw new Exception("something is wrong.");
                }
            }

        }

        /// <summary>
        /// 地域名と場所一覧を含む構造体
        /// </summary>
        public struct Area {
            /// <summary>
            /// 地域名
            /// </summary>
            public readonly string Name;
            /// <summary>
            /// 場所一覧
            /// </summary>
            public readonly string[] Places;
            internal Area(string n, string[] p) {
                Name = n;
                Places = p;
            }
        }

        /// <summary>
        /// 対話APIで利用可能な場所一覧を地域別に格納したもの
        /// </summary>
        public static readonly Dictionary<string, Area> Areas;
        /// <summary>
        /// 地域一覧
        /// </summary>
        public static readonly string[] AreaNames;
        static Dialogue() {
            Areas = new Dictionary<string, Area>();
            Areas["北海道"] = new Area("北海道", new string[] { "稚内", "旭川", "留萌", "網走", "北見", "紋別", "根室", "釧路", "帯広", "室蘭", "浦河", "札幌", "岩見沢", "倶知安", "函館", "江差" });
            Areas["東北"] = new Area("東北", new string[] { "青森", "弘前", "深浦", "むつ", "八戸", "秋田", "横手", "鷹巣", "盛岡", "二戸", "一関", "宮古", "大船渡", "山形", "米沢", "酒田", "新庄", "仙台", "古川", "石巻", "白石", "福島", "郡山", "白河", "小名浜", "相馬", "若松", "田島" });
            Areas["関東甲信越"] = new Area("関東甲信越", new string[] { "宇都宮", "大田原", "水戸", "土浦", "前橋", "みなかみ", "さいたま", "熊谷", "秩父", "東京", "大島", "八丈島", "父島", "千葉", "銚子", "館山", "横浜", "小田原", "甲府", "河口湖", "長野", "松本", "諏訪", "軽井沢", "飯田", "新潟", "津川", "長岡", "湯沢", "高田", "相川" });
            Areas["中部"] = new Area("中部", new string[] { "静岡", "網代", "石廊崎", "三島", "浜松", "御前崎", "富山", "伏木", "岐阜", "高山", "名古屋", "豊橋", "福井", "大野", "敦賀", "金沢", "輪島" });
            Areas["関西"] = new Area("関西", new string[] { "大津", "彦根", "津", "上野", "四日市", "尾鷲", "京都", "舞鶴", "奈良", "風屋", "和歌山", "潮岬", "大阪", "神戸", "姫路", "洲本", "豊岡" });
            Areas["中国・四国"] = new Area("中国・四国", new string[] { "鳥取", "米子", "岡山", "津山", "松江", "浜田", "西郷", "広島", "呉", "福山", "庄原", "下関", "山口", "柳井", "萩", "高松", "徳島", "池田", "日和佐", "松山", "新居浜", "宇和島", "高知", "室戸岬", "清水" });
            Areas["九州・沖縄"] = new Area("九州・沖縄", new string[] { "福岡", "八幡", "飯塚", "久留米", "佐賀", "伊万里", "長崎", "佐世保", "厳原", "福江", "大分", "中津", "日田", "佐伯", "熊本", "阿蘇乙姫", "牛深", "人吉", "宮崎", "油津", "延岡", "都城", "高千穂", "鹿児島", "阿久根", "枕崎", "鹿屋", "種子島", "名瀬", "沖永良部", "那覇", "名護", "久米島", "南大東島", "宮古島", "石垣島", "与那国島" });
            Areas["その他"] = new Area("その他", new string[] { "海外" });
            AreaNames = new string[] { "北海道", "東北" , "関東甲信越" , "中部" , "関西" , "中国・四国" , "九州・沖縄" , "その他" };
        }

        /// <summary>
        /// 対話APIのシステムが返した応答を格納する構造体
        /// </summary>
        public struct ResultSet {
            /// <summary>
            /// システムの応答文章
            /// </summary>
            public string Result;
            /// <summary>
            /// システムの応答文章のよみがな（漢字部分はカタカナ）
            /// </summary>
            public string ResultYomi;
            /// <summary>
            /// 対話を続けるために必要なID
            /// </summary>
            public string ID;
        }

        private string APIKey;

        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="key">Docomo API のキー</param>
        public Dialogue(string key) {
            APIKey = key;
        }

        
        /// <summary>
        /// 対話をおこなうAPI
        /// </summary>
        /// <param name="sentence">ユーザーの発話（255文字以下）</param>
        /// <param name="user">ユーザーの情報（オプション）</param>
        /// <param name="mode">対話モード（対話・しりとり）</param>
        /// <param name="id">対話ID（対話を続ける場合はResultSetに含まれるidを渡してください</param>
        /// <param name="characterId">0:デフォルト 20:関西風 30:赤ちゃん風</param>
        /// <returns>システムからの応答</returns>
        public async Task<ResultSet> ExecAsync(String sentence, UserInfo user = null, DialogueMode mode = DialogueMode.DIALOG, String id = null, int characterId = 0) {
            var client = new HttpClient();

            //対話APIのエンドポイントURL
            var uri = "https://api.apigw.smt.docomo.ne.jp/dialogue/v1/dialogue?APIKEY=" + APIKey;


            //要求するメッセージを作成
            Dictionary<string, object> request = new Dictionary<string, object>();
            request["utt"] = sentence;
            request["context"] = id;
            if (user != null) {
                request["nickname"] = user.NickName;
                request["nickname_y"] = user.NickNameYomi;
                request["sex"] = user.Sex.ToJapanese();
                request["bloodtype"] = user.BloodType.ToString();
                request["birthdateY"] = user.BirthDay.Year;
                request["birthdateM"] = user.BirthDay.Month;
                request["birthdateD"] = user.BirthDay.Day;
                request["age"] = user.Age;
                request["constellations"] = user.Constellations.ToJapanese();
                request["place"] = user.Place;
            }
            request["mode"] = mode.ToString().ToLower();
            request["t"] = characterId;

            //JSONへ変換（Newtonsoft Json.NETライブラリ使用）
            string json = JsonConvert.SerializeObject(request);

            //要求を送信
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

            //エラー確認
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                string err_resp = await response.Content.ReadAsStringAsync();
                throw new Exception("ERROR" + err_resp);
            }

            //サーバーからの応答(Json)を文字列として取り出す
            string response_json = await response.Content.ReadAsStringAsync();
            //jsonをパース（Newtonsoft Json.NETライブラリ使用）
            var result = JsonConvert.DeserializeObject<dynamic>(response_json);

            //変換結果を取り出す（convertedに変換結果が入っている）
            ResultSet r = new ResultSet();
            r.Result = result["utt"];
            r.ResultYomi = result["yomi"];
            r.ID = result["context"];
            return r;
        }
    }

    /// <summary>
    /// 性別を扱うSex列挙型を日本語に変換する拡張メソッド
    /// </summary>
    //DocomoのAPIを呼び出す際に利用しているので変換後の文字列を変更しないでください。
    public static class SexExt {
        /// <summary>
        /// 日本語に変換します
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string ToJapanese(this Dialogue.Sex s) {
            switch (s) {
                case Dialogue.Sex.FEMALE:
                    return "女";
                case Dialogue.Sex.MALE:
                    return "男";
            }
            return "";
        }
    }

    /// <summary>
    /// 星座を扱うConstellations列挙型を日本語に変換する拡張メソッド
    /// </summary>
    //DocomoのAPIを呼び出す際に利用しているので変換後の文字列を変更しないでください。
    public static class ConstellationsExt {
        /// <summary>
        /// 日本語に変換します
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string ToJapanese(this Dialogue.Constellations c) {
            switch (c) {
                case Dialogue.Constellations.Aries:
                    return "牡羊座";
                case Dialogue.Constellations.Taurus:
                    return "牡牛座";
                case Dialogue.Constellations.Gemini:
                    return "双子座";
                case Dialogue.Constellations.Cancer:
                    return "蟹座";
                case Dialogue.Constellations.Leo:
                    return "獅子座";
                case Dialogue.Constellations.Virgo:
                    return "乙女座";
                case Dialogue.Constellations.Libra:
                    return "天秤座";
                case Dialogue.Constellations.Scorpion:
                    return "蠍座";
                case Dialogue.Constellations.Sagittarius:
                    return "射手座";
                case Dialogue.Constellations.Capricorn:
                    return "山羊座";
                case Dialogue.Constellations.Aquarius:
                    return "水瓶座";
                case Dialogue.Constellations.Pisces:
                    return "魚座";

            }
            return "";
        }
    }
}
