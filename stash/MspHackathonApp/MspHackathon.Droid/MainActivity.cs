using Android.App;
using Android.Widget;
using Android.OS;
using MspHackathonLib.Cognitive;
using Android.Content;
using Android.Provider;
using Android.Net;
using Android.Runtime;
using System.IO;

namespace MspHackathon.Droid
{
    [Activity(Label = "MspHackathon.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        Java.IO.File savedDir = null;
        Java.IO.File savedFile = null;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            savedDir = new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures),"MspHackathon");
            if (!savedDir.Exists())
            {
                savedDir.Mkdirs();
            }
            savedFile = new Java.IO.File(savedDir,"testpicture.jpg");
            var button1 = FindViewById<Button>(Resource.Id.button1);
            button1.Click += (s, e) =>
            {
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                
                intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(savedFile));
                StartActivityForResult(intent, 0);
                //
               
            };
            
            // Set our view from the "main" layout resource
           
        }

        protected override async void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contentUri = Uri.FromFile(savedFile);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);

            Android.Graphics.Bitmap bmp = Android.Graphics.BitmapFactory.DecodeFile(savedFile.Path);

            string newPath = savedFile.Path.Replace(".jpg", ".png");
            using (var fs = new FileStream(newPath, FileMode.OpenOrCreate))
            {
                bmp.Compress(Android.Graphics.Bitmap.CompressFormat.Png, 100, fs);
                await FaceWrap.DetectAsync(fs);
            }

            ///var stream = new FileStream(newPath,FileMode.Open);
            
        }
    }
}

