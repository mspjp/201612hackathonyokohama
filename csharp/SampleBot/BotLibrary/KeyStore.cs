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
    public class KeyStore
    {
        public Dictionary<string, string> Keys { get; set; }
        private KeyStore()
        {
            Keys = new Dictionary<string, string>();
            Keys.Add("FACE_APIKEY",ApiKey.FACE_APIKEY);
            Keys.Add("DOCOMO_APIKEY",ApiKey.DOCOMO_APIKEY);
        }

        private static KeyStore _instance;

        public static KeyStore Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new KeyStore();

                return _instance;
            }
        }
    }
}
