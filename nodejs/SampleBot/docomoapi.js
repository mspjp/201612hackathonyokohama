var request = require('request');
var _api_call  = function (key, arg, url, callback){
  var options = {
    uri: url + '?APIKEY=' + key,
    method: 'POST',
    json: arg,
    'content-type': 'application/json'
  };
  request(options, function(error, response, body){
    if (!error && response.statusCode == 200) {
      callback(body);
    } else {
      console.log('error: '+ response.statusCode, body);
      callback();
    }
  });
}

var client = function(apiKey){
    this.apiKey = apiKey;

    //類似度算出
    //ドキュメント: https://dev.smt.docomo.ne.jp/?p=docs.api.page&api_name=language_analysis&p_name=api_3
    //str1: 文字列1(例:キーワード)
    //str2: 文字列2(例:KeyWord)
    //callback :
    //  function(r):
    //    r:成功時:0~1(類似度)
    //      エラー時:-1
    this.similarity = function (str1, str2, callback) {
      var result = _api_call(apiKey, {
        query_pair:[str1, str2]
      }, 'https://api.apigw.smt.docomo.ne.jp/gooLanguageAnalysis/v1/similarity',
      function (body){
        if(body){
          callback(body.score);
        }else{
          callback(-1);
        }
      });
    };

    //固有表現抽出
    //ドキュメント: https://dev.smt.docomo.ne.jp/?p=docs.api.page&api_name=language_analysis&p_name=api_2
    //sentence: 文字列 (例:海老名SAで10時に)
    //classFilter: 抽出する固有表現のフィルタ　未指定の場合は全種類を抽出 複数の場合は|区切り(例:LOC|TIM)
    // 指定する種類は以下のとおりです
    // 人工物名:ART 組織名:ORG 人名:PSN 地名:LOC 日付表現:DAT 時刻表現:TIM
    //callback :
    //  function(r):
    //    r:成功時:[[固有表現, 固有表現の種類]]  (例:[[海老名SA, LOC], [10時, TIM]])
    //      エラー時:-1
    this.entity = function (sentence, classFilter, callback) {
      if(!classFilter){
        classFilter = 'ART|DAT|LOC|ORG|PSN|TIM';
      }
      var result = _api_call(apiKey, {
        sentence:sentence,
        class_filter:classFilter
      }, 'https://api.apigw.smt.docomo.ne.jp/gooLanguageAnalysis/v1/entity',
      function (body){
        if(body){
          callback(body.ne_list);
        }else{
          callback(-1);
        }
      });
    };

    //ひらがな、カタカナ化
    //ドキュメント: https://dev.smt.docomo.ne.jp/?p=docs.api.page&api_name=language_analysis&p_name=api_4
    //sentence: 文字列 (例:漢字の文章)
    //mode: 変換モード hiragana もしくは katakana (例:hiragana)
    //callback :
    //  function(r):
    //    r:成功時:変換結果 (例:かんじの ぶんしょう)
    //      エラー時:-1
    this.hiragana = function (sentence, mode, callback) {
      var result = _api_call(apiKey, {
        sentence:sentence,
        output_type: mode
      }, 'https://api.apigw.smt.docomo.ne.jp/gooLanguageAnalysis/v1/hiragana',
      function (body){
        if(body){
          callback(body.converted);
        }else{
          callback(-1);
        }
      });
    };

    //形態素解析
    //ドキュメント: https://dev.smt.docomo.ne.jp/?p=docs.api.page&api_name=language_analysis&p_name=api_1
    //sentence: 文字列 (例:走る猫。追いかける人。)
    //intoFilter: 形態素情報フィルタ　未指定の場合はすべて 複数指定する場合は|区切り(例:form|pos)
    // 指定できるフィルタは以下のとおりです。
    // 表記:form 形態素:pos 読み:read
    //posFilter: 形態素品詞フィルタ 未指定の場合はすべて 複数指定する場合は|区切り(例:動詞語幹|名詞)
    //callback :
    //  function(r):
    //    r:成功時:[[[形態素フィルタで指定した要素（順番は表記、形態素、読みの順のようです）]]]
    //        (例:[ [["走","動詞語幹"],["猫","名詞"]] , [["追","動詞語幹"],["人","名詞"]] ])
    //      エラー時:-1
    this.morph = function (sentence, intoFilter, posFilter, callback) {
      var result = _api_call(apiKey, {
        sentence:sentence,
        info_filter: intoFilter,
        pos_filter:posFilter
      }, 'https://api.apigw.smt.docomo.ne.jp/gooLanguageAnalysis/v1/morph',
      function (body){
        if(body){
          callback(body.word_list);
        }else{
          callback(-1);
        }
      });
    };
}

module.exports = client;
