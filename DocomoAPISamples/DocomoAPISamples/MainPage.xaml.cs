using DocomoAPISamples.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace DocomoAPISamples
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //TODO 提供されたDocomoのAPIキーを入れてください。もしくはDocomoからAPIキーを取得してください。
        //https://dev.smt.docomo.ne.jp/?p=docs.api.page&api_name=language_analysis&p_name=api_usage_scenario
        private string APIKey = "YOURKEY";
        public MainPage()
        {
            this.InitializeComponent();
            HiraganaMode.ItemsSource = Enum.GetValues(typeof(Hiragana.OutputType));
        }

        private async void HiraganaConvert(object sender, RoutedEventArgs e)
        {
            try
            {
                HiraganaConvertButton.IsEnabled = true;
                HiraganaResult.Text = "変換中";
                var c = new Hiragana(APIKey);
                var mode = (Hiragana.OutputType)HiraganaMode.SelectedItem;
                var text = HiraganaSentence.Text;
                var result = await Task.Run(
                    async() => await c.ExecAsync(text, mode)
                );
                HiraganaResult.Text = result;
            }
            catch(Exception ex)
            {
                HiraganaResult.Text = "エラー：" + ex.Message;
            }
            finally
            {
                HiraganaConvertButton.IsEnabled = true;
            }
        }

        private async void MorphExtract(object sender, RoutedEventArgs e)
        {
            try
            {
                MorphExtractButton.IsEnabled = true;
                MorphResult.Text = "解析中";
                var c = new Morph(APIKey);
                Morph.InfoFilter info = 0;
                if (MorphForm.IsChecked == true)
                    info |= Morph.InfoFilter.FORM;
                if (MorphPos.IsChecked == true)
                    info |= Morph.InfoFilter.POS;
                if (MorphRead.IsChecked == true)
                    info |= Morph.InfoFilter.READ;

                string[] list = null;
                if(!string.IsNullOrWhiteSpace(MorphPosFilter.Text))
                    list = MorphPosFilter.Text.Split(',');

                var text = MorphSentence.Text;
                var result = await Task.Run(
                    async () => await c.ExecAsync(text, info, list)
                );
                MorphResult.Text = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                MorphResult.Text = "エラー：" + ex.Message;
            }
            finally
            {
                MorphExtractButton.IsEnabled = true;
            }

        }

        private async void EntityExtract(object sender, RoutedEventArgs e)
        {
            try
            {
                EntityExtractButton.IsEnabled = true;
                EntityResult.Text = "解析中";
                var c = new Entity(APIKey);
                Entity.ClassType filter = 0;
                if (EntityArt.IsChecked == true)
                    filter |= Entity.ClassType.ART;
                if (EntityDat.IsChecked == true)
                    filter |= Entity.ClassType.DAT;
                if (EntityLoc.IsChecked == true)
                    filter |= Entity.ClassType.LOC;
                if (EntityOrg.IsChecked == true)
                    filter |= Entity.ClassType.ORG;
                if (EntityPsn.IsChecked == true)
                    filter |= Entity.ClassType.PSN;
                if (EntityTim.IsChecked == true)
                    filter |= Entity.ClassType.TIM;


                var text = EntitySentence.Text;
                var result = await Task.Run(
                    async () => await c.ExecAsync(text, filter)
                );
                EntityResult.Text = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                EntityResult.Text = "エラー：" + ex.Message;
            }
            finally
            {
                EntityExtractButton.IsEnabled = true;
            }
        }

        private async void SimirarityCalc(object sender, RoutedEventArgs e)
        {
            try
            {
                SimirarityCalcButton.IsEnabled = true;
                SimirarityResult.Text = "変換中";
                var c = new Similarity(APIKey);
                var text1 = SimiraritySentence1.Text;
                var text2 = SimiraritySentence2.Text;
                var result = await Task.Run(
                    async () => await c.ExecAsync(text1, text2)
                );
                SimirarityResult.Text = result.ToString();
            }
            catch (Exception ex)
            {
                SimirarityResult.Text = "エラー：" + ex.Message;
            }
            finally
            {
                SimirarityCalcButton.IsEnabled = true;
            }
        }
    }
}
