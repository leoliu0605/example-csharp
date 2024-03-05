using System.Management;
using UsbDeviceDescript;

namespace WinUSB;

class Program
{
    private static Splash.IO.PORTS.USB? _watcher = null;
    public static event EventHandler<UsbEventArgs>? UsbInsertEvent = null;
    public static event EventHandler<UsbEventArgs>? UsbRemoveEvent = null;

    static void Main(string[] args)
    {
        foreach (var item in GetUsbs())
        {
            Console.WriteLine(item.ToString());
        }
        if (_watcher is null)
        {
            _watcher = new Splash.IO.PORTS.USB();
            _watcher.AddUSBEventWatcher(
                USBEventHandler,
                USBEventHandler,
                new TimeSpan(0, 0, 0, 0, 1)
            );
        }
    }

    private static UsbAttribute[] GetUsbs()
    {
        var list = new List<UsbAttribute>();
        if (USBList.AllUsbDevices != null)
        {
            foreach (PnPEntityInfo item in USBList.AllUsbDevices)
            {
                ushort vid = item.VendorID;
                ushort pid = item.ProductID;
                string name = item.Name;
                list.Add(new UsbAttribute(vid, pid, name));
            }
        }
        return list.ToArray();
    }

    private static void USBEventHandler(Object sender, EventArrivedEventArgs e)
    {
        var list = GetUsbs();
        if (e.NewEvent.ClassPath.ClassName == "__InstanceCreationEvent")
        {
            if (UsbInsertEvent != null)
            {
                var args = new UsbEventArgs(string.Empty, list);
                UsbInsertEvent(null, args);
            }
        }
        else if (e.NewEvent.ClassPath.ClassName == "__InstanceDeletionEvent")
        {
            if (UsbRemoveEvent != null)
            {
                var args = new UsbEventArgs(string.Empty, list);
                UsbRemoveEvent(null, args);
            }
        }
    }

    public sealed class UsbEventArgs : EventArgs
    {
        public UsbEventArgs(string message, params UsbAttribute[] usbs)
        {
            Message = message;
            UsbAttributes = usbs;
        }

        public string Message { get; private set; }
        public UsbAttribute[] UsbAttributes { get; private set; }
    }

    public class UsbAttribute : Attribute
    {
        private readonly ushort ProductID;
        private readonly ushort VendorID;

        public UsbAttribute(ushort vid, ushort pid)
        {
            this.VendorID = vid;
            this.ProductID = pid;
            this.Name = string.Empty;
        }

        public UsbAttribute(ushort vid, ushort pid, string name)
        {
            this.VendorID = vid;
            this.ProductID = pid;
            this.Name = name;
        }

        public (ushort VID, ushort PID) ID => (this.VendorID, this.ProductID);

        public string Name { get; }

        public override string ToString()
        {
            return $"\"{Name,-35} -> VendorID=0x{ID.VID:X4} / ProductID=0x{ID.PID:X4}\"";
        }
    }
}
