# コピペ用サンプルコード集(C#)

- 発話テキストが入っている変数名: **text**
- 画像のURLが入っている変数名: **image**

- FaceAPIKeyの変数名: **ApiKey.FACE_APIKEY**
- EmotionAPIKeyの変数名: **ApiKey.EMOTION_APIKEY**
- ComputerVisionApiKeyの変数名: **ApiKey.COMPUTER_VISION_APIKEY**
- Bing Search APIKeyの変数名: **ApiKey.BING_SEARCH_APIKEY**
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

機能:漢字の入っている文章をひらがなに変換する

コピペゾーン:コピペゾーン1

コピペ用コード

```cs
//text変数にBotからの発話を入れておく
var client = new Hiragana(ApiKey.DOCOMO_APIKEY);
var mode = Hiragana.OutputType.HIRAGANA;
//result変数にひらがなに変換された文章が入る
var result = await client.ExecAsync(text, mode);


```

### 要素抽出

機能:文章の中から、人名、地名、日付などの要素を抽出する

コピペゾーン:コピペゾーン1

コピペ用コード

```cs
//text変数にBotからの発話を入れておく
var client = new Entity(ApiKey.DOCOMO_APIKEY);
Entity.ClassType filter = Entity.ClassType.ALL;
//result変数に抽出した要素が入る
var result = await client.ExecAsync(text, filter);

```

### 文章類似度計算

機能:2つの文章間の類似度(どれぐらい似ているか)を算出する

コピペゾーン:コピペゾーン1

コピペ用コード

```cs
//text1とtext2に類似度を算出したい文章を入れておく
var client = new Similarity(ApiKey.DOCOMO_APIKEY);
//result変数に類似度が入る
var result = await client.ExecAsync(text1, text2);

```

### 雑談対話

機能:発話を入れると、それっぽい雑談の発話を返してくれる

コピペゾーン:コピペゾーン1

コピペ用コード

```cs
//textにBotからの発話を入れておく
var client = new Dialogue(ApiKey.DOCOMO_APIKEY);
//result変数にそれらしい雑談対話が入る
var result = await client.ExecAsync(text);

```

## Face API

### 顔検出

機能:顔画像から写っている顔のパーツの位置、何歳ぐらいか、性別などを抽出する

コピペゾーン:コピペゾーン2

コピペ用コード

```cs
//image変数に顔画像のURLを入れておく
var client = new FaceServiceClient(ApiKey.FACE_APIKEY);

//抽出できた顔とパラメータがfacesに入る
var faces = await client.DetectAsync(image, true, false, new List<FaceAttributeType>()
{
    FaceAttributeType.Age,
    FaceAttributeType.Gender,
    FaceAttributeType.Smile,
    FaceAttributeType.FacialHair,
    FaceAttributeType.HeadPose,
    FaceAttributeType.Glasses
});

```


## Emotion API

### 感情抽出

機能:顔画像から写っている顔の感情を判定する

コピペゾーン:コピペゾーン2

コピペ用コード

```cs
//image変数に顔画像のURLを入れておく
var client = new EmotionServiceClient(ApiKey.EMOTION_APIKEY);
//emotion変数に抽出した感情値が入る
var emotion = await client.RecognizeAsync(image);

```


## Conputer Vision API

### 物体抽出

機能:風景画像から、何が写っているかを抽出する

コピペゾーン:コピペゾーン2

コピペ用コード

```cs
//imageに風景画像のURLを入れておく
var client = new VisionServiceClient(ApiKey.COMPUTER_VISION_APIKEY);
//vision変数に抽出された風景が入る
var vision = await client.AnalyzeImageAsync(image, new List<VisualFeature>()
{
    VisualFeature.Adult,
    VisualFeature.Categories,
    VisualFeature.Color,
    VisualFeature.Description,
    VisualFeature.Faces,
    VisualFeature.ImageType,
    VisualFeature.Tags
});


```

## BingSearch API

### Bing検索

機能:Bingで検索を行う

コピペゾーン:コピペゾーン1

コピペ用コード

```cs
//text変数にBotからの発話を入れておく
var client = new WebSearch(ApiKey.BING_SEARCH_APIKEY);
var webResult = await client.ExecuteAsync(text,10);

```

