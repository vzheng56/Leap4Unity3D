/******************************************************************************
* 嗨，我本来是拥护开源的 迟迟开源 还望见谅！                                  *
* Leap4Unity1.0  是在给的Sample的基础之上添加的功能和工具 目前只是简单的识别  *
* 你可以在帮助文档里看到这个工具的实用方法                                    *
* 如果你愿意 可以参与进来：http://www.enjoythis.net/forum.php                 * 
* 如果你升级了它，欢迎你共享出来 如果你使用了它，                             *
* 你可以使用到任何商业或者非商业途径                                          *
* 我们在翻译Leap帮助文档 我们在一起学习： QQ群： 94418644  一起吧！           *
/******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;
public class ShowArrowInScenes : MonoBehaviour
{
    public float disMoveInLeap = 60.0f;
    public Color32 CurrentDirClolr;
    public Color32 NoCurrentDirClolr;

    private bool isHandFound = false;
    private bool isDeviceFound = false;
    private Vector3 handPosition;
    private Vector3 handDirection;

    private GameObject Object_JianTou;
    private GameObject JiaTouChild_UP;
    private GameObject JiaTouChild_Down;
    private GameObject JiaTouChild_Left;
    private GameObject JiaTouChild_Right;
    private GameObject JiaTouChild_Center;

    private GameObject JiaTouChild_TurnLeft;
    private GameObject JiaTouChild_TurnRight;

    private Material Mat_JiaTouChild_UP;
    private Material Mat_JiaTouChild_Down;
    private Material Mat_JiaTouChild_Left;
    private Material Mat_JiaTouChild_Right;
    private Material Mat_JiaTouChild_Center;

    private Material Mat_JiaTouChild_TurnLeft;
    private Material Mat_JiaTouChild_TurnRight;

    // Use this for initialization
    void Start()
    {
        Object_JianTou = gameObject;
        //Find the Object_JianTou s Children
        JiaTouChild_UP = Object_JianTou.transform.FindChild("Plane_Up").gameObject;
        JiaTouChild_Down = Object_JianTou.transform.FindChild("Plane_Down").gameObject;
        JiaTouChild_Left = Object_JianTou.transform.FindChild("Plane_Left").gameObject;
        JiaTouChild_Right = Object_JianTou.transform.FindChild("Plane_Right").gameObject;
        JiaTouChild_Center = Object_JianTou.transform.FindChild("Plane_Center").gameObject;

        JiaTouChild_TurnLeft = Object_JianTou.transform.FindChild("Plane_TurnLeft").gameObject;
        JiaTouChild_TurnRight = Object_JianTou.transform.FindChild("Plane_TurnRight").gameObject;

        //get the Material
        Mat_JiaTouChild_UP = JiaTouChild_UP.renderer.material;
        Mat_JiaTouChild_Down = JiaTouChild_Down.renderer.material;
        Mat_JiaTouChild_Left = JiaTouChild_Left.renderer.material;
        Mat_JiaTouChild_Right = JiaTouChild_Right.renderer.material;
        Mat_JiaTouChild_Center = JiaTouChild_Center.renderer.material;

        Mat_JiaTouChild_TurnLeft = JiaTouChild_TurnLeft.renderer.material;
        Mat_JiaTouChild_TurnRight = JiaTouChild_TurnRight.renderer.material;

        Leap4Unity.HandFound += new Leap4Unity.HandFoundHandler(OnHandFound);
        Leap4Unity.HandLost += new Leap4Unity.ObjectLostHandler(OnHandLost);
        Leap4Unity.HandUpdated += new Leap4Unity.HandUpdatedHandler(OnHandUpdated);

        Leap4Unity.PointableUpdated += Leap4Unity_PointableUpdated;
        Leap4Unity.DeviceFound += new Leap4Unity.DeviceFoundHandler(LeapInput_DeviceFound);
        Leap4Unity.DeviceLost += new Leap4Unity.DeviceLostHandler(LeapInput_DeviceLost);
        //arrow_Up_Tex.a
    }

    void Leap4Unity_PointableUpdated(Pointable p)
    {
       // Debug.Log("Fingure Direction: " + p.Direction.ToUnity());
       // handDirection = p.Direction.ToUnity();
    }

    void LeapInput_DeviceLost(Device d)
    {
        //     isDeviceFound = false;
     //   Debug.Log("Lost");
        // SplashNoDevice();
    }

    void LeapInput_DeviceFound(Device d)
    {
        //   isDeviceFound = true;
        //   Debug.Log("Device Found");
        //   iTween.Stop();
    }

    void OnHandFound(Leap.Hand h)
    {
        isHandFound = true;
    }
    void OnHandLost(int id)
    {
        isHandFound = false;
    }

    void OnHandUpdated(Leap.Hand h)
    {
        handPosition = h.PalmPosition.ToUnity();
        //Debug.Log("Hand Direction:  " + h.PalmNormal.ToUnity());
       // handDirection = h.Direction.ToUnity();
        handDirection = h.PalmNormal.ToUnity();
        //目前这个手的方向获取的有问题 暂时使用 手指头的方向判断

    }

    void Update()
    {
        if (isHandFound)
        {
            if (handPosition.z > disMoveInLeap)
            {
                Mat_JiaTouChild_UP.color = CurrentDirClolr;
            }
            else
                Mat_JiaTouChild_UP.color = NoCurrentDirClolr;

            if (handPosition.z < -disMoveInLeap)
            {
                Mat_JiaTouChild_Down.color = CurrentDirClolr;
            }
            else
                Mat_JiaTouChild_Down.color = NoCurrentDirClolr;

            if (handPosition.x > disMoveInLeap)
            {
                Mat_JiaTouChild_Right.color = CurrentDirClolr;
            }
            else
                Mat_JiaTouChild_Right.color = NoCurrentDirClolr;

            if (handPosition.x < -disMoveInLeap)
            {
                Mat_JiaTouChild_Left.color = CurrentDirClolr;
            }
            else
                Mat_JiaTouChild_Left.color = NoCurrentDirClolr;
            //Hand Direction
            if (handDirection.x < -0.2f)
            {
                SplashTurnMateRial(JiaTouChild_TurnRight);
                Mat_JiaTouChild_TurnLeft.color = new Color(1, 1, 1, 0);
            }
            else if (handDirection.x > 0.2f)
            {
                SplashTurnMateRial(JiaTouChild_TurnLeft);
                Mat_JiaTouChild_TurnRight.color = new Color(1, 1, 1, 0);
            }
            else
            {
                Mat_JiaTouChild_TurnRight.color = new Color(1, 1, 1, 0);
                Mat_JiaTouChild_TurnLeft.color = new Color(1, 1, 1, 0);
            }


        }

        else
        {
            Mat_JiaTouChild_UP.color = NoCurrentDirClolr;
            Mat_JiaTouChild_TurnLeft.color = new Color(1, 1, 1, 0);
            Mat_JiaTouChild_Down.color = NoCurrentDirClolr;
            Mat_JiaTouChild_Left.color = NoCurrentDirClolr;

            Mat_JiaTouChild_TurnRight.color = new Color(1, 1, 1, 0);
            Mat_JiaTouChild_TurnLeft.color = new Color(1, 1, 1, 0);
        }
        //if (!isDeviceFound)
        //else

        //if (isHandFound)  Mat_JiaTouChild_Center.color = CurrentDirClolr;
    }
    Vector3 HandSmallChange()
    {
        Vector3 vex_Return = handPosition / 40;

        float x = Mathf.Clamp(vex_Return.x, -8, 8);
        float z = Mathf.Clamp(vex_Return.z, -8, 8);

        vex_Return.x = x;
        vex_Return.z = z;

        return vex_Return;
    }

    void SplashNoDevice()
    {

        //iTween.ValueTo(gameObject,iTween.Hash(
        iTween.ColorFrom(JiaTouChild_Center, iTween.Hash("Color", new Color(1, 1, 0.3f, .0f), "time", 1, "easetype", "linear", "looptype", "pingPong"));
        Mat_JiaTouChild_UP.color = new Color(1, 1, 1, 0);
        Mat_JiaTouChild_Down.color = new Color(1, 1, 1, 0);
        Mat_JiaTouChild_Left.color = new Color(1, 1, 1, 0);
        Mat_JiaTouChild_Right.color = new Color(1, 1, 1, 0);
    }

    void SplashTurnMateRial(GameObject mat_turn)
    {
        iTween.ColorTo(mat_turn, iTween.Hash("Color", new Color(1, 1, 1, .3f), "time", 1, "easetype", "linear"));
    }
}