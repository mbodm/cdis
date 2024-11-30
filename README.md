# cdis

A tiny Windows command line tool to change a display's input source by using DDC/CI

![cdis](screenshot.png)

### What is it?

- It's a tiny command line tool for Windows 10/11
- It's named `cdis.exe` where "cdis" stands for "**C**ontrol**D**isplay**I**nput**S**ource"
- It uses DDC/CI to control the input source of a DDC-capable display/monitor
- It does this by using the Windows API
- It runs on any Windows 10/11 machine
- Its release binary (executable) has a size of ~12KB
- It's a .NET Framework 4.8 console application and it's written in C#
- It has no dependencies to anything (no 3rd party library, etc.)
- It's developed by using the more modern SDK-style project format in ".csproj" file
- It's developed with Visual Studio 2022 17.12.1 (Community Edition)
- It's built by using the global `dotnet` tools on the command line
- It's built on a Windows 11 machine (Version 23H2)
- It's built and published as a .NET Framework 4.8 executable (_framework-dependent_)
- It doesn't require any runtime-installation (Windows 10/11 has the runtime by default)
- It's the result of my early [ControlDisplayInputSource](https://github.com/MBODM/ControlDisplayInputSource) experiments
- It's the direct successor of my [cdis2410](https://github.com/MBODM/cdis2410) and [cdis2410-net48](https://github.com/MBODM/cdis2410-net48) tools
- It's free to use and open source (under MIT license)

### Why it exists?

It's possible to change the input source of a computer's display by software (in contrast to the hardware buttons of your display). Typically by using DDC/CI commands. The DDC VCP60 command is the key component here.

I was just looking for some simple way to quickly switch my display's input source between 3 computers, with just one click. I found some tools out there (which all use DDC/CI too, of course). But i decided to build a simple and tiny tool by myself, which i can use in scripts or Windows Desktop shortcuts.

As a result, this tool was born.

### How to use?

The tool itself should be rather self-explanatory.

- Just use the `--cap` argument to see which VCP60 values your display supports. Each VCP60 value represents one physical input source of your display. Either you just test each VCP60 value (by using the `--set` argument), or you take a look into your display's user manual, to find out which VCP60 value corresponds to which physical input source.
- By using the `--get` argument you can see which VCP60 value (and therefore the corresponding input source) is currently active.
- By using the `--set` argument you can set the VCP60 value (and therefore the corresponding input source).

### Nice, anything else?

"_How to switch the display input source by using the keyboard?_"

A quick personal suggestion (this is how i use the tool), and the answer to above question, you can find [here](https://github.com/mbodm/cdis/blob/main/README_keyboard.md).

### Why .NET Framework 4.8?

You will find a detailed answer to that question [here](https://github.com/mbodm/cdis/blob/main/README_net48.md).

### Earlier Windows versions?

So, what about running `cdis.exe` on Windows versions, older than Windows 10/11?

##### Windows 7/8

In general, this tool runs on Windows 7/8 as well, by default. You just need to separately install the .NET Framework 4.8 runtime on those Windows versions (you can download the runtime from Microsoft). That's all.

##### Windows Vista and earlier

To make it run on even older Windows versions, you just need to download the source, set an older target framework (in example .NET Framework 4.6) and re-compile it. You can find more information [here](https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/versions-and-dependencies).

### Support for multiple displays?

Sorry, full multi-monitor support is somewhat "spicy", to do it the right way. Correctly determine all monitors, handling Desktop with "Extend" and "Duplicate" modes, and name/control all devices correctly, ends up rather complex on the Windows platform.

Therefore:
- `cdis.exe` doesn't offer multi-monitor control
- `cdis.exe` will always use the primary monitor (of the active Desktop)

Check out the other DDC/CI tools out there, like in example NirSoft's fantastic  [ControlMyMonitor](https://www.nirsoft.net/utils/control_my_monitor.html), if you are looking for multi-monitor support. Also [ddcutil](https://www.ddcutil.com/windows_programs/) has a nice list. Sorry `cdis.exe` can't help you here.

### Windows blocks access?

Typically, when you just start some ".exe" file, which has no certificate and is downloaded from somewhere (like in example from GitHub, in a ".zip" file 😄), Windows will block the access (by showing the blue SmartScreen popup), until you allow the ".exe" (telling Windows "_it's ok_").

To do this, just right-click the ".exe" file in Explorer, select "Properties", check the "Unblock" checkbox and click the OK button. Now you can start your downloaded ".exe" file.

This is nothing special to `cdis.exe` and it's always happening in Windows, for any ".exe" not having a certificate (not many have one). I just thought, it`s worth to mention this here, for the less experienced IT-peoples. You can read about it [here](https://www.windowscentral.com/how-fix-app-has-been-blocked-your-protection-windows-10), or you can watch the first part of [this](https://www.youtube.com/watch?v=0YYWaQSbiVA) video.

### Build the source?

If you wanna build the app on your own, just download the source and execute the `release.bat` script. Feel free to enhance the source, adapt it into your own projects, or whatever. It's under MIT license. So you can do with it, whatever you want. 😜

**Have fun.**
