namespace WinFolderBrowserDialog;

class Program
{
    static void Main(string[] args)
    {
        var dialog = new FolderBrowserEx.FolderBrowserDialog
        {
            Title = "Select a folder",
            InitialFolder = @"C:\",
            AllowMultiSelect = true
        };

        if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            foreach (var folder in dialog.SelectedFolders)
            {
                Console.WriteLine(folder);
            }
        }
        else
        {
            Console.WriteLine("No folder selected");
        }
    }
}
