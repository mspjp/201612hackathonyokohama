using BotLibrary;
using BotLibrary.Docomo;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using BotLibrary.Bing;

namespace ApiSamples
{
    class Program
    {
        private static string _docomoApiKey = ApiKey.DOCOMO_APIKEY;
        private static string _faceApiKey = ApiKey.FACE_APIKEY;
        private static string _emotionKey = ApiKey.EMOTION_APIKEY;
        private static string _computervisionkey = ApiKey.COMPUTER_VISION_APIKEY;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("MSPハッカソンのテストプログラムです");
                Console.WriteLine("試したいプログラムの数字を入力してください");
                Console.WriteLine("形態素解析: 1");
                Console.WriteLine("ひらがな変換: 2");
                Console.WriteLine("要素抽出: 3");
                Console.WriteLine("文章類似度計算: 4");
                Console.WriteLine("顔検出: 5");
                Console.WriteLine("感情検出: 6");
                Console.WriteLine("対話: 7");
                Console.WriteLine("画像分析: 8");
                Console.WriteLine("Bing検索: 9");
                Console.WriteLine("終了: 999");
                Console.Write(":");
                var command = Console.ReadLine();

                if (command == "999")
                    break;

                switch (command)
                {
                    case "1":
                        Console.WriteLine("形態素解析(MorphAnalysisAsync) 開始");

                        var results = MorphAnalysisAsync(_docomoApiKey).Result;
                        foreach (var r in results)
                        {
                            var line = string.Join(" ", r.Select(q => q.Form + "[" + q.Pos + "]"));
                            Console.WriteLine(line);
                        }
                        break;
                    case "2":
                        Console.WriteLine("ひがらな変換(HiraganaConvertAsync) 開始");

                        var result = HiraganaConvertAsync(_docomoApiKey).Result;
                        Console.WriteLine(result);
                        break;
                    case "3":
                        Console.WriteLine("要素抽出(EntityExtractAsync) 開始");

                        var resultsEntity = EntityExtractAsync(_docomoApiKey).Result;
                        var resultEntity = string.Join(" ", resultsEntity.Select(q => q.Entity + "[" + q.Type + "]"));
                        Console.WriteLine(resultEntity);
                        break;
                    case "4":
                        Console.WriteLine("文章類似度計算(SimilarityCalc) 開始");

                        var resultSimilarity = SimirarityCalcAsync(_docomoApiKey).Result;
                        Console.WriteLine(resultSimilarity);
                        break;
                    case "5":
                        Console.WriteLine("顔検出 開始");

                        var resultFaceDetect = FaceDetectAsync(_faceApiKey).Result;
                        Console.WriteLine("年齢: " + resultFaceDetect.First().FaceAttributes.Age + "歳");
                        Console.WriteLine("性別: " + resultFaceDetect.First().FaceAttributes.Gender);
                        Console.WriteLine("メガネ: " + resultFaceDetect.First().FaceAttributes.Glasses);
                        break;
                    case "6":
                        Console.WriteLine("感情検出　開始");

                        var resultEmotionDetect = EmotionDetectAync(_emotionKey).Result;
                        Console.WriteLine("怒り: " + resultEmotionDetect.First().Scores.Anger);
                        Console.WriteLine("軽蔑: " + resultEmotionDetect.First().Scores.Contempt);
                        Console.WriteLine("嫌悪: " + resultEmotionDetect.First().Scores.Disgust);
                        Console.WriteLine("恐怖: " + resultEmotionDetect.First().Scores.Fear);
                        Console.WriteLine("中立: " + resultEmotionDetect.First().Scores.Neutral);
                        Console.WriteLine("悲しみ: " + resultEmotionDetect.First().Scores.Sadness);
                        Console.WriteLine("驚き: " + resultEmotionDetect.First().Scores.Surprise);
                        Console.WriteLine("幸福: " + resultEmotionDetect.First().Scores.Happiness);
                        break;
                    case "7":
                        Console.WriteLine("対話API 対話モード(DialogueAsync) 開始");
                        var resultDialogue1 = DialogueAsync(_docomoApiKey).Result.Result;
                        Console.WriteLine(resultDialogue1);

                        Console.WriteLine("対話API しりとりモード-連続的な対話を行うケース(DialogueModeSrtrAsync) 開始");
                        var resultDialogue2 = DialogueModeSrtrAsync(_docomoApiKey).Result;
                        Console.WriteLine(string.Join(">", resultDialogue2));


                        Console.WriteLine("対話API 対話モード-ユーザーの情報を追加したケース-赤ちゃん風(DialogueUserAsync) 開始");
                        var resultDialogue3 = DialogueUserAsync(_docomoApiKey).Result.Result;
                        Console.WriteLine(resultDialogue3);
                        break;
                    case "8":
                        Console.WriteLine("画像分析　開始");

                        var resultComputerVision = ComputerVisionDetectAync(_computervisionkey).Result;
                        foreach(var tag in resultComputerVision.Tags)
                        {
                            Console.WriteLine(tag.Name);
                        }
                        break;
                    case "9":
                        var client = new WebSearch(ApiKey.BING_SEARCH_APIKEY);
                        var webResult = client.ExecuteAsync("アンパン",20).Result;
                        
                        break;
                    default:
                        Console.WriteLine("そのようなコマンドはありません");
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("****** コマンド実行完了しました *********");
                Console.WriteLine();
            }
        }
        

        private static async Task<List<List<Morph.MorphResultSet>>> MorphAnalysisAsync(string docomoApiKey)
        {
            var text = "あらゆる現実を全て自分の方へねじ曲げたのだ";
            var client = new Morph(docomoApiKey);
            Morph.InfoFilter info = Morph.InfoFilter.FORM | Morph.InfoFilter.POS;
            var results = await client.ExecAsync(text, info);
            return results;

        }

        private static async Task<string> HiraganaConvertAsync(string docomoApiKey)
        {
            var text = "あらゆる現実を全て自分の方へねじ曲げたのだ";
            var client = new Hiragana(docomoApiKey);
            var mode = Hiragana.OutputType.HIRAGANA;
            var result = await client.ExecAsync(text, mode);
            return result;
        }

        private static async Task<List<Entity.EntityResultSet>> EntityExtractAsync(string docomoApiKey)
        {
            var text = "今日は横浜市で中村さんとスポーツをしに行きました";
            var client = new Entity(docomoApiKey);
            Entity.ClassType filter = Entity.ClassType.ALL;
            var result = await client.ExecAsync(text, filter);
            return result;
        }

        private static async Task<double> SimirarityCalcAsync(string docomoApiKey)
        {
            var text1 = "今日は車で公園に行きました";
            var text2 = "今日は電車で公園に行きました";
            var client = new Similarity(docomoApiKey);
            var result = await client.ExecAsync(text1, text2);
            return result;
        }

        private static async Task<Dialogue.ResultSet> DialogueAsync(string docomoApiKey) {
            var text = "おはよう、こんにちは、こんばんわ。";
            var client = new Dialogue(docomoApiKey);
            var result = await client.ExecAsync(text);
            return result;
        }

        private static async Task<List<string>> DialogueModeSrtrAsync(string docomoApiKey) {
            var srtr = new List<string>();

            var text = "さくら";
            srtr.Add(text);
            var client = new Dialogue(docomoApiKey);
            var result = await client.ExecAsync(text, mode:Dialogue.DialogueMode.SRTR);
            srtr.Add(result.Result);

            text = "適当な文字";
            srtr.Add(text);
            //resultのIDを渡して対話を続ける
            result = await client.ExecAsync(text, mode: Dialogue.DialogueMode.SRTR, id:result.ID);
            srtr.Add(result.Result);

            return srtr;
        }

        private static async Task<Dialogue.ResultSet> DialogueUserAsync(string docomoApiKey) {
            var text = "こんにちは";
            var user = new Dialogue.UserInfo();
            //これらの情報は全てオプションなのですべて指定する必要はない
            
            user.BirthDay = new DateTime(1996, 1, 31);
            //誕生日から年齢と星座を変換
            user.ConverBirthDayToAge();
            user.ConvertBirthDayToConstellations();
            user.NickName = "舞黒花子";
            user.NickNameYomi = "マイクロハナコ";
            //地域名を指定しているがここの天気は？と聞いても何故か場所を教えろと答えるので意味が無いかも（APIコンソールも同様）
            user.Place = "東京";
            //Dialogue.Areas に利用可能な地域一覧が格納されている
            user.Sex = Dialogue.Sex.MALE;
            user.BloodType = Dialogue.BloodType.A;

            var client = new Dialogue(docomoApiKey);
            var result = await client.ExecAsync(text, user:user, characterId:30);
            return result;
        }

        private static async Task<Microsoft.ProjectOxford.Face.Contract.Face[]> FaceDetectAsync(string faceApiKey)
        {
            var client = new FaceServiceClient(faceApiKey);
            var url = "http://yukainanakamatati.com/wp-content/uploads/2014/07/a1-e1406013277329.jpg";
            var faces = await client.DetectAsync(url, true, false, new List<FaceAttributeType>()
            {
                FaceAttributeType.Age,
                FaceAttributeType.Gender,
                FaceAttributeType.Smile,
                FaceAttributeType.FacialHair,
                FaceAttributeType.HeadPose,
                FaceAttributeType.Glasses
            });
            return faces;
        }

        private static async Task<Emotion[]> EmotionDetectAync(string emotionApiKey)
        {
            var client = new EmotionServiceClient(emotionApiKey);
            var url = "https://github.com/Microsoft/Cognitive-Face-Windows/blob/master/Data/detection2.jpg?raw=true";
            var emotion = await client.RecognizeAsync(url);

            return emotion;

        }

        private static async Task<AnalysisResult> ComputerVisionDetectAync(string computerVisionApiKey)
        {
            VisionServiceClient client = new VisionServiceClient(computerVisionApiKey);
            var url = "https://github.com/Microsoft/Cognitive-Face-Windows/blob/master/Data/detection2.jpg?raw=true";
            var vision = await client.AnalyzeImageAsync(url, new List<VisualFeature>()
            {
                VisualFeature.Adult,
                VisualFeature.Categories,
                VisualFeature.Color,
                VisualFeature.Description,
                VisualFeature.Faces,
                VisualFeature.ImageType,
                VisualFeature.Tags
            });
            return vision;
        }
    }
}
