var request = require('request');

function post(apiKey,endpoint,body,callBack){
    var options = {
        uri: endpoint,
        headers:{
            'Ocp-Apim-Subscription-Key':apiKey,
            'Content-Type':'application/json'
        },
        body: body,
        json: true
    };

    return request.post(options,callBack);
}

function get(apiKey,endpoint,callBack){
    var options = {
        uri: endpoint,
        headers:{
            'Ocp-Apim-Subscription-Key':apiKey,
            'Content-Type':'application/json'
        },
        json: true
    };

    return request.get(options,callBack);
}
    

module.exports = {
    //顔検出
    //ドキュメント https://dev.projectoxford.ai/docs/services/563879b61984550e40cbbe8d/operations/563879b61984550f30395236
    //apiKey:FaceAPIのAPIKey
    //url:検出したい顔写真のURL
    //returnFaceId:顔のID。複数顔を認識した場合などに有効(例:true)
    //returnFaceLandmarks:顔のパーツ座標を返すかどうか(例:true)
    //returnFaceAttributes:顔認識の様々なオプション(例:age, gender, smile, glasses)
    faceDetect : function(apiKey,url,returnFaceId,returnFaceLandmarks,returnFaceAttributes,callBack){
        var endpoint = "https://api.projectoxford.ai/face/v1.0/detect";
        endpoint += "?returnFaceId="+returnFaceId;
        endpoint += "&returnFaceLandmarks="+returnFaceLandmarks;
        endpoint += "&returnFaceAttributes="+returnFaceAttributes;

        var body = {
            "url":url
        };
        post(apiKey,endpoint,body,callBack);
    },

    //感情抽出
    //ドキュメント https://dev.projectoxford.ai/docs/services/5639d931ca73072154c1ce89/operations/563b31ea778daf121cc3a5fa
    //apiKey:EmotionAPIのAPIKey
    //url:感情を抽出したい顔写真のURL
    emotion : function(apiKey,url,callBack){
        var endpoint = "https://api.projectoxford.ai/emotion/v1.0/recognize";
        
        var body = {
            "url":url
        };
        post(apiKey,endpoint,body,callBack);
    },

    //風景画像から物体抽出
    //ドキュメント https://dev.projectoxford.ai/docs/services/56f91f2d778daf23d8ec6739/operations/56f91f2e778daf14a499e1fa
    //apiKey:ComputerVisionAPIのAPIKey
    //url:物体抽出したい風景のURL
    computerVision : function(apiKey,url,callBack){
        var endpoint = "https://api.projectoxford.ai/vision/v1.0/analyze";
        
        var body = {
            "url":url
        };
        post(apiKey,endpoint,body,callBack);
    },
    
    //Bing検索
    //ドキュメント https://dev.cognitive.microsoft.com/docs/services/56b43eeccf5ff8098cef3807/operations/56b4447dcf5ff8098cef380d
    //apiKey: bing searchのapikey
    //query: 検索クエリ
    //count: 何件検索するか
    //offset: 検索結果を最初から何件飛ばすか
    bingSearch : function(apiKey,query,count,offset,callBack){
        var endpoint = "https://api.cognitive.microsoft.com/bing/v5.0/search";
        endpoint += "?q="+encodeURIComponent(query);
        endpoint += "&count="+count;
        endpoint += "&offset="+offset;
        endpoint += "&mkt=ja-jp";
        endpoint += "&safesearch=Moderate";

        get(apiKey,endpoint,callBack);
    }
};