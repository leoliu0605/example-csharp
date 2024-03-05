namespace WinProgressBar;

class Program
{
    [STAThread] // Required for Windows Forms
    static void Main(string[] args)
    {
        var progress1 = new ProgressBarText
        {
            Value = 0,
            Maximum = 100,
            VisualMode = ProgressBarDisplayMode.CustomText,
            CustomText = "CustomText Demo",
            Size = new Size(280, 23),
            Location = new Point(0, 0)
        };
        var progress2 = new ProgressBarText
        {
            Value = 0,
            Maximum = 100,
            VisualMode = ProgressBarDisplayMode.TextAndPercentage,
            CustomText = "Text + Percentage Demo",
            Size = new Size(280, 23),
            Location = new Point(0, 30)
        };

        var form = new Form
        {
            Text = "WinProgressBar",
            Size = new Size(300, 100),
            FormBorderStyle = FormBorderStyle.FixedSingle,
            StartPosition = FormStartPosition.CenterScreen,
            Controls = { progress1, progress2 }
        };
        form.FormClosed += (sender, e) =>
        {
            if (Application.OpenForms.Count == 0)
            {
                Application.ExitThread();
            }
        };
        Task.Factory.StartNew(() =>
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                form.Invoke(() =>
                {
                    progress1.Value++;
                    progress1.CustomText = $"Progress: {progress1.Value}%";
                    progress2.Increment(1);
                });
            }
        });
        form.ShowDialog();
        Application.Run();
    }
}
