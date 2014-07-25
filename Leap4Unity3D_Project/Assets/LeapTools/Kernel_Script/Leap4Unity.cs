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
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Leap;

public static class Leap4Unity
{
    public static bool EnableTranslation = true;
    public static bool EnableRotation = true;
    public static bool EnableScaling = false;
    public static bool EnableGesture = false;


    public delegate void PointableFoundHandler(Pointable p);
    public delegate void PointableUpdatedHandler(Pointable p);
    public delegate void HandFoundHandler(Hand h);
    public delegate void HandUpdatedHandler(Hand h);
    public delegate void DeviceFoundHandler(Device d);
    public delegate void DeviceLostHandler(Device d);
    public delegate void ObjectLostHandler(int id);

    public delegate void PointGestureHandler(Gesture g);


    public delegate void SelfMade_ShootHandle(bool b);

    public static event PointableFoundHandler PointableFound;
    public static event PointableUpdatedHandler PointableUpdated;
    public static event ObjectLostHandler PointableLost;

    public static event HandFoundHandler HandFound;
    public static event HandUpdatedHandler HandUpdated;
    public static event ObjectLostHandler HandLost;

    public static event DeviceFoundHandler DeviceFound;
    public static event DeviceLostHandler DeviceLost;

    public static event PointGestureHandler PointableCircleGesture;
    public static event PointGestureHandler PointableSwapGesture;
    public static event PointGestureHandler PointableKeyTapGesture;
    public static event PointGestureHandler PointableScreenTapGesture;

    public static event SelfMade_ShootHandle Sele_ShootGesture;

    public static Leap.Frame Frame
    {
        get { return m_Frame; }
    }

    public static void Update()
    {
        if (m_controller != null)
        {
            Frame lastFrame = m_Frame == null ? Frame.Invalid : m_Frame;
            m_Frame = m_controller.Frame();

            DispatchLostEvents(Frame, lastFrame);
            DispatchFoundEvents(Frame, lastFrame);
            DispatchUpdatedEvents(Frame, lastFrame);
            DispatchSeleGestureUpdateEvent(Frame, lastFrame);
            if (EnableGesture && m_Frame.Fingers.Count<=2)
                DispatchGestureFoundEvents(Frame, lastFrame);
        }
    }

    //*********************************************************************
    // Private data & functions
    //*********************************************************************
    private enum HandID : int
    {
        Primary = 0,
        Secondary = 1
    };


    static Leap.Controller m_controller = new Leap.Controller();
    static Leap.Frame m_Frame = null;
    private static void DispatchLostEvents(Frame newFrame, Frame oldFrame)
    {
        foreach (Hand h in oldFrame.Hands)
        {
            if (!h.IsValid)
                continue;
            if (!newFrame.Hand(h.Id).IsValid && HandLost != null)
                HandLost(h.Id);
        }
        foreach (Pointable p in oldFrame.Pointables)
        {
            if (!p.IsValid)
                continue;
            if (!newFrame.Pointable(p.Id).IsValid && PointableLost != null)
                PointableLost(p.Id);
        }

        foreach (Device d in m_controller.Devices)
        {
            if(d.IsValid&&DeviceLost!=null)
                DeviceLost(d);
        }
    }

   static Device oldDevice = new Device();
    private static void DispatchFoundEvents(Frame newFrame, Frame oldFrame)
    {
        foreach (Hand h in newFrame.Hands)
        {
            if (!h.IsValid)
                continue;
            if (!oldFrame.Hand(h.Id).IsValid && HandFound != null)
                HandFound(h);
        }
        foreach (Pointable p in newFrame.Pointables)
        {
            if (!p.IsValid)
                continue;
            if (!oldFrame.Pointable(p.Id).IsValid && PointableFound != null)
                PointableFound(p);
        }

        foreach (Device d in m_controller.Devices)
        {
            if (!d.IsValid)
                continue;
            else if (!d.Equals(oldDevice) && DeviceFound != null)
            {
                DeviceFound(d);
            }
            Debug.Log(d.ToString());
            oldDevice = d;
            Debug.Log(d.Equals(oldDevice));
        }

    }
    private static void DispatchUpdatedEvents(Frame newFrame, Frame oldFrame)
    {
        foreach (Hand h in newFrame.Hands)
        {
            if (!h.IsValid)
                continue;
            if (oldFrame.Hand(h.Id).IsValid && HandUpdated != null)
                HandUpdated(h);
        }
        foreach (Pointable p in newFrame.Pointables)
        {
            if (!p.IsValid)
                continue;
            if (oldFrame.Pointable(p.Id).IsValid && PointableUpdated != null)
                PointableUpdated(p);
        }
    }

    private static void DispatchGestureFoundEvents(Frame newFrame,Frame oldFrame)
    {
        //Open Gesture Enable
        if (! m_controller.IsGestureEnabled(Gesture.GestureType.TYPECIRCLE))
        {
            m_controller.EnableGesture(Gesture.GestureType.TYPECIRCLE); 
        }

        if (!m_controller.IsGestureEnabled(Gesture.GestureType.TYPESWIPE))
        {
            m_controller.EnableGesture(Gesture.GestureType.TYPESWIPE);
        }

        if (!m_controller.IsGestureEnabled(Gesture.GestureType.TYPESCREENTAP)||
            !m_controller.IsGestureEnabled(Gesture.GestureType.TYPEKEYTAP)
            )
        {
            m_controller.EnableGesture(Gesture.GestureType.TYPESCREENTAP);
            m_controller.EnableGesture(Gesture.GestureType.TYPEKEYTAP);
        }




        GestureList gestures = oldFrame.IsValid?
                                newFrame.Gestures(oldFrame) :
                                newFrame.Gestures();

        foreach (Gesture g in gestures)
        {
            Debug.Log("Gesture  Type:  "+g.Type);

            #region Gesture_Circle
            if (g.Type == Leap.Gesture.GestureType.TYPECIRCLE)
            {

                if (g != null)
                {
                    CircleGesture cg = new CircleGesture(g);

                    float progress = cg.Progress;
                    if (Gesture.GestureState.STATESTOP == cg.State)
                    {
                        PointableCircleGesture(cg);
                    }
                }
            }
            #endregion    

            #region Gesture_Swap
            if (g.Type == Leap.Gesture.GestureType.TYPESWIPE)
            {

                if (g != null)
                {
                    SwipeGesture sg = new SwipeGesture(g);

                    if (Gesture.GestureState.STATESTOP== sg.State)
                    {
                        PointableSwapGesture(sg);
                    }
                }
            }
            #endregion

            #region Gesture_KeyTap
            if (g.Type == Leap.Gesture.GestureType.TYPEKEYTAP)
            {

                if (g != null)
                {
                    KeyTapGesture ktg = new KeyTapGesture(g);
                    Debug.Log("Gesture  Type: Send ");
                    if (Gesture.GestureState.STATESTOP == ktg.State)
                    {
                        PointableKeyTapGesture(ktg);
                    }
                }
            }
            #endregion

            #region Gesture_ScreenTap
            if (g.Type == Leap.Gesture.GestureType.TYPESCREENTAP)
            {

                if (g != null)
                {
                    ScreenTapGesture stg = new ScreenTapGesture(g);

                    if (Gesture.GestureState.STATESTOP == stg.State)
                    {
                        PointableScreenTapGesture(stg);
                    }
                }
            }
            #endregion
        }
    }

    private static void DispatchSeleGestureUpdateEvent(Frame newFrame, Frame oldFrame)
    {
        if (1 == newFrame.Hands.Count)
        {
            PointableList pointableList = newFrame.Hands.Frontmost.Pointables;
            if (2 == pointableList.Count)
            {
                if (Vector3.Distance(pointableList[0].TipPosition.ToUnity(), pointableList[1].TipPosition.ToUnity()) < 40
                    &&Sele_ShootGesture!=null)
                {
                    Sele_ShootGesture(true);
                }

                if (Vector3.Distance(pointableList[0].TipPosition.ToUnity(), pointableList[1].TipPosition.ToUnity()) >80
    && Sele_ShootGesture != null)
                {
                    Sele_ShootGesture(false);
                }
                
            }
        }
    }


#if UNITY_STANDALONE_WIN
    [DllImport("mono", SetLastError = true)]
    static extern void mono_thread_exit();
#endif

   static void OnApplicationQuit()
    {
        m_controller.Dispose();
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR && UNITY_3_5
    mono_thread_exit ();
#endif
    }
}