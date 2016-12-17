# コピペ用サンプルコード集(node.js)

- 発話テキストが入っている変数名: **text**
- 画像のURLが入っている変数名: **image**

- FaceAPIKeyの変数名: **API_KEY.FACE_APIKEY**
- EmotionAPIKeyの変数名: **API_KEY.EMOTION_APIKEY**
- ComputerVisionApiKeyの変数名: **API_KEY.COMPUTER_VISION_APIKEY**
- BingSearchAPIの変数名: **API_KEY.BING_SEARCH_APIKEY**
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

機能: 漢字の入っている文章をひらがなに変換する
コピペゾーン:コピペゾーン1

コピペ用コード

```js

```

### 要素抽出

機能:文章の中から、人名、地名、日付などの要素を抽出する
コピペゾーン:コピペゾーン1

コピペ用コード

```js

```

### 文章類似度計算

機能:2つの文章間の類似度(どれぐらい似ているか)を算出する
コピペゾーン:コピペゾーン1

コピペ用コード

```js

```

### 雑談対話

機能:発話を入れると、それっぽい雑談の発話を返してくれる
コピペゾーン:コピペゾーン1

コピペ用コード

```js

```


## Face API

### 顔検出

機能:顔画像から写っている顔のパーツの位置、何歳ぐらいか、性別などを抽出する
コピペゾーン:コピペゾーン2


コピペ用コード

```js

```


## Emotion API

### 感情抽出

機能:顔画像から写っている顔の感情を判定する
コピペゾーン:コピペゾーン2

コピペ用コード

```js

```


## Conputer Vision API

### 物体抽出

機能:風景画像から、何が写っているかを抽出する
コピペゾーン:コピペゾーン2

コピペ用コード

```js

```

## BingSearch API

### Bing検索

機能:Bingで検索を行う
コピペゾーン:コピペゾーン1

コピペ用コード

```js

```