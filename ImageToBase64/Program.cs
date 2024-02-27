using System.Diagnostics;
using SixLabors.ImageSharp;

namespace ImageToBase64;

class Program
{
    static async Task Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();
        Stream stream = await httpClient.GetStreamAsync(
            "https://images.pexels.com/photos/1386422/pexels-photo-1386422.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
        );

        var image = Image.Load(stream);
        var base64String = image.ToBase64String();
        var htmlContent = $"<img src=\"data:image/jpeg;base64,{base64String}\" alt=\"Cat Image\">";
        var filePath = Path.Combine(AppContext.BaseDirectory, "index.html");
        File.WriteAllText(filePath, htmlContent);
        Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        Console.WriteLine("Image converted to base64 and saved to index.html");
    }
}
