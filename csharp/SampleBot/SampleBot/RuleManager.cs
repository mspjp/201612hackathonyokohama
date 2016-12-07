using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SampleBot
{
    public class Rule
    {
        public string Pattern { get; set; }
        public List<string> Responces { get; set; } = new List<string>();
    }
    
    public class RuleManager
    {
        List<Rule> _rules;
        Random _rand;
        private RuleManager(List<Rule> rules)
        {
            this._rules = rules;

            _rand = new Random(DateTime.Now.Millisecond);
        }

        public List<string> SearchResponses(string utterance)
        {
            var responses = new List<string>();
            foreach(var rule in _rules)
            {
                if (Regex.IsMatch(utterance,rule.Pattern))
                {
                    if(rule.Responces.Count == 1)
                    {
                        responses.Add(rule.Responces.First());
                    }else
                    {
                        int pick = _rand.Next(rule.Responces.Count);
                        responses.Add(rule.Responces.ElementAt(pick));
                    }
                }
            }
            return responses;
        }

        private static RuleManager _instance;

        public static RuleManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    List<Rule> rules = new List<Rule>();
                    using (var reader = new StreamReader(HttpContext.Current.Server.MapPath("~/rule.csv")))
                    {
                        string line = "";
                        while ((line = reader.ReadLine()) != null)
                        {
                            var rule = new Rule();
                            string[] cols = line.Split(',');
                            rule.Pattern = cols[0];
                            string[] subCols = cols[1].Split(':');
                            rule.Responces = subCols.ToList();
                            rules.Add(rule);
                        }
                    }

                    _instance = new RuleManager(rules);
                }
                return _instance;
            }
            
        }
    }
}