using UnityEngine;
using System;
using System.Collections;

public class StateDebug : MonoBehaviour
{
#if UNITY_EDITOR_OSX || UNITY_EDITOR
	public string changeState;
	[InspecterButton("Change State")]
	public bool
		swichFlag = false;

	// Use this for initialization
	void Start()
	{
		swichFlag = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		if (swichFlag && changeState != "")
		{
			swichFlag = false;
			ChangeState();
		}
	}

	void ChangeState()
	{
		SceneState state;
		try
		{
			state = (SceneState)Enum.Parse(typeof(SceneState), changeState);
		}
		catch
		{
			return;
		}
		SceneManager.Instance.SetState(state);
	}
#endif
}
