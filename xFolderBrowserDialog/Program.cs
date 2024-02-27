namespace xFolderBrowserDialog;

class Program
{
    static void Main(string[] args)
    {
        var folder = new System.xFolderBrowserDialog()
        {
            Title = "Select a folder",
            Multiselect = true
        };

        if (folder.ShowDialog())
        {
            foreach (var folderName in folder.FolderNames)
            {
                Console.WriteLine(folderName);
            }
        }
    }
}
