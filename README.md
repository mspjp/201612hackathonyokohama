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

Tutorialを見てください

- [csharp](csharp/README.md)
- [node.js](node.js/README.md)


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
