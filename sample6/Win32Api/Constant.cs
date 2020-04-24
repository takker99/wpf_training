// cf.https://blog.xin9le.net/entry/2013/11/06/021557
namespace Sample6.Win32Api
{
    public class Constant
    {
        //--- GetWindowLong
        public const int GWL_STYLE = -16;
        public const int GWL_EXSTYLE = -20;
        //--- Window Style
        public const int WS_EX_CONTEXTHELP = 0x00400;
        public const int WS_MAXIMIZEBOX = 0x10000;
        public const int WS_MINIMIZEBOX = 0x20000;
        public const int WS_SYSMENU = 0x80000;
        //--- Window Message
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSCOMMAND = 0x0112;
        //--- System Command
        public const int SC_CONTEXTHELP = 0xF180;
        public const long SC_CLOSE = 0xF060L;
        //--- Virtual Keyboard
        public const int VK_F4 = 0x73;
        //--- Constructor
        private Constant() { }
    }
}
