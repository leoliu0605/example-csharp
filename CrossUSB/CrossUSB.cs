using System.Reactive.Linq;
using Device.Net;
using Device.Net.Windows;
using Hid.Net.Windows;
using SerialPort.Net.Windows;
using Usb.Net.Windows;

namespace System
{
    public class CrossUSB
    {
        public enum UsbType
        {
            All,
            Hid
        }

        private static readonly IDeviceFactory _allFactories = new WindowsSerialPortDeviceFactory()
            .Aggregate(WindowsUsbDeviceFactoryExtensions.CreateWindowsUsbDeviceFactory())
            .Aggregate(
                WindowsUsbDeviceFactoryExtensions.CreateWindowsUsbDeviceFactory(
                    classGuid: WindowsDeviceConstants.GUID_DEVINTERFACE_USB_DEVICE
                )
            )
            .Aggregate(WindowsHidDeviceFactoryExtensions.CreateWindowsHidDeviceFactory());

        public CrossUSB() { }

        public static async Task<IEnumerable<ConnectedDeviceDefinition>> AllDevices(
            UsbType type = UsbType.All
        )
        {
            var devices = await _allFactories.GetConnectedDeviceDefinitionsAsync();
            if (type == UsbType.Hid)
                return devices.Where(d => d.DeviceType == DeviceType.Hid);
            return devices;
        }

        public static async Task<IEnumerable<ConnectedDeviceDefinition>> AllDevices(
            uint vendorId,
            uint productId
        )
        {
            var devices = await AllDevices();
            return devices.Where(d => d.VendorId == vendorId && d.ProductId == productId);
        }
    }
}
