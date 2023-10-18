# How To

1. Clone the repo or download the source.

2. Find your target device MAC address (CMD > getmac)

3. Edit this string with your address:
```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    // Enter the target devices MAC address here.
    WakeOnLan("00-00-00-00-00-00");
}
```
4. Build for release or keep it in Debug and hit F5.

---
# Bixby Support?

Yes! Open your Bixby app and locate the 'Quick Commands' tab, from there add a new command, for example 'Start My Computer', the following action would be 'Open WOL'. You can add as many different varients of the command as you need or don't use Bixby at all.

---
# Doesn't Work?

  [Windows 10/11 How To Enable Wake On Lan](https://www.windowscentral.com/how-enable-and-use-wake-lan-wol-windows-10)

  [How To Enable Wake On Lan For MSI Motherboards](https://us.msi.com/faq/motherboard-503#:~:text=In%20Windows%20device%20manager%2C%20go,device%20to%20wake%20the%20computer%5D)

---
# Can I Contribute?
Yes, you're free to repurpose the source, contribute, fork or do whatever you need with it.
