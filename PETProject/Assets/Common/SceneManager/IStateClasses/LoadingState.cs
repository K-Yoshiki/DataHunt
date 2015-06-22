using UnityEngine;
using System.Collections;

public class LoadingState : IState {

	public void Initialize()
	{
		if (Application.loadedLevelName != "Loading")
		{
			Fade.Instance.FadeSceneLoad("Loading", Color.black, 1.0f, 0f, 1.5f);
		}
		Debug.Log("LoadingStage : Initialize");
	}

	public void Update () 
	{
	
	}

	public void Exit()
	{

	}
}
