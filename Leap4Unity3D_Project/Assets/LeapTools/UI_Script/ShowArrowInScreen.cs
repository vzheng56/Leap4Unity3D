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
public class ShowArrowInScreen : MonoBehaviour
{

    public Texture2D arrow_Left_Tex;
    public Texture2D arrow_Right_Tex;
    public Texture2D arrow_Up_Tex;
    public Texture2D arrow_Down_Tex;
    public Texture2D arrow_Down_None;
    public Texture2D arrow_Down_Hand;
    public Texture2D arrow_Lost_Device;
    public Texture2D arrow_Found_Device;
    public float disMoveInLeap;


    private bool isHandFound = false;
    private bool isDeviceFound = false;
    private Vector3 handPosition;
	// Use this for initialization
	void Start () {
        Debug.Log("aaa");
        Leap4Unity.HandFound += new Leap4Unity.HandFoundHandler(OnHandFound);
        Leap4Unity.HandLost += new Leap4Unity.ObjectLostHandler(OnHandLost);
        Leap4Unity.HandUpdated += new Leap4Unity.HandUpdatedHandler(OnHandUpdated);

        Leap4Unity.DeviceFound += new Leap4Unity.DeviceFoundHandler(LeapInput_DeviceFound);
        Leap4Unity.DeviceLost += new Leap4Unity.DeviceLostHandler(LeapInput_DeviceLost);
	
	//arrow_Up_Tex.a
	}

    void LeapInput_DeviceLost(Device d)
    {
      //  isDeviceFound = false;
    }

    void LeapInput_DeviceFound(Device d)
    {
     //   isDeviceFound = true;
    }

    void OnHandFound(Leap.Hand h)
    {
        Debug.Log("hand Found");
        isHandFound = true;
    }
    void OnHandLost(int id)
    {
        isHandFound = false;
    }

    void OnHandUpdated(Leap.Hand h)
    {
        handPosition = h.PalmPosition.ToUnity();
    }


    void OnGUI()
    {


        Texture2D Tex_Up;
        Texture2D Tex_Down;
        Texture2D Tex_Left;
        Texture2D Tex_Right;
        if (handPosition.z > disMoveInLeap)
        {
            Tex_Up = arrow_Up_Tex;
        }
        else
            Tex_Up = arrow_Down_None;

        if (handPosition.z <-disMoveInLeap)
        {
            Tex_Down = arrow_Down_Tex;
        }
        else
            Tex_Down = arrow_Down_None;

        if (handPosition.x <-disMoveInLeap)
        {
            Tex_Left = arrow_Left_Tex;
        }
        else
            Tex_Left = arrow_Down_None;

        if (handPosition.x>disMoveInLeap)
        {
            Tex_Right = arrow_Right_Tex;
        }
        else
            Tex_Right = arrow_Down_None;


        GUI.DrawTexture(new Rect(UnityEngine.Screen.width - 70, UnityEngine.Screen.height - 100, 30, 30), Tex_Up, ScaleMode.StretchToFill, true);
        GUI.DrawTexture(new Rect(UnityEngine.Screen.width - 100, UnityEngine.Screen.height - 70, 30, 30), Tex_Left);
        GUI.DrawTexture(new Rect(UnityEngine.Screen.width - 40, UnityEngine.Screen.height - 70, 30, 30), Tex_Right);
        GUI.DrawTexture(new Rect(UnityEngine.Screen.width - 70, UnityEngine.Screen.height - 40, 30, 30), Tex_Down);
          
        if (!isDeviceFound)
            GUI.DrawTexture(new Rect(UnityEngine.Screen.width - 105, UnityEngine.Screen.height - 105, 100, 100), arrow_Lost_Device);
        else
            GUI.DrawTexture(new Rect(UnityEngine.Screen.width - 75, UnityEngine.Screen.height - 75, 40, 40), arrow_Found_Device);
        if (isHandFound)
            GUI.DrawTexture(new Rect(UnityEngine.Screen.width - 75 + HandSmallChange().x, UnityEngine.Screen.height - 75 - HandSmallChange().z, 40, 40), arrow_Down_Hand);
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
}
