using UnityEngine;
using System.Collections;


public class HomeState : IState
{
	public void Initialize()
	{
		if (Application.loadedLevelName != "Home")
		{
			Fade.Instance.FadeSceneLoad("Home", Color.black, 1.0f, 0f, 1.0f);
		}
		Debug.Log("HomeState : Initialize");
	}

	public void Update()
	{
	}

	public void Exit()
	{
	}
}