using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLibrary
{
    public class ApiKey
    {
        public Dictionary<string, string> Keys { get; set; }
        private ApiKey()
        {
            Keys = new Dictionary<string, string>();
            Keys.Add("FACE_APIKEY",Properties.Resources.FACE_APIKEY);
            Keys.Add("DOCOMO_APIKEY", Properties.Resources.DOCOMO_APIKEY);
        }

        private static ApiKey _instance;

        public static ApiKey Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ApiKey();

                return _instance;
            }
        }
    }
}
