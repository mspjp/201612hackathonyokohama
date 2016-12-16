# コピペ用サンプルコード集(C#)

- 発話テキストが入っている変数名: **text**
- 画像のURLが入っている変数名: **image**

- FaceAPIKeyの変数名: **ApiKey.FACE_APIKEY**
- EmotionAPIKeyの変数名: **ApiKey.EMOTION_APIKEY**
- ComputerVisionApiKeyの変数名: **ApiKey.COMPUTER_VISION_APIKEY**
- docomoAPIKeyの変数名: **ApiKey.DOCOMO_APIKEY**

で統一お願いします。

## Docomo API

### 形態素解析

機能:文章を意味のあるまとまり(形態素)に分割する
コピペゾーン:コピペゾーン1

コピペ用コード

```cs
//text変数にBotからの発話を入れておく
var client = new Morph(ApiKey.DOCOMO_APIKEY);
Morph.InfoFilter info = Morph.InfoFilter.FORM | Morph.InfoFilter.POS;

//result変数に形態素解析結果が入る
var results = await client.ExecAsync(text, info);

```

### 漢字ひらがな変換

機能:
コピペゾーン:

コピペ用コード

```cs

```

### 要素抽出

機能:
コピペゾーン:

コピペ用コード

```cs

```

### 文章類似度計算

機能:
コピペゾーン:

コピペ用コード

```cs

```

### 雑談対話

機能:
コピペゾーン:

コピペ用コード

```cs

```

## Face API

### 顔検出

機能:
コピペゾーン:

コピペ用コード

```cs

```

### 笑顔判定

機能:
コピペゾーン:

コピペ用コード

```cs

```

## Emotion API

### 感情抽出

機能:
コピペゾーン:

コピペ用コード

```cs

```


## Conputer Vision API

### 物体抽出

機能:
コピペゾーン:

コピペ用コード

```cs

```

