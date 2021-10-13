# How To

1. `Clone the repo or download the source.`

2. `Open the .sln in Visual Studio 2019+`

3. `Connect your Android device.`

4. `Enter the target address:` 
```csharp
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // Enter the target devices MAC address here.
            WakeOnLan("00-00-00-00-00-00");
        }
```

5. `Build for release or keep it in Debug and hit F5.`
---
# Bixby Support?

Yes! Open your Bixby app and locate the 'Quick Commands' tab, from there add a new command, for example 'Start My Computer', the following action would be 'Open WOL'. You can add as many different varients of the command as you need or don't use Bixby at all.
---
# Doesn't Work?

**Make sure your device supports Wake On Lan, ensure it's enabled in the BIOS/OS.**

[How To Enable WOL (Windows 10)](https://www.windowscentral.com/how-enable-and-use-wake-lan-wol-windows-10)

---
# Can I Contribute?
Yes, you're free to repurpose the source, contribute, fork or do whatever you need with it.
