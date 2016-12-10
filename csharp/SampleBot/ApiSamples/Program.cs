using BotLibrary;
using BotLibrary.Docomo;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.ProjectOxford.Emotion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion.Contract;

namespace ApiSamples
{
    class Program
    {
        private static string _docomoApiKey = ApiKey.DOCOMO_APIKEY;
        private static string _faceApiKey = ApiKey.FACE_APIKEY;
        private static string _emotionKey = ApiKey.EMOTION_APIKEY;
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

        private static async Task<Face[]> FaceDetectAsync(string faceApiKey)
        {
            var client = new FaceServiceClient(_faceApiKey);
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
            var client = new EmotionServiceClient(_emotionKey);
            var url = "https://github.com/Microsoft/Cognitive-Face-Windows/blob/master/Data/detection2.jpg?raw=true";
            var emotion = await client.RecognizeAsync(url);

            return emotion;

        }
    }
}
