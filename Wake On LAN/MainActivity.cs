using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

/* 
 * _=========================================================_
 * | The code is super basic and can be optimised further.   |
 * | I will leave it as is, feel free to modify and improve. |
 * | Ensure changes are made on a seperate branch from main. |
 * ===========================================================
 */

namespace WakeOnLan
{
    // To change the display name, replace 'Label = "WOL"' with whatever you want.
    [Activity(Label = "WOL", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Can't call 'SendMagicPacket' directly from here, seems to throw an invalid physical address error.
            // I can't be bothered fixing it so this works.

            // Execute wake and close.
            // Replace this with your target devices MAC address.
            WakeOnLan("00-00-00-00-00-00");
        }

        /// <summary>
        /// Wake the target address up.
        /// </summary>
        private void WakeOnLan(string address)
        {
            // Send the packet.
            SendMagicPacket(PhysicalAddress.Parse(address));

            // Kill the app.
            FinishAndRemoveTask();
            Process.KillProcess(Process.MyPid());
        }

        /// <summary>
        /// Send the magic packet to the target MAC address.
        /// </summary>
        private void SendMagicPacket(PhysicalAddress target)
        {
            // Create the base data for the magic packet.
            IEnumerable<byte> header = Enumerable.Repeat(byte.MaxValue, 6);
            IEnumerable<byte> data = Enumerable.Repeat(target.GetAddressBytes(), 16).SelectMany(mac => mac);

            // Merge the header with the data to form the full string.
            byte[] magicPacket = header.Concat(data).ToArray();

            // Create a new client.
            using UdpClient client = new UdpClient();

            // Send the packet.
            client.Send(magicPacket, magicPacket.Length, new IPEndPoint(IPAddress.Broadcast, 9));

            // Kill the task.
            FinishAndRemoveTask();
            Process.KillProcess(Process.MyPid());
        }
    }
}
