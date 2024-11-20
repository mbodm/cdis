using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ControlDisplayInputSourceCore
{
    internal enum LPMC_VCP_CODE_TYPE
    {
        MC_MOMENTARY,
        MC_SET_PARAMETER
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct PHYSICAL_MONITOR
    {
        public IntPtr hPhysicalMonitor;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string szPhysicalMonitorDescription;
    }

    internal sealed class WinApi
    {
        public const uint MONITOR_DEFAULTTOPRIMARY = 1;

        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(
            IntPtr hwnd,
            uint dwFlags);

        [DllImport("dxva2.dll")]
        public static extern bool GetNumberOfPhysicalMonitorsFromHMONITOR(
            IntPtr hMonitor,
            out uint pdwNumberOfPhysicalMonitors);

        [DllImport("dxva2.dll")]
        public static extern bool GetPhysicalMonitorsFromHMONITOR(
            IntPtr hMonitor,
            uint dwPhysicalMonitorArraySize,
            [Out] PHYSICAL_MONITOR[] pPhysicalMonitorArray);

        [DllImport("dxva2.dll")]
        public static extern bool GetVCPFeatureAndVCPFeatureReply(
            IntPtr hMonitor,
            byte bVCPCode,
            out LPMC_VCP_CODE_TYPE pvct,
            out uint pdwCurrentValue,
            out uint pdwMaximumValue);

        [DllImport("Dxva2.dll")]
        public static extern bool SetVCPFeature(
            IntPtr hMonitor,
            byte bVCPCode,
            uint dwNewValue);

        [DllImport("Dxva2.dll")]
        public static extern bool GetCapabilitiesStringLength(
            IntPtr hMonitor,
            out uint pdwCapabilitiesStringLengthInCharacters);

        [DllImport("Dxva2.dll")]
        public static extern bool CapabilitiesRequestAndCapabilitiesReply(
            IntPtr hMonitor,
            StringBuilder pszASCIICapabilitiesString,
            uint dwCapabilitiesStringLengthInCharacters);
    }
}
