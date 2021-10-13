# WakeOnLan
 A simple android application which simply sends a magic packet to the target MAC address waking the device up.
 
Clone the repo, connect your device, open Visual Studio (2019+) and build to the device, making sure you input your devices MAC address into the method call during OnCreate.
The application will destroy itself when run, you can setup Bixby to execute the application via voice or launch it manually to send ther WOL packet.
