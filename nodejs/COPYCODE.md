# コピペ用サンプルコード集(node.js)

- 発話テキストが入っている変数名: **text**
- 画像のURLが入っている変数名: **image**

- FaceAPIKeyの変数名: **API_KEY.FACE_APIKEY**
- EmotionAPIKeyの変数名: **API_KEY.EMOTION_APIKEY**
- ComputerVisionApiKeyの変数名: **API_KEY.COMPUTER_VISION_APIKEY**
- docomoAPIKeyの変数名: **API_KEY.DOCOMO_APIKEY**

で統一お願いします。

## Docomo API

### 形態素解析

機能: 文章から形態素(意味のあるまとまり)に分割する
コピペゾーン:コピペゾーン1

コピペ用コード

```js
//text変数にBotからの発話を入れておく
var result = "";    //形態素解析結果が入る
docomo.morph(text, 'form|pos|read', '', function(r){
    result = r;
});
```

### 漢字ひらがな変換

機能:
コピペゾーン:

コピペ用コード

```js

```

### 要素抽出

機能:
コピペゾーン:

コピペ用コード

```js

```

### 文章類似度計算

機能:
コピペゾーン:

コピペ用コード

```js

```

### 雑談対話

機能:
コピペゾーン:

コピペ用コード

```js

```


## Face API

### 顔検出

機能:
コピペゾーン:

コピペ用コード

```js

```

### 笑顔判定

機能:
コピペゾーン:

コピペ用コード

```js

```

## Emotion API

### 感情抽出

機能:
コピペゾーン:

コピペ用コード

```js

```


## Conputer Vision API

### 物体抽出

機能:
コピペゾーン:

コピペ用コード

```js

```

