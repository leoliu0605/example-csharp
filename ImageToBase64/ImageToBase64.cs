using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace System
{
    public static class ImageToBase64
    {
        public static string ToBase64String(this Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, new JpegEncoder { Quality = 100 });
                var array = ms.ToArray();
                var base64String = Convert.ToBase64String(array);
                return base64String;
            }
        }
    }
}
