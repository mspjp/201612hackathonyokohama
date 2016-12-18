# コピペ用サンプルコード集(node.js)

- 発話テキストが入っている変数名: **text**
- 画像のURLが入っている変数名: **image**

- FaceAPIKeyの変数名: **API_KEY.FACE_APIKEY**
- EmotionAPIKeyの変数名: **API_KEY.EMOTION_APIKEY**
- ComputerVisionApiKeyの変数名: **API_KEY.COMPUTER_VISION_APIKEY**
- BingSearchAPIの変数名: **API_KEY.BING_SEARCH_APIKEY**
- docomoAPIKeyの変数名: **API_KEY.DOCOMO_APIKEY**

で統一お願いします。

## Bot発話文取得

コピペゾーン: ゾーン1～4

```js
var text = session.message.text;
```

## Botに発話させる

コピペゾーン: ゾーン1～4

```js
session.send("こんにちは！");
```

## Docomo API

### 形態素解析

機能: 文章から形態素(意味のあるまとまり)に分割する

コピペゾーン:コピペゾーン1

コピペ用コード

```js
//text変数にBotからの発話を入れておく
docomo.morph(text, 'form|pos|read', '', function(r){
    // 解析結果がrに格納される
    // 形式は[1文ごと[1要素ごと[形態素フィルタで指定した要素（順番は表記、形態素、読みの順のようです）]]])
    // 例 [ [["走","動詞語幹", "ハシ"],["猫","名詞", "ネコ"]] , [["追","動詞語幹", "オウ"],["人","名詞", "ヒト"]] ]
    console.log(r);
});
```

### 漢字ひらがな変換

機能: 漢字の入っている文章をひらがなに変換する

コピペゾーン:コピペゾーン1

コピペ用コード

```js
//text変数にBotからの発話を入れておく
docomo.hiragana(text, 'hiragana', function(r){
    // 変換結果の文字列が格納される(ひらがなモードなのでひらがな)
    console.log(r);
});
```

### 要素抽出

機能:文章の中から、人名、地名、日付などの要素を抽出する

コピペゾーン:コピペゾーン1

コピペ用コード

```js
//text変数にBotからの発話を入れておく
//DATは日付だけを抽出することを示す
docomo.entity(text, 'DAT', function(r){
    //抽出結果
    // 形式:[[固有表現, 固有表現の種類]] 
    // 例:[[10月10日, DAT], [12月, DAT]]
    console.log(r);
});

```

### 文章類似度計算

機能:2つの文章間の類似度(どれぐらい似ているか)を算出する

コピペゾーン:コピペゾーン1

コピペ用コード

```js
//text1, 2変数にBotからの発話などを入れておく
docomo.similarity(text1,text2, function(s){
    //類似度(0~1.0)
    console.log(s);
});

```

### 雑談対話

機能:発話を入れると、それっぽい雑談の発話を返してくれる

コピペゾーン:コピペゾーン1

コピペ用コード

```js
//text変数にBotからの発話を入れておく
docomo.dialogue(text, null, null, "dialog", 0, function(r, y, id){
    //APIからの応答
    //r:応答
    //y:読み
    console.log(r);
});
```


## Face API

### 顔検出

機能:顔画像から写っている顔のパーツの位置、何歳ぐらいか、性別などを抽出する

コピペゾーン:コピペゾーン3


コピペ用コード

```js
//image変数に顔画像のURLを入れておく
cognitive.faceDetect(API_KEY.FACE_APIKEY,image,true,true,"smile,age",function(err,res,body){
    console.log(body);
});
```


## Emotion API

### 感情抽出

機能:顔画像から写っている顔の感情を判定する

コピペゾーン:コピペゾーン3

コピペ用コード

```js
//image変数に顔画像のURLを入れておく
cognitive.emotion(API_KEY.EMOTION_APIKEY,image,function(err,res,body){
    console.log(body);
});
```


## Conputer Vision API

### 物体抽出

機能:風景画像から、何が写っているかを抽出する

コピペゾーン:コピペゾーン3

コピペ用コード

```js
//imageに風景画像のURLを入れておく
cognitive.computerVision(API_KEY.COMPUTER_VISION_APIKEY,image,function(err,res,body){
    console.log(body);
});

```

## BingSearch API

### Bing検索

機能:Bingで検索を行う

コピペゾーン:コピペゾーン1

コピペ用コード

```js
var query = "病院";
cognitive.bingSearch(API_KEY.BING_SEARCH_APIKEY,query,10,0,function(err,res,body){
    console.log(body);
});

```