using ControlDisplayInputSource;

Console.WriteLine();
Console.WriteLine(Helper.AppTitle);

if (args.Length < 1)
{
    Helper.ExitWithSuccess(true);
}

var isGet = args[0] == "--get";
var isSet = args[0] == "--set";
var isCap = args[0] == "--cap";

if ((isGet && args.Length > 1) || (isSet && args.Length > 2) || (isCap && args.Length > 1))
{
    Helper.ExitWithError(1, "Too many arguments.");
}

try
{
    var mc = new DDCMonitorControl();

    mc.Init();

    if (isGet)
    {
        Console.WriteLine();
        Console.WriteLine($"Display description: {mc.GetMonitorDescription()}");
        Console.WriteLine($"Current VCP60 value: {mc.GetVCP60Value()}");
    }
    else if (isSet)
    {
        if (!uint.TryParse(args[1], out uint value) || value < 1 || value > 65535)
        {
            Helper.ExitWithError(3, "Given argument is not a number between 1 and 65535.");
        }

        Console.WriteLine();
        Console.WriteLine($"Display description: {mc.GetMonitorDescription()}");
        Console.WriteLine($"Set new VCP60 value: {value}");

        mc.SetVCP60Value(value);
    }
    else if (isCap)
    {
        var capString = mc.GetCapabilitiesString();

        var vcp60Numbers = Helper.GetVCP60ValuesAsNumbersFromCapabilitiesString(capString);
        if (string.IsNullOrEmpty(vcp60Numbers))
        {
            Helper.ExitWithError(4, "Could not parse the VCP60 numbers from DDC capabilities string.");
        }

        Console.WriteLine();
        Console.WriteLine($"Display description: {mc.GetMonitorDescription()}");
        Console.WriteLine($"Support for VCP60 #: {vcp60Numbers}");
    }
    else
    {
        Helper.ExitWithError(2, "Unknown argument.");
    }
}
catch (InvalidOperationException e)
{
    var exitCode = e.Data.Contains("code") && e.Data["code"] is int value ? value : 255;

    Helper.ExitWithError(exitCode, e.Message);
}

Helper.ExitWithSuccess(false);
