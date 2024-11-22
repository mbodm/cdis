### Why .NET 4.8 and not .NET 8.0?

To make sure the compiled binary runs on all Windows 10/11 machines (without any further pre-requirements) you normally have 2 options:
- Create a .NET 8.0 console application and publish it _self-contained_
- Create a .NET 4.8 console application and publish it _framework-dependent_

The latter one runs on any Windows 10/11 machine out of the box, even when _framework-dependent_ (which means it needs an installed runtime), because Windows 10 and Windows 11 have the .NET 4.8 runtime installled by default.

The resulting binary (".exe" file) sizes are:
- An empty NET8 console application, published as _self-contained_ (with active _trim_ option) is ~11MB in size.
- An empty NET8 AOT console application is ~1.3MB in size.
- An empty NET48 console application, published as _framework_dependent_, is ~7KB in size.

Even when the size difference is not that huge, i decided to stick with the NET48 route. And here is why:

Just deploy the binary itself and it should run out of the box, with no further requirements. This goal is achieved by all options. Therefore i thought "why deploy bigger binaries than needed?"
- My primary goal is: "_The published binary shall run out of the box, with no further requirements_".
- I solely target Windows 10/11 machines (which has the .NET 4.8 framework installed out of the box).
- In code i have no need for anything special (specific to NET8 or C# 12)
- Therefore i have no real benefit of the more modern NET8/C#12 approach

My result was: "_Hmm, when it doesn't matter anyway, why not just take the smaller binary size?_"


The following was done, to use the more modern _SDK-sytle project format_, with .NET 4.8 as framework:
- First created a .NET 8 console application
- Changed _Target Framework Moniker_ in project (".csproj" file) to `net48` (instead of `net8.0`)
- Removed `<ImplicitUsings>enable</ImplicitUsings>` setting there (since it's C# 7.3)
- Removed `<Nullable>enable</Nullable>` setting there (since it's C# 7.3)
- Added missing `using` statements in all source files (as result of above `<ImplicitUsings>` change)
- Published with default setttings (in a `net48` project you can't change them)
- Built on a Windows 10 machine

Result:
- Runs on Windows 11 (all versions) or Windows 10 ("_May 2019 Update_" or later)
- Runs on any Windows (7, 8, or 10 before May 2019) with an installed .NET 4.8 runtime