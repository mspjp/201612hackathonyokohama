using DocomoAPISamples.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspHackathonApp
{
    class Program
    {
        private static string docomoApiKey = "77356844387a3351332f6267716f536d684e41584d706f78625430495872343566754f5862713031574143";
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("MSPハッカソンのテストプログラムです");
                Console.WriteLine("試したいプログラムの数字を入力してください");
                Console.WriteLine("形態素解析: 1");

                Console.WriteLine("終了: 999");
                Console.Write(":");
                var command = Console.ReadLine();

                if (command == "999")
                    break;

                ExecuteAsync(command).Wait();
                Console.WriteLine();
                Console.WriteLine("****** コマンド実行完了しました *********");
                Console.WriteLine();
            }
        }

        private static async Task ExecuteAsync(string command)
        {
            switch (command)
            {
                case "1":
                    Console.WriteLine("形態素解析 開始");
                    var client = new Morph(docomoApiKey);
                    Morph.InfoFilter info = Morph.InfoFilter.FORM|Morph.InfoFilter.POS;
                    var text = "あらゆる現実を全て自分の方へねじ曲げたのだ";
                    var results = await client.ExecAsync(text,info);
                    
                    foreach(var result in results)
                    {
                        var line = string.Join(" ",result.Select(q=>q.Form+"["+q.Pos+"]"));
                        Console.WriteLine(line);
                    }
                    break;
            }
        }
    }
}
