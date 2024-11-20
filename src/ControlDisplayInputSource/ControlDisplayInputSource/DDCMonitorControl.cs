using System;
using System.Text;

namespace ControlDisplayInputSource
{
    // Since you can do this Windows DDC stuff here more or less in just one specific way,
    // there is no real benefit in "programming against an abstraction" (interface), etc.

    internal sealed class DDCMonitorControl
    {
        private IntPtr hPhysicalMonitor = IntPtr.Zero;
        private string szPhysicalMonitorDescription = string.Empty;

        public bool IsInitialized { get; private set; }

        public void Init()
        {
            if (!IsInitialized)
            {
                var hWindow = DDCWinApi.GetDesktopWindow();
                var hMonitor = DDCWinApi.MonitorFromWindow(hWindow, DDCWinApi.MONITOR_DEFAULTTOPRIMARY);

                if (!DDCWinApi.GetNumberOfPhysicalMonitorsFromHMONITOR(hMonitor, out uint numberOfPhysicalMonitors))
                {
                    ThrowException("Could not determine number of physical monitors.", 50);
                }

                if (numberOfPhysicalMonitors <= 0)
                {
                    ThrowException("Could not found any physical monitor.", 51);
                }

                var physicalMonitors = new PHYSICAL_MONITOR[numberOfPhysicalMonitors];

                if (!DDCWinApi.GetPhysicalMonitorsFromHMONITOR(hMonitor, numberOfPhysicalMonitors, physicalMonitors))
                {
                    ThrowException("Could not determine primary physical monitor.", 52);
                }

                hPhysicalMonitor = physicalMonitors[0].hPhysicalMonitor;
                szPhysicalMonitorDescription = physicalMonitors[0].szPhysicalMonitorDescription;

                IsInitialized = true;
            }
        }

        public string GetMonitorDescription()
        {
            Init();

            return szPhysicalMonitorDescription;
        }

        public uint GetVCP60Value()
        {
            Init();

            if (!DDCWinApi.GetVCPFeatureAndVCPFeatureReply(hPhysicalMonitor, 0x60, out _, out uint currentValue, out _))
            {
                ThrowException("Could not get current VCP60 value.", 53);
            }

            return currentValue;
        }

        public void SetVCP60Value(uint value)
        {
            Init();

            if (!DDCWinApi.SetVCPFeature(hPhysicalMonitor, 0x60, value))
            {
                ThrowException("Could not set VCP60 value.", 54);
            }
        }

        public string GetCapabilitiesString()
        {
            Init();

            var result = DDCWinApi.GetCapabilitiesStringLength(hPhysicalMonitor, out uint pdwCapabilitiesStringLengthInCharacters);
            if (!result || pdwCapabilitiesStringLengthInCharacters == 0)
            {
                ThrowException("Could not determine length of DDC capabilities string.", 55);
            }

            var pszASCIICapabilitiesString = new StringBuilder((int)pdwCapabilitiesStringLengthInCharacters);

            if (!DDCWinApi.CapabilitiesRequestAndCapabilitiesReply(hPhysicalMonitor, pszASCIICapabilitiesString, pdwCapabilitiesStringLengthInCharacters))
            {
                ThrowException("Could not determine DDC capabilities.", 56);
            }

            return pszASCIICapabilitiesString.ToString();
        }

        private static void ThrowException(string message, int code)
        {
            var e = new InvalidOperationException(message);

            e.Data.Add(nameof(code), code);

            throw e;
        }
    }
}
