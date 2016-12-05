var DocomoApi = require("./docomoapi.js");
var docomoAPiKey = "hogehogehogege";
var docomo = new DocomoApi(docomoAPiKey);

function showDescription(){
    console.log("");
    console.log("MSPハッカソンのテストプログラムです");
    console.log("試したいプログラムの数字を入力してください");
    console.log("形態素解析: 1");
    console.log("ひらがな変換: 2");
    console.log("要素抽出: 3");
    console.log("文章類似度計算: 4");
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
    }else{
        console.log("認識していないコマンドです "+data);
    }

    showDescription();
});