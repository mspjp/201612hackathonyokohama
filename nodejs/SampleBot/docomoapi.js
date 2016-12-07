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
