# Simple Bot Sample

## 目次

- Bot Builderにて作成されたサンプルについて
- Bot Directoryへの登録について
- Bot Connecterでの接続について


## Bot Builderにて作成されたサンプルについて

本ソリューション自体がBotのサンプルです.コントローラが3つ(Botの機能紹介として)含まれています.
簡単に出来ることの紹介をそれぞれ行っています.
ネットワーク上で使用するためにはAzure Appservices等でpublishする必要があります.(ここでpublishする必要はありません)

## Bot Directoryへの登録について
登録方法は以下の通りです.

- [ここ](https://dev.botframework.com/bots/new)に接続し必要な項目を埋めます.
  - Name : Botのニックネーム
  - Bot handle : Botの固有ID
  - Description : Botの説明
  - Messaging Endpoint : Bot接続時のルーティング
    - 今回であれば`{azure appservices url}/api/SimpleMessages`等
    - ルーティングの命名規則はASP.NETと似ている(WebAPIコントローラのルーティング)
      - Bot Frameworkの場合`~/api/SomeController/`を呼び出すと自動的に該当コントローラのPost関数が呼び出される模様
  - Create Microsoft App ID and password
    - リンク先でIDとパスワードが生成出来るので保存しておいてください.
    - Finishを押して戻ってくるとフォームにはIDが入力されています.
  - Instrumentation key
    - App Insightsを使う場合はKeyを入れるらしいです.

全ての項目に問題がなければ登録されます.[ここ](https://dev.botframework.com/bots)に一覧が表示されるはずです.

## Web.configの編集
Bot登録時に生成したID等をWeb.configに書き込みます.
- BotID : BotHandle
- MicrosoftAppId : MicrosoftAppId
- MicrosoftAppPassword : MicrosoftAppPassword

Web.configを編集したらここでpublishします.

一覧から該当BOTのページに飛び,Testボタンを押すと問題がなければ`Endpoint authorization succeeded`と表示されます.


## サービスとの接続
Botの一覧ページから各Botのページに飛び,下の方に行くと接続ボタンがあるのでそこを押すと出来ます
方法は接続時にHow toが出てくる模様です.

## Skypeの場合
Add to Skypeを押すだけだったと思います


## Slackの場合
- Addを押します
- 画面の指示に従ってSlack側にBotを造ってKey2つを取得します.
- 取得した鍵をBot Framework側に記入します.
- Slack側へ許可申請が飛ぶのでAuthorizeします.
- Slack側でBotが出来上がってるのでUserInviteの方法でチャンネルに入れます.




 
