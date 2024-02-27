namespace LogHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DEBUG_Print("Hello, World!");
        }

        private static string _sourceName = ".Net Runtime";

        public static void DEBUG_Print(string message, System.Diagnostics.EventLogEntryType type = System.Diagnostics.EventLogEntryType.Information)
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                System.Diagnostics.EventLog.WriteEntry(_sourceName, message, type);
            }
            else
            {
                throw new NotSupportedException("Not supported on this platform");
            }
        }
    }
}
