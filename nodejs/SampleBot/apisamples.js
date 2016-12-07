var cognitive = require('./cognitive');
var fs = require('fs');

var apikeyPath = './apikey.json';
var API_KEY = JSON.parse(fs.readFileSync(apikeyPath,'utf8'));

var Docomo = require("./docomoapi.js")
var docomo = new Docomo(API_KEY.DOCOMO_APIKEY);

function showDescription(){
    console.log("");
    console.log("MSPハッカソンのテストプログラムです");
    console.log("試したいプログラムの数字を入力してください");
    console.log("形態素解析: 1");
    console.log("ひらがな変換: 2");
    console.log("要素抽出: 3");
    console.log("文章類似度計算: 4");
    console.log("顔検出: 5");
    console.log("終了: 999");
    console.log(":");
}

showDescription();

process.stdin.setEncoding('utf-8');
process.stdin.on('data', function (data) {
    if(data.indexOf("999")!=-1){
        process.exit();
    }else if(data.indexOf("1")!=-1){
      docomo.morph('今日の天気は、晴のち曇。もしくは雨か雪。', 'form|pos|read', '', function(r){
        console.log('今日の天気は、晴のち曇。もしくは雨か雪。:',r);
      });
      docomo.morph('猫のキティーが膝の上に乗っている', 'form|pos', '名詞|動詞語幹', function(r){
        console.log('猫のキティーが膝の上に乗っている:',r);
      });
    }else if(data.indexOf("2")!=-1){
      docomo.hiragana('こにゃにゃちわ〜', 'katakana', function(r){
        console.log('こにゃにゃちわ〜:',r);
      });
      docomo.hiragana('魑魅魍魎', 'hiragana', function(r){
        console.log('魑魅魍魎:',r);
      });
      docomo.hiragana('Step By Step システム構築ガイド', 'hiragana', function(r){
        console.log('Step By Step システム構築ガイド:',r);
      });
    }else if(data.indexOf("3")!=-1){
      docomo.entity('水野氏が設計した日本株式会社 東京本社の完成記念式典を10月10日　20:15から行う', null, function(r){
        console.log('「水野氏が設計した日本株式会社 東京本社の完成記念式典を10月10日　20:15から行う」 固有表現抽出:',r);
      });
      docomo.entity('横浜ハッカソン@10月10日', 'DAT', function(r){
        console.log('横浜ハッカソン@10月10日:',r);
      });
      docomo.entity('横浜ハッカソン@10月10日', null, function(r){
        console.log('横浜ハッカソン@10月10日(ALL):',r);
      });
    }else if(data.indexOf("4")!=-1){
      docomo.similarity('Tsukuba', 'つくば', function(s){
        if(s == -1)console.log('エラー');
        else console.log('Tsukuba-つくば:類似度:', s);
      });
      docomo.similarity('yokkaichi', '四日市', function(s){
        console.log('yokkaichi-四日市:類似度:', s);
      });
    }else if(data.indexOf("5")!=-1){
        var image = "http://www.appbank.net/wp-content/uploads/2016/10/koidance-1.jpg";
        cognitive.faceDetect(API_KEY.FACE_APIKEY,image,true,true,"smile",function(error,response,body){
            if (!error && response.statusCode == 200) {
                console.log(body);
            } else {
                console.log('error: '+ JSON.stringify(response));
            }
        });

    }else{
        console.log("認識していないコマンドです "+data);
    }

    showDescription();
});
