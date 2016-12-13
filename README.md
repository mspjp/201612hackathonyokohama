# 201612hackathonyokohama
2016年12月18日に行う予定の横浜ハッカソンのサンプルコードリポジトリです

## Windowsを使用している方
csharpフォルダの中を使用してください

## Macを使用している方
nodejsフォルダの中を使用してください

# リポジトリ構成

```
/csharp/SampleBot/SampleBot -- ハッカソンBotの.Netテンプレート
/csharp/SampleBot/BotLibrary -- Bot用のライブラリ(docomoAPIなどの実装)
/csharp/SampleBot/ApiSamples -- Bot用のライブラリコピペ用コードとテストコンソールプログラム

/nodejs/SampleBot/app.js -- ハッカソンBotのnodejsテンプレート
/nodejs/SampleBot/docomoapi.js -- docomoAPIのnode.js実装モジュール
/nodejs/SampleBot/apisamples.js -- Bot用のライブラリコピペ用コードとテストコンソールプログラム
```

# セットアップ
公開リポジトリの関係でAPIKEY周りをignoreしているので再生成して、自分のAPIKEYに置き換えてください。

## Windows
csharpフォルダの中のBotLibraryプロジェクトに以下のC#ファイル```ApiKey.cs```というファイル名で追加してください。

```cs
public static class ApiKey{
    public static string DOCOMO_APIKEY = "";
    public static string FACE_APIKEY = "";
    public static string EMOTION_APIKEY = "";
    public static string COMPUTER_VISION_APIKEY = "";
}
```
上記ファイルのApiKeyを自分のものに置き換えてください

csharpフォルダの中のSampleBotプロジェクト内の```Web.config```というファイルを開いて
```xml
<appSettings>
    <!-- update these with your BotId, Microsoft App Id and your Microsoft App Password-->
    <add key="BotId" value="YourBotId" />
    <add key="MicrosoftAppId" value="" />
    <add key="MicrosoftAppPassword" value="" />
</appSettings>
```
上記の部分を自分のBotの情報に置き換えてください。

## Mac,Linux
nodejs/SampleBotフォルダ内に以下のjsonファイルを```apikey.json```というファイル名で追加してください。

```json
{
    "BOT_APIKEY":"",
    "BOT_APISECRET":"",
    "FACE_APIKEY":"",
    "DOCOMO_APIKEY":""
}
```

上記のAPIKeyを自分のものに置き換えてください。
