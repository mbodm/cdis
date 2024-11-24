### Why .NET Framework 4.8?

Why i publish a tool, in 2024 or later, as a .NET Framework 4.8 (NET48) application, instead of a more modern .NET 8 (NET8) application (which may even make use of AOT compilation)?

I specifically said "publish" here, instead of "develop", since the source code is 99% the same, for both. This is often the case, for rather simple CLI tools, which not make much use of modern C# features (which nonetheless offer great QoL improvements). Otherwise the following thoughts would not come into my mind and would not stand up to below argumentation.

#### Basics

Here are some key facts, you need to know first, to make the decision more easy to explain from my side and more clear to understand on your side:
- In the modern .NET platform "_self-contained_" means "app runs stand-alone".
- In the modern .NET platform "_framework-dependent_" means "app needs a runtime".
- An empty _self-contained_ NET8 console application, with _trim_ option, is ~11MB in size.
- Same _self-contained_ NET8 console application, but _AOT_-compiled, is ~1.3MB in size.
- An empty NET48 console application is ~7KB in size (always _framework-dependent_).
- Windows 10 ("May 2019 Update" or later) includes the NET48 runtime by default.
- Windows 11 (all versions) includes the NET48 runtime by default.
- In 2017 .NET Core 2.0 introduced the new "SDK-style project format" (SSPF).
- Most of the best features of modern .NET, publish/deployment-wise, base on SSPF.
- A NET48 project can also make use of modern SSPF (for "_How?_" see [section](#how-to-use-sdk-style-project-format-with-a-net-48-framework-project) below).

That said, this means you obviously have 2 options if you want to make sure the compiled binary runs on all Windows 10/11 machines out of the box (without any further prerequisites):
- Create a .NET 8 console application and publish it _self-contained_
- Create a .NET Framework 4.8 console application (always _framework-dependent_)

As said above, the latter one runs on any Windows 10/11 machine out of the box, even when _framework-dependent_ in general, cause Windows 10/11 includes the .NET 4.8 Framework runtime and therefore has it installled by default.

Even when the size difference is not that huge, i often decide to stick with the NET48 route. And here is why:
- My prio#1: "_A published binary shall run out of the box, with no further requirements_"
- I solely target Windows 10/11 machines (not that many people use older versions)
- Means: I achieve my prio#1 with both above options (_self-contained_ NET8 or NET48)
- In code there is also zero use of anything special (specific to NET8 or C#12)
- I still have the best publish/deployment features (since NET48 also can use SSPF)

The binary size battle is, of course, easy to win for NET48, since it's _framework_dependent_ in general. Which means all the used .NET functionalities (libraries) will exist outside of the binary, in the separate installed runtime. Whereas for a _self-contained_ .NET app the used functionalities are all compiled into the binary itself. Which results, of course, in a bigger binary.

But i just thought:

"_Hmm, when it completely doesn't matter anyway, why not just stick with the smaller binary size, when i obviously have no other real benefit at all?_"

That's why i often end up using NET48+SSPF, in projects i can not make good use of modern C# features.

#### How to use "SDK-style project format" with a .NET 4.8 Framework project?

As a developer, you need to do the following, to use the more modern "_SDK-sytle project format_" together with .NET 4.8 as framework:
- First create a .NET 8 console application
- Change _Target Framework Moniker_ in project (".csproj" file) from `net8.0` to `net48`
- Remove `<ImplicitUsings>enable</ImplicitUsings>` setting (since it's C# 7.3 now)
- Remove `<Nullable>enable</Nullable>` setting (since it's C# 7.3 now)
- Add missing `using` statements in source files (result of `<ImplicitUsings>` change)
- Convert `Main()` to old style (IntelliSense has a Quick Action to do this for you)
- Publish with default setttings (in a `net48` project you can't change them)

#### Earlier Windows versions

.NET Framework (4.8, 4.6, and so on) supports earlier Windows versions anyway. You just need to install the .NET Framework runtime on that Windows machine. In the worst case you need to download the source, change the target framework (in example from .NET Framework 4.8 to .NET Framework 4.6) and re-compile it.

Fictional: But even when i would stick to the NET8 route, it would be also very simple, to make a console application running on older Windows versions.

Knowing myself, i assume i would not offer published binaries for that Windows versions, out of the box and ready to download, on the "Releases" page. I would be just too lazy to do this by myself, for sure. 😄 Cause the target audience is just not that huge, in my opinion.

But nonetheless, it would be also rather easy to do this on your own: You would just need to download the source and compile it as _framework-dependent_, instead of _self-contained_. Then you could install the separate .NET 8.0 or .NET 6.0 runtime on Windows 7/8 and the CLI tool would run like a charm. You can find more information [here](https://learn.microsoft.com/en-us/dotnet/core/install/windows#windows-7--81--server-2012).

So, there is always good support for earlier Windows versions, regardless what.
