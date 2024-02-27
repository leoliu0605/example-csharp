using System.Runtime.CompilerServices;

namespace LogHelper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DEBUG_Print("Hello, World!");
        }

        private static string _sourceName = ".Net Runtime";

        public static void DEBUG_Print(
            string message,
            System.Diagnostics.EventLogEntryType type =
                System.Diagnostics.EventLogEntryType.Information,
            [CallerMemberName] string? call = null,
            [CallerLineNumber] int line = 0
        )
        {
            Console.WriteLine($"{call}({line}): {message}");
            if (
                System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(
                    System.Runtime.InteropServices.OSPlatform.Windows
                )
            )
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
