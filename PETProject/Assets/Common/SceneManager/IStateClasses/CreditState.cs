using UnityEngine;
using System.Collections;

public class CreditState : IState
{
	public void Initialize()
	{
		if(Application.loadedLevelName != "Credit")
		{
			Fade.Instance.FadeSceneLoad("Credit",Color.black, 1.0f , 0.0f, 1.0f);
		}
		Debug.Log("StartState : Credit");

	}

	public void Update()
	{
		
	}
	
	public void Exit()
	{
		
	}
}
