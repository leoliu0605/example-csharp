using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace xBitmap;

class Program
{
    [STAThread] // Required for Windows Forms
    static async Task Main(string[] args)
    {
        HttpClient httpClient = new HttpClient();
        Stream stream = await httpClient.GetStreamAsync(
            "https://images.pexels.com/photos/1386422/pexels-photo-1386422.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
        );

        var image = Image.FromStream(stream);
        var bitmap = ImageToGrayBitmap(image);

        ImageShow(image, "Original Image");
        ImageShow(bitmap, "Grayscale Image");
        Application.Run();
    }

    private static void ImageShow(Image image, string? title = null)
    {
        var form = new Form();
        var picture_box = new PictureBox();

        // Set the title of the form
        form.Text = title;

        // Set the size of the form to the size of the image or the screen size, whichever is smaller
        int formWidth = Math.Min(image.Width, Screen.PrimaryScreen.WorkingArea.Width);
        int formHeight = Math.Min(image.Height, Screen.PrimaryScreen.WorkingArea.Height);

        form.ClientSize = new Size(formWidth, formHeight);
        form.StartPosition = FormStartPosition.CenterScreen;

        // Set the PictureBox to be the same size as the image
        picture_box.SizeMode = PictureBoxSizeMode.AutoSize;
        picture_box.Image = image;

        // Use AutoScroll on the form to allow for scrolling large images
        form.AutoScroll = true;
        form.Controls.Add(picture_box);

        // Use Show instead of ShowDialog to avoid blocking
        form.Show();

        form.FormClosed += (sender, e) =>
        {
            if (Application.OpenForms.Count == 0)
            {
                Application.ExitThread();
            }
        };
    }

    public static Bitmap ImageToGrayBitmap(Image image)
    {
        var width = image.Width;
        var height = image.Height;
        var pxs = new byte[width * height];
        var img = new Bitmap(image);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var color = img.GetPixel(x, y);
                var gray = (byte)((color.R + color.G + color.B) / 3);
                pxs[y * width + x] = gray;
            }
        }

        var bitmap = new Bitmap(
            width,
            height,
            System.Drawing.Imaging.PixelFormat.Format8bppIndexed
        );
        BitmapData bmpData = bitmap.LockBits(
            new Rectangle(0, 0, width, height),
            ImageLockMode.WriteOnly,
            bitmap.PixelFormat
        );

        var stride = bmpData.Stride;
        var offset = stride - width;
        var ptr = bmpData.Scan0;
        var scanbytes = stride * height;

        var posScan = 0;
        var posReal = 0;
        var values = new byte[scanbytes];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                values[posScan++] = pxs[posReal++];
            }
            posScan += offset;
        }
        System.Runtime.InteropServices.Marshal.Copy(values, 0, ptr, scanbytes);
        bitmap.UnlockBits(bmpData);

        var palette = bitmap.Palette;
        for (int i = 0; i < 256; i++)
        {
            palette.Entries[i] = Color.FromArgb(i, i, i);
        }
        bitmap.Palette = palette;
        return bitmap;
    }
}
