using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace xmlStructureEditor
{
    public enum TooltipIcon : int
    {
        None,
        Info,
        Warning,
        Error
    }

    class popupTips
    {


        public enum tipAlignment
        {
            TopLeft,
            TopMiddle,
            TopRight,
            LeftMiddle,
            RightMiddle,
            BottomLeft,
            BottomMiddle,
            BottomRight,
        }

        public enum tipPosition
        {            
            Absolute,
            Track
        }

        public delegate void DeActivateEventHandler();

        internal class mToolTip : NativeWindow
        {
            private const int WM_LBUTTONDOWN = 0x0201;
            public event DeActivateEventHandler DeActivate;

            protected override void WndProc(ref System.Windows.Forms.Message m)
            {
                if (m.Msg == WM_LBUTTONDOWN)
                {
                    System.Diagnostics.Debug.WriteLine(m);
                    if (DeActivate != null)
                        DeActivate();
                }
                base.WndProc(ref m);
            }
        }      
        public class tipM : IDisposable
        {
            private mToolTip mTool = null;
            private Control mToolP;
            private TOOLINFO ti;

            private int mtoolMax = 250;
            private string tipmText, tipmTitle;
            private TooltipIcon tipmTI = TooltipIcon.None;
            private tipAlignment tipmAlign = tipAlignment.TopRight;
            private bool tipmPos, tipmCen = false;

        
            [DllImport("User32", SetLastError = true)]
            private static extern int SetWindowPos(
                IntPtr hWnd,
                IntPtr hWndInsertAfter,
                int X,
                int Y,
                int cx,
                int cy,
                int uFlags);

            [DllImport("User32", SetLastError = true)]
            private static extern int GetClientRect(
                IntPtr hWnd,
                ref RECT lpRect);

            [DllImport("User32", SetLastError = true)]
            private static extern int ClientToScreen(
                IntPtr hWnd,
                ref RECT lpRect);

            [DllImport("User32", SetLastError = true)]
            private static extern int SendMessage(
                IntPtr hWnd,
                int Msg,
                int wParam,
                IntPtr lParam);

            [StructLayout(LayoutKind.Sequential)]
            private struct RECT
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }

            private const int TTS_ALWAYSTIP = 0x01;
            private const int TTS_NOPREFIX = 0x02;
            private const int TTS_BALLOON = 0x40;
            private const int TTS_CLOSE = 0x80;

            private const int TTM_TRACKPOSITION = WM_USER + 18;
            private const int TTM_SETMAXTIPWIDTH = WM_USER + 24;
            private const int TTM_TRACKACTIVATE = WM_USER + 17;
            private const int TTM_ADDTOOL = WM_USER + 50;
            private const int TTM_SETTITLE = WM_USER + 33;

            private const int TTF_IDISHWND = 0x0001;
            private const int TTF_SUBCLASS = 0x0010;
            private const int TTF_TRACK = 0x0020;
            private const int TTF_ABSOLUTE = 0x0080;
            private const int TTF_TRANSPARENT = 0x0100;
            private const int TTF_CENTERTIP = 0x0002;
            private const int TTF_PARSELINKS = 0x1000;
            
            private const string TOOLTIPS_CLASS = "tooltips_class32";
            private const int WS_POPUP = unchecked((int)0x80000000);
            private const int WM_USER = 0x0400;
            private readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
            private const int SWP_NOSIZE = 0x0001;
            private const int SWP_NOMOVE = 0x0002;
            private const int SWP_NOACTIVATE = 0x0010;
            private const int SWP_NOZORDER = 0x0004;


            [StructLayout(LayoutKind.Sequential)]
            private struct TOOLINFO
            {
                public int cbSize;
                public int uFlags;
                public IntPtr hwnd;
                public IntPtr uId;
                public RECT rect;
                public IntPtr hinst;
                [MarshalAs(UnmanagedType.LPTStr)]
                public string lpszText;
                public uint lParam;
            }
             
            public tipM()
            {
                mTool = new mToolTip();
                mTool.DeActivate += new DeActivateEventHandler(this.Hide);
            }
       
            public tipM(Control parent)
            {
                mToolP = parent;
                mTool = new mToolTip();
                mTool.DeActivate += new DeActivateEventHandler(this.Hide);

            }

            ~tipM()
            {
                Dispose(false);
            }

            private bool disposed = false;
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!this.disposed)                   
                    Hide();
                disposed = true;
            }

            private void CreateTool()
            {
                System.Diagnostics.Debug.Assert(
                    mToolP.Handle != IntPtr.Zero,
                    "parent hwnd is null", "SetToolTip");

                CreateParams cp = new CreateParams();
                cp.ClassName = TOOLTIPS_CLASS;
                cp.Style =
                    WS_POPUP |
                    TTS_BALLOON |
                    TTS_NOPREFIX |
                    TTS_ALWAYSTIP |
                    TTS_CLOSE;

                mTool.CreateHandle(cp);

                ti = new TOOLINFO();
                ti.cbSize = Marshal.SizeOf(ti);

                ti.uFlags = TTF_TRACK |
                    TTF_IDISHWND |
                    TTF_TRANSPARENT |
                    TTF_SUBCLASS |
                    TTF_PARSELINKS;

                if (tipmPos)
                    ti.uFlags |= TTF_ABSOLUTE;
        

                if (tipmCen)
                    ti.uFlags |= TTF_CENTERTIP;
   

                ti.uId = mTool.Handle;
                ti.lpszText = tipmText;
                ti.hwnd = mToolP.Handle;

                GetClientRect(mToolP.Handle, ref ti.rect);
                ClientToScreen(mToolP.Handle, ref ti.rect);

                SetWindowPos(
                    mTool.Handle,
                    HWND_TOPMOST,
                    0, 0, 0, 0,
                    SWP_NOACTIVATE |
                    SWP_NOMOVE |
                    SWP_NOSIZE);

                IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ti));
                Marshal.StructureToPtr(ti, ptrStruct, true);

                SendMessage(
                    mTool.Handle, TTM_ADDTOOL, 0, ptrStruct);

                ti = (TOOLINFO)Marshal.PtrToStructure(ptrStruct,
                    typeof(TOOLINFO));

                SendMessage(
                    mTool.Handle, TTM_SETMAXTIPWIDTH,
                    0, new IntPtr(mtoolMax));

                IntPtr ptrTitle = Marshal.StringToHGlobalAuto(tipmTitle);

                SendMessage(
                    mTool.Handle, TTM_SETTITLE,
                    (int)tipmTI, ptrTitle);

                SetPos(ti.rect);

                Marshal.FreeHGlobal(ptrStruct);
                Marshal.FreeHGlobal(ptrTitle);
            }

            private void SetPos(RECT rect)
            {
                int x = 0, y = 0;

                switch (tipmAlign)
                {
                    case tipAlignment.TopLeft:
                        x = rect.left;
                        y = rect.top;
                        break;
                    case tipAlignment.TopMiddle:
                        x = rect.left + (rect.right / 2);
                        y = rect.top;
                        break;
                    case tipAlignment.TopRight:
                        x = rect.left + rect.right;
                        y = rect.top;
                        break;
                    case tipAlignment.LeftMiddle:
                        x = rect.left;
                        y = rect.top + (rect.bottom / 2);
                        break;
                    case tipAlignment.RightMiddle:
                        x = rect.left + rect.right;
                        y = rect.top + (rect.bottom / 2);
                        break;
                    case tipAlignment.BottomLeft:
                        x = rect.left;
                        y = rect.top + rect.bottom;
                        break;
                    case tipAlignment.BottomMiddle:
                        x = rect.left + (rect.right / 2);
                        y = rect.top + rect.bottom;
                        break;
                    case tipAlignment.BottomRight:
                        x = rect.left + rect.right;
                        y = rect.top + rect.bottom;
                        break;
                    default:
                        System.Diagnostics.Debug.Assert(false, "undefined enum", "default case reached");
                        break;
                }

                int pt = MAKELONG(x, y);
                IntPtr ptr = new IntPtr(pt);

                SendMessage(
                    mTool.Handle, TTM_TRACKPOSITION,
                    0, ptr);

            }

            private void Display(int show)
            {
                IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ti));
                Marshal.StructureToPtr(ti, ptrStruct, true);

                SendMessage(
                    mTool.Handle, TTM_TRACKACTIVATE,
                    show, ptrStruct);

                Marshal.FreeHGlobal(ptrStruct);
            }

            public void Hide()
            {
                Display(0);
                mTool.DestroyHandle();
            }

            private int MAKELONG(int loWord, int hiWord)
            {
                return (hiWord << 16) | (loWord & 0xffff);
            }


            public string Title
            {
                get { return tipmTitle; }
                set { tipmTitle = value; }
            }
                      
            public TooltipIcon TitleIcon
            {
                get { return tipmTI; }
                set { tipmTI = value; }
            }

         
            public string Text
            {
                get { return tipmText; }
                set { tipmText = value; }
            }
            
            public Control Parent
            {
                get { return mToolP; }
                set { mToolP = value; }
            }
     
            public tipAlignment Align
            {
                get { return tipmAlign; }
                set { tipmAlign = value; }
            }

          
            public bool UseAbsolutePositioning
            {
                get { return tipmPos;  }
                set { tipmPos = value; }
            }
         
            public bool CenterStem
            {
                get {  return tipmCen; }
                set { tipmCen = value; }
            }
      
            public void Show()
            {              
                Hide();
                CreateTool();
                Display(-1);
            }

        }
    }

    public class eTip
    {
        private Control m_parent;

        private string m_text;
        private string m_title;
        private TooltipIcon m_titleIcon = TooltipIcon.None;

        private const int ECM_FIRST = 0x1500;
        private const int EM_SHOWBALLOONTIP = ECM_FIRST + 3;

        [DllImport("User32", SetLastError = true)]
        private static extern int SendMessage(
            IntPtr hWnd,
            int Msg,
            int wParam,
            IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct editToolTip
        {
            public int cbStruct;
            public string pszTitle;
            public string pszText;
            public int ttiIcon;
        }

        public eTip()
        {
        }

        public eTip(Control parent)
        {
            m_parent = parent;
        }

             public void Show()
        {
            editToolTip ebt = new editToolTip();

            ebt.cbStruct = Marshal.SizeOf(ebt);
            ebt.pszText = m_text;
            ebt.pszTitle = m_title;
            ebt.ttiIcon = (int)m_titleIcon;

            IntPtr ptrStruct = Marshal.AllocHGlobal(Marshal.SizeOf(ebt));
            Marshal.StructureToPtr(ebt, ptrStruct, true);

            System.Diagnostics.Debug.Assert(m_parent != null, "Parent control is null", "Set parent before calling Show");

            int ret = SendMessage(m_parent.Handle,
                EM_SHOWBALLOONTIP,
                0, ptrStruct);

            Marshal.FreeHGlobal(ptrStruct);
        }

             public string Title
             {
                 get { return m_title; }
                 set { m_title = value; }
             }

             public TooltipIcon TitleIcon
             {
                 get { return m_titleIcon; }
                 set { m_titleIcon = value; }
             }


             public string Text
             {
                 get { return m_text; }
                 set { m_text = value; }
             }

             public Control Parent
             {
                 get { return m_parent; }
                 set { m_parent = value; }
             }

    }
}
