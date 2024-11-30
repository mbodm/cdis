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

The tool itself should be rather self-explanatory. Just use the `--cap` argument to see which VCP60 values your display supports. Each VCP60 value represents one physical input source of your display. Either you just test each VCP60 value (by using the `--set` argument), or you take a look into your display's user manual, to find out which VCP60 value corresponds to which physical input source. By using the `--get` argument you can see which VCP60 value (and therefore the corresponding input source) is currently active.

### Nice, anything else?

"_How to switch the display input source by using the keyboard?_"

A quick personal suggestion (this is how i use the tool):

Since `cdis.exe` is a CLI tool, you can use it in scripts and shortcuts. Personally, i created myself 2 different `cdis.exe --set [NUMBER]` Windows Desktop shortcuts (".lnk" files). One for each of my 2 other computers. For each Windows Desktop shortcut i defined a keyboard hotkey (right-click and select "Properties"). In example, something like "SHIFT+ALT+LEFTARROW". This way i just need to press those keys on the keyboard to quickly switch to another computer. Hint: You can also pin Windows Desktop shortcuts to "Start" or to the Windows TaskBar.

The only downside of this "trick": Sometimes Windows adds a small delay (1-2 seconds), when using the keyboard hotkey shortcut. You can read more about it in this [superuser blogpost](https://superuser.com/questions/426947/slow-windows-desktop-keyboard-shortcuts) to see "_why?_" and which solutions exist.

This is, how i solved it:
- I use AutoHotkey for this
- Download the pure "AutoHotkey64.exe" file (portable version) into some folder
- Create a text file named "Keyboard.ahk" in that folder (file name doesn't matter)
- We edit and fill this text file later
- Create a Windows Desktop Shortcut (".lnk" file) for the "AutoHotkey64.exe"
- Right-click the Windows Desktop Shortcut and select "Properties"
- Add the text file name ("Keyboard.ahk" in this case) as parameter to the "AutoHotkey64.exe" call
- This tells AutoHotkey -> "_When you start, read all the commands from Keyboard.ahk file!_"
- Now move the Windows Desktop Shortcut into the startup folder
  - Just press "WIN+R" and type in "shell:startup" and press "ENTER"
  - This opens the startup folder
  - Hint -> Startup folder is user-related
  - Therefore make sure you are currently logged in as the specific user you want to add this for

This way you start AutoHotkey automatically, when you login with your user. AutoHotkey then reads all commands from a text file (in this case "Keyboard.ahk"). Now let's add some AutoHotkey commands to the text file:

```
^!Right::Run "C:\Tools\cdis.exe --set 3"
^!Left::Run "C:\Tools\cdis.exe --set 17"
^!Up::Run "C:\Tools\cdis.exe --set 15"
```

All 3 commands here do something rather simple:
- Register a keyboard hotkey (see AutoHotkey docs for more information)
- In example "^!Right" means "CTRL+ALT+RIGHTARROW"
- Register a "Run" command for each keyboard hotkey
- The Run command here says "_Start cdis.exe with appropriate input source VCP60 value_"

This way you register 3 keyboard hotkeys when you login. And whenever you press that keyboard hotkey cdis starts and activate the appropriate input source of your display.

You can edit the text file and adjust it accordingly, to
- set the correct path where your "cdis.exe" is located
- set the correct VCP60 values for your display
- set the keyboard keys you wish to use

And that's it. Now you can press your choosed keys on your keyboard and your choosed corresponding input source of your display will be activated. Without any delay. For me, it works like a charm. Thanks to AutoHotkey. And if you need more information about AutoHotkey itself, or the used commands, or what you can do else with AutoHotkey (spoiler: A LOT!), just visit the [AutoHotkey](https://www.autohotkey.com) page.

### Why .NET Framework 4.8?

You will find a detailed answer to that question [here](https://github.com/mbodm/cdis/blob/main/NET48.md).

### Earlier Windows versions?

So, what about running `cdis.exe` on Windows versions, older than Windows 10/11?

##### Windows 7/8

In general, this tool runs on Windows 7/8 as well, by default. You just need to separately install the .NET Framework 4.8 runtime on those Windows versions (you can download the runtime from Microsoft). That's all.

##### Windows Vista and earlier

To make it run on even older Windows versions, you just need to download the source, set an older target framework (in example .NET Framework 4.6) and re-compile it. You can find more information [here](https://learn.microsoft.com/en-us/dotnet/framework/migration-guide/versions-and-dependencies).

**Have fun.**
