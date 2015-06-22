using UnityEngine;
using System.Collections;


public class LabState : IState
{
	public void Initialize()
	{
		if (Application.loadedLevelName != "Lab")
		{
			Fade.Instance.FadeSceneLoad("Lab", Color.black, 1.0f, 0f, 1.5f);
		}
		Debug.Log("LabStage : Initialize");
	}

	public void Update()
	{
	}

	public void Exit()
	{
	}
}
