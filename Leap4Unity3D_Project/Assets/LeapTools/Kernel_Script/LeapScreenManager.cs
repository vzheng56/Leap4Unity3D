/******************************************************************************\
* 嗨，我本来是拥护开源的 迟迟开源 还望见谅！                                  *
* Leap4Unity1.0  是在给的Sample的基础之上添加的功能和工具 目前只是简单的识别  *
* 你可以在帮助文档里看到这个工具的实用方法                                    *
* 如果你愿意 可以参与进来：http://www.enjoythis.net/forum.php                 * 
 * 如果你升级了它，欢迎你共享出来 如果你使用了它，                            *
 * 你可以使用到任何商业或者非商业途径                                         *
* 我们在翻译Leap帮助文档 我们在一起学习： QQ群： 94418644  一起吧！           *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;
public static class LeapScreenManager
{

    static Leap.Controller m_controller = new Leap.Controller();
    static Leap.Frame m_Frame = null;

    public static Leap.Frame Frame
    {
        get { return m_Frame; }
    }

    public static void ScreenUpdate()
    {
        if (m_controller != null)
        {
            Frame lastFrame = m_Frame == null ? Frame.Invalid : m_Frame;
            m_Frame = m_controller.Frame();
            ScreenList sl = m_controller.LocatedScreens;

        }
    }

    public static Vector3 IntersectToScreen(Pointable p)
    {
        Vector3 IntersectPoint = new Vector3();
        ScreenList sl = m_controller.LocatedScreens;
        IntersectPoint = sl[0].Intersect(p, true).ToUnity();
        return new Vector3(IntersectPoint.x * UnityEngine.Screen.width, IntersectPoint.y * UnityEngine.Screen.height, 1);
    }
}
