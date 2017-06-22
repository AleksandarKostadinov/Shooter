﻿namespace Shooter
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public struct IconInfo
    {
        public bool fIcon;
        public int xHotSpot;
        public int yHotspot;
        public IntPtr hbmMask;
        public IntPtr hbmColor;
    }

    class CustomCursor
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        public static System.Windows.Forms.Cursor CreateCursor(Bitmap bmp, int xHotspot, int yHotspot)
        {
            IntPtr ptr = bmp.GetHicon();
            IconInfo tmp = new IconInfo();

            GetIconInfo(ptr, ref tmp);

            tmp.xHotSpot = xHotspot;
            tmp.yHotspot = yHotspot;
            tmp.fIcon = false;

            ptr = CreateIconIndirect(ref tmp);

            return new System.Windows.Forms.Cursor(ptr);
        }
    }
}
