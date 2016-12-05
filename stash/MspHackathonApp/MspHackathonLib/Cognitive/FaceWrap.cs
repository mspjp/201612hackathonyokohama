using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspHackathonLib.Cognitive
{
    public class FaceWrap
    {
        public static async Task DetectAsync(Stream stream)
        {
            string faceAPIKey = "275f7ae3c0ca42fda3eca8bee0956fad";
            string imageUrl = "http://hogehoge***.jpg";
            var client = new FaceServiceClient(faceAPIKey);
            var faces = await client.DetectAsync(imageUrl,true,true);
            foreach(var face in faces)
            {
                //顔の座標
                var top = face.FaceRectangle.Top;
                var left = face.FaceRectangle.Left;

                //顔のパーツの座標
                var eyeleftinner = face.FaceLandmarks.EyeLeftInner;
            }
        }
    }
}
