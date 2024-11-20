# cdis

A tiny Windows command line tool to change a display´s input source by using DDC/CI

![cdis](screenshot.png)

### What is it?

- It's a tiny command line tool for Windows 10/11
- It´s named `cdis.exe` where _cdis_ stands for **C**ontrol**D**isplay**I**nput**S**ource
- It uses DDC/CI to get or set the input source of a DDC-capable computer display
- It does this by using the Windows API
- It's a tiny .NET 4.8 console application and is written in C#
- It's developed by using the modern "SDK-style project format" (even when it's a NET48 tool)
- It's developed with Visual Studio 2022 17.12.1 (Community Edition)
- It's built by using the global dotnet tools from the command line
- It has no dependencies to anything (no 3rd party library, etc.)
- It's built and published as a framework-dependent NET48 assembly/executable
- It doesn't require any runtime-installation (since Windows 10/11 supports NET48 out of the box)
- It has an executable size of ~12KB
- It was the result of my early experiments in the [ControlDisplayInputSource](https://github.com/MBODM/ControlDisplayInputSource) repository
- It is the direct successor of my later [cdis2410](https://github.com/MBODM/cdis2410) and [cdis2410-net48](https://github.com/MBODM/cdis2410-net48) tools
- It's free to use and open source (under MIT license)

### Why it exists?

It's possible to change the input source of a computer's display/monitor by software (in contrast to the hardware buttons of your display/monitor). We can do this by using DDC/CI commands. The DDC VCP60 command is the key component here.

I was just looking for a simple way to quickly switch my display´s input source between 3 computers, with just one click. I found some tools out there, which all use DDC/CI too, of course. But i decided to build a simple and tiny tool by myself, which i can use in scripts or shortcuts (i.e. Windows .lnk shortcuts, pinned to the Windows TaskBar).

As a result, this tool was born.

### Why NET48 and not NET8?


#### Have fun.











 
