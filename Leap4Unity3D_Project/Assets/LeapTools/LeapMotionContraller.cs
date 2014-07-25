using UnityEngine;
using System.Collections;
using Leap;
public class LeapMotionContraller : MonoBehaviour
{

    // Use this for initialization

    // public BookEdit bookInterface;
    public Camera currCamera;
    public Texture2D img;
    public GameObject cubeToshow;
    public Camera currentCamera;
    public LayerMask buttonMask;
    public Texture2D[] Texture_circleHand;


    private Vector3 handPosition;
    private float disMoveInLeap = 60;
    private Vector3 fingerPosition;
    private Vector3 fingerScreenPos;
    void Start()
    {
        Leap4Unity.HandUpdated += new Leap4Unity.HandUpdatedHandler(LeapInput_HandUpdated);
        Leap4Unity.PointableSwapGesture += new Leap4Unity.PointGestureHandler(LeapInput_PointableSwapGesture);
        Leap4Unity.PointableCircleGesture += new Leap4Unity.PointGestureHandler(LeapInput_PointableCircleGesture);
        Leap4Unity.PointableUpdated += new Leap4Unity.PointableUpdatedHandler(LeapInput_PointableUpdated);

        Leap4Unity.Sele_ShootGesture += new Leap4Unity.SelfMade_ShootHandle(LeapInput_Sele_ShootGesture);
    }

    void LeapInput_Sele_ShootGesture(bool b)
    {
    }
    Vector3 HandPoint = new Vector3();
    void LeapInput_PointableUpdated(Pointable p)
    {
        HandPoint = LeapScreenManager.IntersectToScreen(p);
    }

    void LeapInput_PointableCircleGesture(Gesture g)
    {
        CircleGesture cg = new CircleGesture(g);
        Vector3 cgNormal = cg.Normal.ToUnity();
        Debug.Log(cgNormal);
        if (cgNormal.z < -0.5f)
        {
        }
        if (cgNormal.z > 0.5f)
        {
        }
    }

    void LeapInput_PointableSwapGesture(Gesture g)
    {
        Leap.SwipeGesture sg = new SwipeGesture(g);
    }

    void LeapInput_HandUpdated(Leap.Hand h)
    {
    }

    // Update is called once per frame
    RaycastHit hitt;
    int int_HandTexIndex = 0;
    bool beginDraw = false;
    void Update()
    {


        Physics.Raycast(currentCamera.ScreenPointToRay(HandPoint), out hitt, 1000, buttonMask);
        if (hitt.collider != null)
        {
            showClrcleTex(); 
        }
        else
        {
            beginDraw = false;
            aa = 0;
        }

    }

    void OnGUI()
    {
        if (!beginDraw)
            GUI.DrawTexture(new Rect(HandPoint.x, UnityEngine.Screen.height - HandPoint.y, img.width * 2, img.height * 2), img);
        else
            GUI.DrawTexture(new Rect(HandPoint.x, UnityEngine.Screen.height - HandPoint.y, img.width * 2, img.height * 2), Texture_circleHand[int_HandTexIndex]);
    }

    int aa = 0;
    bool showClrcleTex()
    {
        aa += (int)(Time.deltaTime + 1) * 3;
        int_HandTexIndex = aa / 10;
        if (int_HandTexIndex > 16)
            int_HandTexIndex = 16;
        if (int_HandTexIndex > 15)
            beginDraw = false;
        else
            beginDraw = true;
        return beginDraw;
    }
}
