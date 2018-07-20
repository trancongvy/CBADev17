using System;
using System.Collections.Generic;
using System.Text;
using LibUsbDotNet;
using LibUsbDotNet.Main;
namespace UsbDriver
{
    class Class2
    {

        LibUsbDotNet.Main.UsbTransfer a;
        public static UsbDeviceFinder MyUsbFinder;
        public Class2()
        {
            
        }
        public void Thu()
        {
            ErrorCode ec = ErrorCode.None;
            UsbRegDeviceList lu = LibUsbDotNet.UsbDevice.AllDevices;
            MyUsbFinder = new UsbDeviceFinder(1614, 33024);
            LibUsbDotNet.UsbDevice  MyUsbDevice=null;
            try
            {
                // Find and open the usb device.
                MyUsbDevice = UsbDevice.OpenUsbDevice(MyUsbFinder);
                //MyUsbDevice = lu[0].Device;
                // If the device is open and ready
                if (MyUsbDevice == null) throw new Exception("Device Not Found.");

                // If this is a "whole" usb device (libusb-win32, linux libusb)
                // it will have an IUsbDevice interface. If not (WinUSB) the
                // variable will be null indicating this is an interface of a
                // device.
                
                IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                if (!ReferenceEquals(wholeUsbDevice, null))
                {
                    // This is a "whole" USB device. Before it can be used,
                    // the desired configuration and interface must be selected.

                    // Select config
                    wholeUsbDevice.SetConfiguration(1);

                    // Claim interface
                    wholeUsbDevice.ClaimInterface(1);
                }

                // open read endpoint
                UsbEndpointReader reader =
                    MyUsbDevice.OpenEndpointReader(ReadEndpointID.Ep02);

                // open write endpoint0123456789
                UsbEndpointWriter writer =
                    MyUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep03);

                // write data, read data
                int bytesWritten;
                //ec = writer.Write(new byte[] { 0x00, 0x03, 0x00, 0x00, 0x00,
           // 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }, 2000, out bytesWritten);

                //if (ec != ErrorCode.None)
                //    throw new Exception(UsbDevice.LastErrorString);
                ec = ErrorCode.None;

                byte[] readBuffer = new byte[1024];
                while (ec == ErrorCode.None)
                {
                    int bytesRead;

                    // If the device hasn't sent data in the last 100 milliseconds,
                    // a timeout error (ec = IoTimedOut) will occur.
                    ec = reader.Read(readBuffer, 100, out bytesRead);

                    if (bytesRead == 0) throw new Exception("No more bytes!");

                    // Write that output to the console.
                  //  PrintHex(readBuffer, bytesRead);
                }

                //Console.WriteLine("\r\nDone!\r\n");
            }
            catch (Exception ex)
            {
                //Console.WriteLine();
                //Console.WriteLine((ec != ErrorCode.None ? ec + ":" : String.Empty) + ex.Message);
            }
            finally
            {
                if (MyUsbDevice != null)
                {
                    if (MyUsbDevice.IsOpen)
                    {
                        // If this is a "whole" usb device (libusb-win32, linux libusb-1.0)
                        // it exposes an IUsbDevice interface. If not (WinUSB) the
                        // 'wholeUsbDevice' variable will be null indicating this is
                        // an interface of a device; it does not require or support
                        // configuration and interface selection.
                        IUsbDevice wholeUsbDevice = MyUsbDevice as IUsbDevice;
                        if (!ReferenceEquals(wholeUsbDevice, null))
                        {
                            // Release interface
                            wholeUsbDevice.ReleaseInterface(1);
                        }

                        MyUsbDevice.Close();
                    }
                    MyUsbDevice = null;

                    // Free usb resources
                    UsbDevice.Exit();

                }
            }
        }
        
    }
}
