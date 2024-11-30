### _How to switch the display input source by using the keyboard?_

Since `cdis.exe` is a CLI tool, you can use it in scripts and shortcuts. Personally, i created myself 2 different `cdis.exe --set [NUMBER]` Windows Desktop shortcuts (".lnk" files). One for each of my 2 other computers. For each Windows Desktop shortcut i defined a keyboard hotkey (right-click and select "Properties"). In example, something like "CTRL+ALT+LEFTARROW". This way i just need to press those keys on the keyboard to quickly switch to another computer. Hint: You can also pin Windows Desktop shortcuts to "Start" or to the Windows TaskBar.

The only downside of this "trick": Sometimes Windows adds a small delay (1-2 seconds), when using the keyboard hotkey shortcut. You can read more about it in this [superuser blogpost](https://superuser.com/questions/426947/slow-windows-desktop-keyboard-shortcuts) to see "_why?_" and which solutions exist.

But if you don't care about the Windows 1-2 seconds delay, it's totally fine to stick with the simple solution above. Otherwise you may be interessted in the following solution. If so, keep on reading.

This is, how i solved it:
- I use [AutoHotkey](https://www.autohotkey.com) for this
- Download the pure `AutoHotkey64.exe` file (portable version) into some folder
- Create a text file named `Keyboard.ahk` in that folder (file name doesn't matter)
- We edit and fill this text file later
- Create a Windows Desktop Shortcut (".lnk" file) for the `AutoHotkey64.exe`
- Right-click the Windows Desktop Shortcut and select "Properties"
- Add the text file name (`Keyboard.ahk` in this case) as parameter to the `AutoHotkey64.exe` call
- This tells AutoHotkey -> "_When you start, read all the commands from Keyboard.ahk file!_"
- Now move the Windows Desktop Shortcut into the startup folder
  - Just press "WIN+R" and type in "shell:startup" and press "ENTER"
  - This opens the startup folder
  - Hint -> Startup folder is user-related
  - Therefore make sure you are currently logged in as the specific user you want to add this for

This way you start AutoHotkey automatically, when you login with your user. AutoHotkey then reads all commands from a text file (in this case `Keyboard.ahk`). Now let's add some AutoHotkey commands to the text file:

```
^!Right::Run "C:\Tools\cdis.exe --set 3"
^!Left::Run "C:\Tools\cdis.exe --set 17"
^!Up::Run "C:\Tools\cdis.exe --set 15"
```

All 3 commands here do something rather simple:
- Register a keyboard hotkey (see AutoHotkey [docs](https://www.autohotkey.com/docs/v2) for more information)
- In example "^!Right" means "CTRL+ALT+RIGHTARROW"
- Register a "Run" command for each keyboard hotkey
- The Run command here says "_Start `cdis.exe` with appropriate input source VCP60 value_"

This way you register 3 keyboard hotkeys when you login. And whenever you press that keyboard hotkey `cdis.exe` starts and activate the appropriate input source of your display.

You can edit the text file and adjust it accordingly, to
- set the correct path where your `cdis.exe` is located
- set the correct VCP60 values for your display
- set the keyboard keys you wish to use

And that's it. Now you can press your choosed keys on your keyboard and your choosed corresponding input source of your display will be activated. Without any delay. For me, it works like a charm. Thanks to AutoHotkey. And if you need more information about AutoHotkey itself, or the used commands, or what you can do else with AutoHotkey (spoiler: A LOT!), just visit the [AutoHotkey](https://www.autohotkey.com) page.
