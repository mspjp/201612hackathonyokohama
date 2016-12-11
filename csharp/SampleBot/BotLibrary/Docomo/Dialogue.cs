using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLibrary.Docomo {
    public class Dialogue {
        public enum Sex {
            MALE, FEMALE
        }
        public enum BloodType {
            A, B, AB, O
        }
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

        public struct UserInfo {
            public string NickName;
            public string NickNameYomi;
            public Sex Sex;
            public BloodType BloodType;
            public DateTime BirthDay;
            public int Age;
            public Constellations Constellations;
            public string Place;

        }
        public struct Area {
            public readonly string Name;
            public readonly string[] Places;
            public Area(string n, string[] p) {
                Name = n;
                Places = p;
            }
        }

        public static Dictionary<string, Area> Areas;
        public static string[] AreaNames;
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


        public Dialogue() {
            UserInfo i;
        }
    }
}
