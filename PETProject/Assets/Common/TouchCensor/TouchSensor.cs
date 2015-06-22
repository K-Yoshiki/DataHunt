using UnityEngine;
using System;
using System.Collections;


public struct TouchInfo
{
	public Vector2 position;
	public Vector2 deltaPosition;
	public Vector2 rawPosition;

	public TouchInfo(Vector2 position, Vector2 daltaPosition, Vector2 rawPosition)
	{
		this.position = position;
		this.deltaPosition = daltaPosition;
		this.rawPosition = rawPosition;
	}
}

/// <summary>
/// Touch Event Manager Class
/// </summary>
public class TouchSensor : MonoBehaviour
{
	public static event Action<TouchInfo> EnterEvent;
	public static event Action<TouchInfo> StayEvent;
	public static event Action<TouchInfo> ExitEvent;
	public static event Action<TouchInfo> SwipeEvent;
	
	bool isTouchExecute;
	
	void Start()
	{
		EnterEvent += delegate{};
		StayEvent += delegate{};
		ExitEvent += delegate{};
		SwipeEvent += delegate{};
		
		isTouchExecute = false;
		StartCoroutine(MouseDebugger());
	}
	
	void Update()
	{
		if (Input.touchCount >= 1)
		{
			SingleFinger(Input.GetTouch(0));
			isTouchExecute = true;
		}
	}
	
	void SingleFinger(Touch touch)
	{
		if (touch.phase == TouchPhase.Began)
		{
			EnterEvent(new TouchInfo(touch.position, touch.deltaPosition, touch.rawPosition));
		}
		else if (touch.phase == TouchPhase.Stationary)
		{
			StayEvent(new TouchInfo(touch.position, touch.deltaPosition, touch.rawPosition));
		}
		else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
		{
			ExitEvent(new TouchInfo(touch.position, touch.deltaPosition, touch.rawPosition));
		}
		else if (touch.phase == TouchPhase.Moved)
		{
			SwipeEvent(new TouchInfo(touch.position, touch.deltaPosition, touch.rawPosition));
		}
	}
	
	IEnumerator MouseDebugger()
	{
		Debug.Log("TouchCensor - Start MouseDebugger()");
		TouchInfo touch = new TouchInfo();
		Vector2 oldPos = Vector2.zero;
		while(true)
		{
			oldPos = touch.position;
			touch.position = Input.mousePosition;
			touch.deltaPosition = touch.position - oldPos;

			if (isTouchExecute)
			{
				isTouchExecute = false;
			}
			else if(Input.GetKeyDown(KeyCode.Mouse0))
			{
				EnterEvent(touch);
			}
			else if(Input.GetKeyUp(KeyCode.Mouse0))
			{
				ExitEvent(touch);
			}
			else if(Input.GetKey(KeyCode.Mouse0))
			{
				if(oldPos == touch.position)
				{
					StayEvent(touch);
				}
				else
				{
					SwipeEvent(touch);
				}
			}
			yield return 0;
		}
	}
}