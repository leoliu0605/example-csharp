using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using UsbDeviceDescript;

namespace xUSB;

class Program
{
    private static Splash.IO.PORTS.USB _watcher = null;
    public static event EventHandler<UsbEventArgs> UsbInsertEvent = null;
    public static event EventHandler<UsbEventArgs> UsbRemoveEvent = null;

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
        var usb = new List<UsbAttribute>();
        if (USBList.AllUsbDevices != null)
        {
            foreach (PnPEntityInfo List in USBList.AllUsbDevices)
            {
                string name = List.Name;
                ushort vid = List.VendorID;
                ushort pid = List.ProductID;
                usb.Add(new UsbAttribute(name, vid, pid));
            }
        }
        return usb.ToArray();
    }

    private static void USBEventHandler(Object sender, EventArrivedEventArgs e)
    {
        var usbs = GetUsbs();
        if (e.NewEvent.ClassPath.ClassName == "__InstanceCreationEvent")
        {
            if (UsbInsertEvent != null)
            {
                var args = new UsbEventArgs(string.Empty, usbs);
                UsbInsertEvent(null, args);
            }
        }
        else if (e.NewEvent.ClassPath.ClassName == "__InstanceDeletionEvent")
        {
            if (UsbRemoveEvent != null)
            {
                var args = new UsbEventArgs(string.Empty, usbs);
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
        private ushort ProductID;
        private ushort VendorID;

        public UsbAttribute(ushort vid, ushort pid)
        {
            this.VendorID = vid;
            this.ProductID = pid;
        }

        public UsbAttribute(string name, ushort vid, ushort pid)
        {
            this.Name = name;
            this.VendorID = vid;
            this.ProductID = pid;
        }

        public (ushort VID, ushort PID) ID => (this.VendorID, this.ProductID);

        public string Name { get; }

        public override string ToString()
        {
            return $"\"{Name}->VendorID=0x{ID.VID:X4}; ProductID=0x{ID.PID:X4}\"";
        }
    }
}
