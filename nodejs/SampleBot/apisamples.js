var cognitive = require('./cognitive');
var fs = require('fs');

var apikeyPath = './apikey.json';
var API_KEY = JSON.parse(fs.readFileSync(apikeyPath,'utf8'));

var docomo = require("./docomoapi.js")(API_KEY.DOCOMO_APIKEY);

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
        //形態素解析
        docomo.morph();
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