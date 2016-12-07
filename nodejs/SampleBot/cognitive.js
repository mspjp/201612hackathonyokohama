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
    }
};