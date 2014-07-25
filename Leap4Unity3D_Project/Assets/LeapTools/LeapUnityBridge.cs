/******************************************************************************\
* Copyright (C) Leap Motion, Inc. 2011-2013.                                   *
* Leap Motion proprietary and  confidential.  Not for distribution.            *
* Use subject to the terms of the Leap Motion SDK Agreement available at       *
* https://developer.leapmotion.com/sdk_agreement, or another agreement between *
* Leap Motion and you, your company or other organization.                     *
\******************************************************************************/

using UnityEngine;
using System.Collections;
using Leap;
/// <summary>
/// Attach one of these to one of the objects in your scene to use Leap input.
/// It will take care of calling update on LeapInput and create hand objects
/// to represent the hand data in the scene using LeapUnityHandController.
/// It has a number of public fields so you can easily set the values from
/// the Unity inspector. Hands will 
/// </summary>
public class LeapUnityBridge : MonoBehaviour
{
	/// <summary>
	/// These values, set from the Inspector, set the corresponding fields in the
	/// LeapUnityExtension for translating vectors.
	/// </summary>
	public Vector3 m_LeapScaling = new Vector3(0.02f, 0.02f, 0.02f);
	public Vector3 m_LeapOffset = new Vector3(0,0,0);

    public bool EnableGesture = false;
    public bool EnableTransform = false;
    public bool EnableScale = false;
    public bool EnableRotate = false;

    public bool EnableVirtualKeyBoard = false;
    public bool EnableMouseControl = false;
	
	void Awake()
	{
		Leap.UnityVectorExtension.InputScale = m_LeapScaling;
		Leap.UnityVectorExtension.InputOffset = m_LeapOffset;
        Leap4Unity.EnableGesture = EnableGesture;
        VirtualMouseKeyboardContral.MouseControl.enableMouseControl = EnableMouseControl;
        VirtualMouseKeyboardContral.KeyBoardControl.enableKeyBoardControl = EnableVirtualKeyBoard;
	}

    void Update()
    {
        Leap.UnityVectorExtension.InputOffset = m_LeapOffset;
        Leap4Unity.Update();
        LeapScreenManager.ScreenUpdate();
    }
};
