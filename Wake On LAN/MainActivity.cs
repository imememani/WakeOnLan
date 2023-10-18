using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;

/* 
 * _=========================================================_
 * | The code is super basic and can be optimised further.   |
 * | I will leave it as is, feel free to modify and improve. |
 * | Ensure changes are made on a seperate branch from main. |
 * |                                                         |
 * | If not working, ensure firewall allows port 9 UDP In!!  |
 * | This is essential otherwise firewall will block packet. |
 * |                                                         |
 * ===========================================================
 * 
 * Helpful links:
 * - Will show what adapter settings are needed, should work for Win11 too.
 * https://www.windowscentral.com/how-enable-and-use-wake-lan-wol-windows-10
 * 
 * MSI Motherboards:
 * - What options need to be changed on an MSI motherboard (should generally apply to all others that support WOL).
 * https://us.msi.com/faq/motherboard-503#:~:text=In%20Windows%20device%20manager%2C%20go,device%20to%20wake%20the%20computer%5D.
 */

namespace WakeOnLan
{
    // To change the display name, replace 'Label = "WOL"' with whatever you want.
    [Activity(Label = "WOL", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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
            // Send a notification.
            SendNotification();

            // Send the packet.
            SendMagicPacket(PhysicalAddress.Parse(address));
        }

        /// <summary>
        /// Send the magic packet to the target MAC address.
        /// </summary>
        private async void SendMagicPacket(PhysicalAddress target)
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

            // Make sure the notification has sent before closing.
            await Task.Delay(50);

            // Kill the app.
            FinishAndRemoveTask();
            Process.KillProcess(Process.MyPid());
        }

        /// <summary>
        /// Quick dirty way to notify action complete.
        /// </summary>
        private void SendNotification()
        {
            // Config.
            int notification_ID = 1100;
            string channel_ID = "WOL";

            // Obtain the notif manager.
            NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);

            // Register the application channel.
            manager.CreateNotificationChannel(new NotificationChannel(channel_ID, "WOL_NOTIFICATION", NotificationImportance.High));

            // Build the notification.
            NotificationCompat.Builder builder = new NotificationCompat.Builder(this, channel_ID)
                                                 .SetContentTitle("PC Power On!")
                                                 .SetContentText("Your computer has been powered on.")
                                                 .SetSmallIcon(Resource.Drawable.icon);

            // Push the notification.
            manager.Notify(notification_ID, builder.Build());
        }
    }
}
