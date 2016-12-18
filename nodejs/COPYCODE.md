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
    console.log(r);
});
```

### 漢字ひらがな変換

機能: 漢字の入っている文章をひらがなに変換する

コピペゾーン:コピペゾーン1

コピペ用コード

```js
docomo.hiragana(text, 'hiragana', function(r){
    console.log(r);
});
```

### 要素抽出

機能:文章の中から、人名、地名、日付などの要素を抽出する

コピペゾーン:コピペゾーン1

コピペ用コード

```js
docomo.entity(text, 'DAT', function(r){
    console.log(r);
});

```

### 文章類似度計算

機能:2つの文章間の類似度(どれぐらい似ているか)を算出する

コピペゾーン:コピペゾーン1

コピペ用コード

```js
docomo.similarity(text1,text2, function(s){
    console.log(s);
});

```

### 雑談対話

機能:発話を入れると、それっぽい雑談の発話を返してくれる

コピペゾーン:コピペゾーン1

コピペ用コード

```js
docomo.dialogue(text, null, null, "dialog", 0, function(r, y, id){
    console.log(r);
});
```


## Face API

### 顔検出

機能:顔画像から写っている顔のパーツの位置、何歳ぐらいか、性別などを抽出する

コピペゾーン:コピペゾーン2


コピペ用コード

```js
cognitive.faceDetect(API_KEY.FACE_APIKEY,image,true,true,"smile,age",function(err,res,body){
    console.log(body);
});
```


## Emotion API

### 感情抽出

機能:顔画像から写っている顔の感情を判定する

コピペゾーン:コピペゾーン2

コピペ用コード

```js
cognitive.emotion(API_KEY.EMOTION_APIKEY,image,function(err,res,body){
    console.log(body);
});
```


## Conputer Vision API

### 物体抽出

機能:風景画像から、何が写っているかを抽出する

コピペゾーン:コピペゾーン2

コピペ用コード

```js
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