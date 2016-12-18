# Bot作成(C#編)チュートリアル
## 1. 注意事項
このチュートリアルを行うには以下の環境がひつようになります。
- Windows10 PC
- Visual Studio Community 2015
- Microsoft Imagine (旧Dream Sparkのライセンス)
- Microsoft Azureの環境

まだ準備ができていない人はスタッフまで声をかけてください。

このチュートリアルには

- **学習価値のある一般技術知識**
- **ハッカソンでしか使えない技術知識**

の2つの情報が含まれています。

**学習価値のある一般技術知識**  は一般的に通用しているMicrosoftの技術知識です。ほかに応用が利くのでハッカソンを通して学習してもらえればと思います。

**ハッカソンでしか使えない技術知識**  今回、ハッカソンでアイデアを実現するために様々なライブラリや環境を用意しています。なのでこの知識は覚える必要はありません。ハッカソンで使ってあとは忘れてください。

## 2. セットアップ
### 2.1 プロジェクトを開く
**ハッカソンでしか使えない技術知識**

ダウンロードした201612hackathonyokohamaフォルダを開き、csharp/SampleBotフォルダの中にある**SampleBot.sln**をダブルクリックして開きます。

![1](./img/1.png)

VisualStudioが立ち上がると、右上の**ソリューションエクスプローラー**という部分に3つのプロジェクトがあることがわかります。
3つのプロジェクトの説明は以下のとおりです。

- **ApiSamples** - Botの機能として使う各種APIのサンプルコード
- **BotLibrary** - Botの機能として使う各種APIを使うためのライブラリ
- **SampleBot** - Bot本体 (Asp.netのプロジェクト)

![2](./img/2.png)

したがって、皆さんがBotを作るためにいじるプロジェクトは**SampleBot**プロジェクトとなります。

### 2.2 Botのサーバーを作る
**学習価値のある一般技術知識**

Botはサーバー上で動くため、ホスティングするサーバーを立てなければなりません。
今回はMicrosoft Azureというクラウド上に、学生ライセンスを使って無料でホスティングを行います。

[http://portal.azure.com](http://portal.azure.com)にアクセスし、**MicrosoftImagineで学生認証を行ったMicrosoftアカウントでサインイン**します。

以下のような画面が表示されればOKです。
![3](./img/3.png)

Webサーバーを作ります。Microsoft AzureではWebサーバーなどの資源は**リソース**として管理されます。
そしてMicrosoft AzureのWebサーバーは**Web App**というリソースになります。
Web Appを新規作成しましょう。

左上の**+新規**>**Web+モバイル**>**Web App**を選択します。

![4](./img/4.png)

下図のような画面が出るので

- アプリ名 - お好きなIDを入力
- サブスクリプション - Microsoft Imagine(もしくはDream Spark)を選択
- リソースグループ - 新規作成、**MyBotGroup**と入力
- App Serviceプラン/場所 - 後述

を入力します。

アプリ名は他人と被らないものを入力してください。右側に緑色のチェックマークが表示されればOKです。

アプリ名.azurewebsites.netがBotのURLとなります。

サブスクリプションの選択で**Microsoft Imagine(もしくはDream Spark)**が表示されない方は学生認証の準備をしていない可能性があります。速やかに手を挙げてスタッフを呼んでください。

![5](./img/5.png)

**App Serviceプラン 場所**をクリックすると下図のようになります。**新規作成**を押し

- App Service プラン - お好きなIDを入力
- 場所 - Japan Eastを選択
- 価格レベル - F1 Freeを選択

を入力し、OKを押します。

![6](./img/6.png)

ここまでできたら**作成**を押します。

![7](./img/7.png)

右上のベルマークに**デプロイメントが成功しました**と表示されればOKです。

![8](./img/8.png)

作成したWebサイトを見てみましょう。
左側の**すべてのリソース**から先ほど作成したIDをクリックします。

![9](./img/9.png)

先ほど作成したWebAppの管理画面が表示されます。
URLが表示されているのでURLをクリックします。

![10](./img/10.png)

Webサイトを表示することができました。
デフォルトで用意されているページが表示されているはずです。

![11](./img/11.png)

### 2.3 BotをBot Connectorに新規登録する
**学習価値のある一般技術知識**

Microsoft BotFrameworkでは[Bot Connector](http://www.atmarkit.co.jp/ait/articles/1604/15/news032.html)というものを利用して、自分のBotを配置したWebサーバーとslackやSkypeなどのメッセンジャーサービスをつなげることができます。

なのでBotをメッセンジャーサービスにつなげるには、必ずBot Connectorに登録しなければいけません。

![19.png](./img/19.png)

[BotFrameworkの公式サイト](https://dev.botframework.com)にアクセスします。

**Register a bot**をクリックします。

![20.png](./img/20.png)

Microsoftのアカウントでのサインインを求められた場合、ご自身のMicrosoftアカウントでサインインしてください。

Botの新規登録ページが出てきたら下記の情報を入力してください。

- **Icon**:Botのアイコンになります。好きなアイコンにしたければ30K以下のpngファイルを指定してください。特に気にならなければ何も指定しなくてOK
- **Name**: Botの名前。好きな名前をどうぞ
- **Bot handle**:BotのIDになります。他と被らない好きなIDを入れます。
- **Description**:詳細説明。テキトーなことを書いてOK

![21](./img/21.png)

- **Messaging endpoint**:Botを配置したWebAppのURL。

```
https://{webappのID}.azurewebsites.net/api/Messages

```

を入力(↑**httpsなところに注意！**)

できたら**Create Microsoft App ID and password**ボタンを押します。

![22](./img/22.png)

新しくタブが開くと、すでにアプリ名とアプリIDが入力されていると思います。

**アプリIDをどこかにメモしてください**

**アプリパスワードを作成して続行**ボタンを押します。

![23](./img/23.png)

新しくパスワードが作成されましたという表示の元、Botのアプリパスワードが表示されます。

**このパスワードは1回しか表示されないのでかならずどこかにメモしてください**

(パスワードを忘れた場合再発行になります)

**OK**をおしてダイアログを閉じます

![24](./img/24.png)

**終了してボットのフレームワークに戻る**を押します。

戻ると、アプリIDが入力されています。入力されていない場合はメモしたアプリIDを入力してください。

Adminの項目は何も入力しなくてもOKです。

一番下の**By clicking Register ～**の部分にチェックをいれ、**Register**ボタンを押してBotを新規登録します。

![25](./img/25.png)

**Bot created**と表示されればOKです。**OK**ボタンをおします。

![26](./img/26.png)

BotをBot Connectorに新規登録できました。

![27](./img/27.png)

この時点で

- BotのMicrosoft App Id
- BotのMicrosoft App password

がどこかにメモできているでしょうか。
メモできていなければ、上の手順を読み直して、Botを再度作成してください。

### 2.4 BotのIDとパスワードをBotのプログラムに入れる
**学習価値のある一般技術知識**

Botを動かすためには、先ほど取得したBotのIDとpasswordを開発しているBotのプログラムに入力する必要があります。

VisualStudioを開き、SampleBotプロジェクトの中の、**Web.config**ファイルを開いてください。

Web.configファイルは下図のようなxmlファイルになっています。

その中に、**MicrosoftAppId**と**MicrosoftAppPasseword**の2つの項目があります(下図の赤い部分)

それぞれの**value**の部分に、先ほど取得したBotのMicrosoftAppIdとMicrosoftAppPasswordを入力してください。

![29](./img/29.png)

### 2.5 その他のWebAPIKeyを入れる
**ハッカソンでしか使えない技術知識**

ダウンロードしたSampleBotプロジェクトは、今回ハッカソンで使用する各種WebAPIのAPIKeyが入っていないため、ビルド(プログラムを実行ファイルにすること)ができません。

そこでまずはApiKeyを入力しましょう。

ソリューションエクスプローラーから**BotLibrary**プロジェクトを右クリックし、**追加**>**新しい項目**をクリックします。

![15](./img/15.png)

新しい項目の追加ダイアログが表示されたら、真ん中のリストから、**クラス**という項目を選択し、**名前**の欄に**ApiKey.cs**と入力し、**追加**ボタンを押します。

![16](./img/16.png)

ApiKey.csというファイルが新しく作成されるので、中身を以下のようにします。

```cs
public static class ApiKey
{
    public static string DOCOMO_APIKEY = "";
    public static string FACE_APIKEY = "";
    public static string EMOTION_APIKEY = "";
    public static string COMPUTER_VISION_APIKEY = "";
    public static string BING_SEARCH_APIKEY = "";
}
```

![17](./img/17.png)

各種APIKeyはスタッフから指示があるまでお待ちください。

これで、メニューバーから**ビルド**>**ソリューションのビルド**を選択し、エラーが出なければOKです。

![18](./img/18.png)

### 2.6Botをサーバーに配置する
**学習価値のある一般技術知識**

今回開発するBotはMicrosoftの[BotFramework](https://dev.botframework.com/)というものを使用して開発します。
BotFrameworkはC#とnode.jsの2種類の開発方法がありますが、C#での開発の場合、**ASP.net**という技術を用いてBotを開発します。
ASP.netはC#でWebアプリを作るテクノロジーです。

つまり、C#とBotFrameworkで開発したBotをWebサーバーに配置するということは、**ASP.netのWebアプリをWebサーバーに配置する**ということになります。

WebAppにアプリを配置するには、WebAppから**発行プロファイル**をダウンロードします。ダウンロードした発行プロファイルの情報を使用して、VisualStudioから、WebAppへアプリを配置することができます。

![12](./img/12.png)

先ほどのWebAppを表示します。
**概要**を表示して、右側にある**項目表示**をクリックし、**発行プロファイルの取得**を押します。

![13](./img/13.png)

発行プロファイルがダウンロードできます。

![14](./img/14.png)

では実際に配置していきましょう。

VisualStudioでSampleBotプロジェクトを開き、ソリューションエクスプローラーからSampleBotプロジェクトを右クリックし、**公開**を押します。

![30](./img/30.png)

発行ダイアログが表示されるので、**インポート**を押します。

![31](./img/31.png)

発行プロファイルを指定するダイアログが表示されるので**参照**から先ほどダウンロードした**発行プロファイル**を指定します。

![32](./img/32.png)

発行プロファイルを指定して**開く**を押します。

![33](./img/33.png)

**OK**を押して発行プロファイルの指定を終わります。

正しい発行プロファイルを指定できた場合、下図のようになるので、**発行**を押します。

![34](./img/34.png)

ブラウザで自分のWebAppのURLが表示され、Botのページへようこそという文字が表示されればWebAppへSampleBotの配置完了です。

![35](./img/35.png)

### 2.7 Botと対話してみる
**学習価値のある一般技術知識**

ここまでで、ついにBotをサーバーに配置し、対話できる状態になりました。
では実際に対話してみましょう。

再び[dev.botframework.com](https://dev.botframework.com/)にアクセスします。

自分が先ほど作成したBotを表示するために**My bots**をクリックします。

![36](./img/36.png)

先ほど作成したBotの名前をクリックします。

![37](./img/37.png)

Botのページが表示されました。

**Test**ボタンがありますがこれはおそらくクリックしてもエラーがでると思います。(すみません仕様です)

![38](./img/38.png)

下にスクロールしていくと、チャットのテストをすることができます。
テキトーに入力して反応を見ましょう。

**こんにちは**と入力すると違った反応が見えると思います。

![39](./img/39.png)

### 2.8 Slackと連携する
**学習価値のある一般技術知識**

Botの準備はできたのでメッセンジャーアプリと連携してみましょう。
今回はいろんな都合により、slackを使いたいと思います。

slackのアカウントを持っていない人は、[https://slack.com/create](https://slack.com/create)にアクセスし、アカウントを新規作成してください。
基本的にはサイトに沿ってアカウントを作ることができるとは思いますがわからない場合はスタッフに声をかけてください。

また、Botを配置できるチームを持っていない方は[https://slack.com/](https://slack.com/)で新しいチームを作成してください。
（今回のテスト用に個人用のチームを作成することをおすすめします。）

slackのアカウントを作成することができたら、作成したBotのページにアクセスします。

下の方にスクロールすると、Slackのアイコンがあるので**Add**というところを押します。

![40](./img/40.png)

slackの設定ページが表示されます。

最初の**Login to Slack～**をクリックして展開し、中にあるリンクをクリックします。

![41](./img/41.png)

右上の**Create New App**ボタンをクリックします。

![42](./img/42.png)

App Nameには英語で好きな名前を指定してください。Slack BotのIDになります。
Development Slack TeamはデフォルトのままでOKです。

**Create App**ボタンをクリックします。

![43](./img/43.png)

左のリストから、**OAth&Permissions**を選択し、**Redirect URL(s)**のところに、以下のURLを入力します。

```
https://slack.botframework.com
```

入力できたら、**Save Changes**ボタンを押します。

![44](./img/44.png)

左側のリストから、**Bot Users**をクリックし、**Add a Bot User**ボタンを押します。

![45](./img/45.png)

**Add Bot User**ボタンをクリックします。

![46](./img/46.png)

左側のリストから、**Basic Information**をクリックします。

表示された**Client ID**と**Client Secret(Showボタンを押したもの)**をどこかにメモしておいてください。

![47](./img/47.png)

先ほどのBotとSlackの連携設定ページに戻り、**Submit your Credentials**を展開し、**Client Id**と**Client Secret**に先ほどコピーしたものを貼り付けます。

**Submit Slack Credentials**ボタンを押します。

![48](./img/48.png)

**Autholize**をクリックします。

![49](./img/49.png)

**Credentials have been validated**と表示されればOKです。

**I'm done configuring Slack**ボタンを押します。

![50](./img/50.png)

以上でslackとBot Frameworkの連携が完了しました。

Slackを開くと、設定したBotがDirect Messageのところにいるので話すと、同じように会話することができます。

![51](./img/51.png)

Slackはスマホアプリもあり、アプリをいれると、スマートフォンからも会話することができます。

## 3. Botの拡張
### 3.1 ローカルで開発をする
**ハッカソンでしか使えない技術知識**

先ほどまでで、BotのプログラムをWebサーバーに配置してBotと対話することができました。

しかし、Botのプログラムを変更するたびに、毎回Webサーバーに配置して確認->またプログラム変更みたいなことをしていると非常に効率が悪いです。

そこでBotFrameworkではエミュレーターを使用してWebサーバーに配置しなくても、Botがどういう応答をするのかを確認しながら開発することができます。

[BotFramework Emulator](https://docs.botframework.com/en-us/tools/bot-framework-emulator/)にアクセスします。

Get it hereのhereにあるリンクをクリックします。

![52](./img/52.png)

ダウンロードしたbotframeworkのインストーラを起動し、インストールします。

![53](./img/53.png)

エミュレーターをインストールできました。

![54](./img/54.png)

VisualStudioを開いて、ツールバーにある**緑の三角マーク**を押します。
このボタンでプログラムを実行することができます。

![55](./img/55.png)

ブラウザで新しくページが表示されます。

```
localhost:3979
```

と、アドレスバーにでると思いますが、これがローカルで動いているBotのアドレスとポート番号です。

![56](./img/56.png)

Bot Framework Emulator上のEnter your endpoint URLと書かれている部分にBotのアドレスとポート番号＋api/messagesを入力します。

![57](./img/57-1.png)

続けて表示されたテキストボックスの各項目を埋めます。

- **Microsoft App Id** :自分のBotのApp Id
- **Microsoft App Password** :自分のBotのApp Password

![57](./img/57-2.png)

内容を確認してConnectボタンを押します。

下のテキストボックスに発話を入力すると会話することができます。

![58](./img/58.png)

Botのサーバーを停止するには**赤い■ボタン**を押します。

![59](./img/59.png)

これで、Webサーバーに配置せずとも、ローカルでBotの開発ができるようになりました。

実際にBotをslackなどと対話させたい場合は、もう一度前の手順をつかってWebAppに配置しましょう。

### 3.2 ルールファイルを編集して応答ルールを変える
**ハッカソンでしか使えない技術知識**

今回スタッフが作成したSampleBotは、プログラムがかけなくても応答パターンを変更できるように、Botの応答ルールをcsvファイルとして記述することができます。

VisualStudioで**ソリューションエクスプローラー**から**SampleBot**プロジェクトの中の**App_Data/rule.csv**ファイルを開きます。

今回作成したcsvは下図のようなルールになっています。

ルールは

```
ルール(正規表現),応答発話
```
という風にcsvを記述していきます。

![60](./img/60.png)

Botに発話が入力されると、左側に書いた正規表現のルールを検索し、マッチしたルールの右側にある応答文をBotが返してくれます。

例えば**おやすみ**というワードが発話に入っている時、**おやすみなさいませ**と応答させるなら、以下のようなルールを追加します。

```
.*おやすみ.*,おやすみなさいませ
```
発話中に**hoge**が入っているというルールは正規表現で以下のように書くことができます。
```
.*hoge.*
```
![61](./img/61.png)

csvを変更したら、プログラムを実行してエミュレーターから**おやすみ**と入力してみましょう。

![62](./img/62.png)

応答ルールを変えることができました。

あまりプログラムに自身がない人はこのcsvファイルを編集して、オリジナルな応答を行うBotを作りましょう。

### 3.3 プログラムで応答を変える
**ハッカソンでしか使えない技術知識**

応答ルールをcsvで記述するだけでなく、C#のプログラムを編集して応答ルールを変更することもできます。

VisualStudioで**ソリューションエクスプローラー**から**SampleBot**プロジェクトの中の**Controller/MessagesController.cs**ファイルを開きます。

Botにメッセージが来ると、このファイルの中の**OnMessageAsync**というメソッドが呼び出されます。

![63](./img/63.png)

OnMessageAsyncメソッドでは引数にActivity型のmessageという引数が与えられています。

このmessageの中に、Botに送られてきたメッセージが入っています。
Botに送られてきたメッセージを取得するには以下の文を記述します。
```cs
var text = message.Text;
```

![64](./img/64.png)

また、Botからメッセージを応答させたい場合は以下の文を記述します。

```cs
await ReplyMessageAsync(connector, message,"応答分です");
```

### 3.4 応答ルールファイルとプログラムを連携させる
**ハッカソンでしか使えない技術知識**

SampleBotでは、先ほど記述した応答ルールのcsvとC#のプログラムを連携させることができます。

以下のように、応答に

```
{command1}
```
のように{　　}でくくったような応答を書くと、

![65](./img/65.png)

**MessageController.cs**ファイルの**OnCommandAsync**メソッドに{  }内の文字列が渡されます。(今回の場合はcommand1)

![66](./img/66.png)

このように正規表現で記述した条件にマッチした場合、C#のプログラムでAPIを利用したりなどができます。

![67](./img/67.png)

### 3.5 発話データを外部APIと連携する
**ハッカソンでしか使えない技術知識**

今回、よりすごい機能を簡単に実装できるように、コピーアンドペースト用のサンプルコード集を作りました。
サンプルコード集は[COPYCODE.md](./COPYCODE.md)にあります。

この中で、発話のテキストデータを解析するAPIを使用してみましょう。

例えば、docomoのAPIの場合、漢字をひらがなに変換することができます。
コピペ用コードはこのようになっていますが、下のコードの意味としては、ExecAsyncメソッドに渡すtextという変数に入った文字列を、ひらがなに変換することができるコードです。
```cs
//コピペゾーン1にコピペする
var text = "あらゆる現実を全て自分の方へねじ曲げたのだ";
var client = new Hiragana(ApiKey.DOCOMO_APIKEY);
var mode = Hiragana.OutputType.HIRAGANA;
var result = await client.ExecAsync(text, mode);
```

コピペ用コードには説明のところに**コピペゾーン**が指定されています。コピペゾーンとはソースコードにコメントとして書いてあるコピペゾーンのことです。

![68](./img/68.png)

今回はコピペゾーン1が指定されているのでコピペゾーン1の下にコピペしましょう。

するとおそらく赤波線がついている部分が現れると思います。

![69](./img/69.png)

これは**名前空間**というものが正しく追加されていないため現れるエラー表示です。
この場合、赤波線がついているところを右クリックし、[クイックアクションとリファクタリング]をクリックします。

![70](./img/70.png)

すると下図のような表示がでるのでusing～始まっているものを選択しましょう。

![71](./img/71.png)

エラーが消えて、使えるようになりました。

![72](./img/72.png)

今回の場合、resultには発話がひらがなに変換された文字列が入っています。

### 3.6 画像データを外部APIと連携する
**ハッカソンでしか使えない技術知識**

今回のBotは、添付された画像データも利用することができます。
例えばサンプルコード集[COPYCODE.md](./COPYCODE.md)にある、Cognitive ServiceのFace APIを用いると添付された画像にうつった顔が何歳か判定することができます。

画像データの場合、**コピペゾーン3**に主にコピペすることになります。

![73](./img/73.png)

今回はすでに以下のようなプログラムが書かれていると思います。

```cs
//Face APIを呼び出す
var client = new FaceServiceClient(ApiKey.FACE_APIKEY);
var faces = await client.DetectAsync(imageUrl, true, false, new List<FaceAttributeType>()
{
    FaceAttributeType.Age,
    FaceAttributeType.Smile
});

if (faces.Count() == 0)
{
    var reply = message.CreateReply("顔が検出できませんでした");
    await connector.Conversations.ReplyToActivityAsync(reply);
}
else
{
    var reply = message.CreateReply("素敵なお顔ですね！ " + faces.First().FaceAttributes.Age + "歳ですか？");
    await connector.Conversations.ReplyToActivityAsync(reply);
}

```

このプログラムは添付ファイルのURLを取得し、顔が写っているなら何歳か判定するプログラムとなっています。

![74](./img/74.png)

**重要1:画像ファイルの使用はslackかWebChatでしか使用できません**

つまりエミュレータでのローカルデバッグでは画像の取得はできないということを注意してください。

**重要2:同じファイル名の画像ファイルは2回連続で添付できません**

### 3.7 練習
**ハッカソンでしか使えない技術知識**

サンプルコード集[COPYCODE.md](./COPYCODE.md)には様々な機能がコピペできる状態で存在しています。

**今現在、Botに添付ファイルが来ると年齢認識するプログラムが書かれていますが、年齢認識ではなく、感情判定できるBotを作ってみてください**

機能の実装練習なので、わからなければ即座に手を挙げて、スタッフを呼んでください

## 4. おわりに
お疲れ様でした。チュートリアルは以上となります。

以降は様々な機能を駆使して、オリジナルなBotのアイデアを実現していきましょう。

### 4.1 あまりプログラミングが得意ではない人
基本的にはrule.csvファイルのルールを編集するだけでBotの応答ルールは変えることができます。まずはそこから初めて、Botにどういう応答をしてほしいかを考えながら進めていきましょう。プログラムと連携したいときはスタッフを呼んでください。

### 4.2 プログラミングがそこそこ得意な人
command機能やcognitive service、docomoのAPIなどを駆使して面白いアイデアを実現していきましょう。コピペでエラーなどが出たらスタッフを呼んでいただければ対処します。サンプルコード集はこちらです。[COPYCODE.md](./COPYCODE.md)

### 4.3 プログミングが得意な人
オリジナルなアイデアで賞をめざしてください！余裕があれば、周りで困っている人を助けてもらうとうれしいです。