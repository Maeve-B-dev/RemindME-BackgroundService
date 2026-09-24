# RemindME - BackgroundService
A simple Background Service to remind user of certain tasks using auditive signals. Lightweight implementation(~15MB of ram).
Will remind the user in a defined intervall using a custom audio file.

## Support
Supported OS: Windows(10+), Linux(Tested on Debian 13 and Arch)

Supported Audio Formats: .wav only

## Installation
Download the latest version from [Packages](https://github.com/Maeve-B-dev/RemindME-BackgroundService/releases) and unzip it into your desired installation folder.

### Windows
**Command-Line usage:**

This will allow you to see the Worker Logs directly. Usefull when changing config.

Run `ReminderBackgroundService.exe`

**Background Service usage:**

The program will run invisibly in the background. Useful for everyday usage. Uses less ram.

Starting: Run `BackgroundStart.vbs`. Allow the programm to run as Administrator(Necessary to add/remove a background service). You should see a PopUp confirming the start of the service.

Stopping: Run `BackgroundStop.vbs`. You can also stop & delete `ReminderBackgroundServices` using either `sc.exe` or `services.msc`

### Linux
**Command-Line usage:**

This will allow you to see the Worker Logs directly. Usefull when changing config.

Make `ReminderBackgroundService` executable by opening a terminal in your installation directory and running `sudo chmod +x ./ReminderBackgroundService`. Execute it by running `./ReminderBackgroundService`.

**Background Service usage:**

The program will run invisibly in the background. Useful for everyday usage. Uses less ram.

Starting: Run `start.sh`. The programm will create a new directory containing the PID of the process, should you need it.

Stopping: Run `stop.sh`. `/tmp/` will be deleted!

## Configuration
In Appsettings.json, modify any Variables under "AppSettings". The following Variables are available:

TimeInMS: Set the intervall in MS, e.g. 90.000(for 15 minute intervalls between reminders).

filePath: Relative path to .wav file, e.g. ./alert.wav(provided with Program)

A restart is required for changes to take effect.