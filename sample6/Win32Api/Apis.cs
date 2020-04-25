using System;
using System.Runtime.InteropServices;

namespace Sample6.Win32Api
{
    public static class Apis
    {
        #region DllImports

        [DllImport("user32")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwLong);

        #endregion

        public static void ChangeSystemMenuVisible(IntPtr hwnd, bool visible)
        {
            int style = GetWindowLong(hwnd, Constant.GWL_STYLE);
            if (visible)
            {
                style |= Constant.WS_SYSMENU;
            }
            else
            {
                style &= ~Constant.WS_SYSMENU;
            }

            SetWindowLong(hwnd, Constant.GWL_STYLE, style);
        }
    }
}
