/******************************************************************************\
* 嗨，我本来是拥护开源的 迟迟开源 还望见谅！                                  *
* Leap4Unity1.0  是在给的Sample的基础之上添加的功能和工具 目前只是简单的识别  *
* 你可以在帮助文档里看到这个工具的实用方法                                    *
* 如果你愿意 可以参与进来：http://www.enjoythis.net/forum.php                 * 
* 如果你升级了它，欢迎你共享出来 如果你使用了它，                             *
* 你可以使用到任何商业或者非商业途径                                          *
* 我们在翻译Leap帮助文档 我们在一起学习： QQ群： 94418644  一起吧！           */
using System;
using System.Threading;
using System.Runtime.InteropServices;
//using System.Windows.Forms;

namespace VirtualMouseKeyboardContral
{
    class MouseControl
    {
        public static bool enableMouseControl;
        /// <summary>
        /// 鼠标控制参数
        /// </summary>
        const int MOUSEEVENTF_LEFTDOWN = 0x2;
        const int MOUSEEVENTF_LEFTUP = 0x4;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x20;
        const int MOUSEEVENTF_MIDDLEUP = 0x40;
        const int MOUSEEVENTF_MOVE = 0x1;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        const int MOUSEEVENTF_RIGHTDOWN = 0x8;
        const int MOUSEEVENTF_RIGHTUP = 0x10;

        /// <summary>
        /// 鼠标的位置
        /// </summary>
        public struct PONITAPI
        {
            public int x, y;
        }

        [DllImport("user32.dll")]
        public static extern int GetCursorPos(ref PONITAPI p);

        [DllImport("user32.dll")]
        public static extern int SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        //获取鼠标的坐标 并返回是够获得
        public static bool  GetMousePosition(ref PONITAPI p)
        {
            if (1 == GetCursorPos(ref p))
                return true;
            else
                return false;
        }

        public static bool SetMousePosition(int x, int y)
        {
            if (!enableMouseControl) return false;
            if (1 == SetCursorPos(x, y))
                return true;
            else
                return false;
        }

        public static void LeftMouseButtonDown(PONITAPI p)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, p.x, p.y, 0, 0);
        }

        public static void LeftMouseButtonUp(PONITAPI p)
        {
            mouse_event(MOUSEEVENTF_LEFTUP, p.x, p.y, 0, 0);
        }

        public static void RightMouseButtonDown(PONITAPI p)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, p.x, p.y, 0, 0);
        }

        public static void RightMouseButtonUp(PONITAPI p)
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, p.x, p.y, 0, 0);
        }
    }
    class KeyBoardControl
    {
    public static bool  enableKeyBoardControl=false;
       
        /// <summary>
        /// 导入键盘事件和方法
        /// </summary>
        [DllImport("USER32.DLL")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        //W
        public static void PressButtonDown_W()
        {
                keybd_event(0x57, 0, 0, 0);
        }

        public static void PressButtonUp_W()
        {
            if (enableKeyBoardControl)
                keybd_event(0x57, 0, 0x0002, 0);
        }
        //A
        public static void PressButtonDown_A()
        {
            if (enableKeyBoardControl)
                keybd_event(0x41, 0, 0, 0);
        }

        public static void PressButtonUp_A()
        {
            if (enableKeyBoardControl)
                keybd_event(0x41, 0, 0x0002, 0);
        }

        //D
        public static void PressButtonDown_D()
        {
            if (enableKeyBoardControl)
                keybd_event(0x44, 0, 0, 0);
        }

        public static void PressButtonUp_D()
        {
            if (enableKeyBoardControl)
                keybd_event(0x44, 0, 0x0002, 0);
        }
        //S
        public static void PressButtonDown_S()
        {
            if (enableKeyBoardControl)
                keybd_event(0x53, 0, 0, 0);
        }

        public static void PressButtonUp_S()
        {
            if (enableKeyBoardControl)
                keybd_event(0x53, 0, 0x0002, 0);
        }
    }

}