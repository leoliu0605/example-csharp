using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Diagnostics;

namespace ImageToBase64;
class Program
{
    static async Task Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();
        Stream stream = await httpClient.GetStreamAsync("https://images.pexels.com/photos/1386422/pexels-photo-1386422.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1");

        var image = Image.Load(stream);
        var base64String = ImageToBase64String(image);
        var htmlContent = $"<img src=\"data:image/jpeg;base64,{base64String}\" alt=\"Cat Image\">";
        File.WriteAllText("index.html", htmlContent);
        Process.Start(new ProcessStartInfo("index.html") { UseShellExecute = true });
        Console.WriteLine("Image converted to base64 and saved to index.html");
    }

    public static string ImageToBase64String(Image image)
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
