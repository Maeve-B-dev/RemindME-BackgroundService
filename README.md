# RemindME - BackgroundService
A simple Background Service to remind user of certain tasks using auditive signals. Lightweight implementation(~15MB of ram).
Will remind the user in a defined intervall using a custom audio file.

## Support
Supported OS: Windows(10+), Linux(Tested on Debian 13 and Arch)

Supported Audio Formats: .wav only

## Installation
Download the latest version from [Packages]() and unzip it into your desired installation folder.

### Windows
**Command-Line usage:**

This will allow you to see the Worker Logs directly. Usefull when changing config.

Run `ReminderBackgroundService.exe`

**Background Service usage:**


### Linux
**Command-Line usage:**

This will allow you to see the Worker Logs directly. Usefull when changing config.

Make `ReminderBackgroundService` executable by opening a terminal in your installation directory and running `sudo chmod +x ./ReminderBackgroundService`. Execute it by running `./ReminderBackgroundService`.

**Background Service usage:**


## Configuration
In Appsettings.json, modify any Variables under "AppSettings". The following Variables are available:

TimeInMS: Set the intervall in MS, e.g. 90.000(for 15 minute intervalls between reminders).

filePath: Relative path to .wav file, e.g. ./alert.wav(provided with Program)

A restart is required for changes to take effect.