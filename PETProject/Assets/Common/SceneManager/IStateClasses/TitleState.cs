using UnityEngine;
using System.Collections;

public class TitleState : IState 
{
	public void Initialize()
	{
		if(Application.loadedLevelName != "Title")
		{
			Fade.Instance.FadeSceneLoad("Title", Color.black, 0f, 0f, 1.0f);
		}
		Debug.Log("TitleState : Initialize");
	}

	public void Update()
	{

	}

	public void Exit()
	{

	}
}
