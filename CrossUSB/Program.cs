using System.Reactive.Linq;
using Device.Net;

namespace CrossUSB;

class Program
{
    private static async Task Main()
    {
        var devices = await System.CrossUSB.AllDevices(System.CrossUSB.UsbType.All);
        Console.WriteLine("Currently connected devices:\r\n");
        Console.WriteLine(
            string.Join(
                "\r\n",
                devices
                    .OrderBy(d => d.Manufacturer)
                    .ThenBy(d => d.ProductName)
                    .Select(d =>
                        $"{d.Manufacturer} - {d.ProductName} ({d.DeviceType} - {d.ClassGuid})\r\nDevice Path: {d.DeviceId}\r\nVendor: 0x{d.VendorId:X4} Product Id: 0x{d.ProductId:X4}\r\n"
                    )
            )
        );
    }
}
