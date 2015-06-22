using UnityEngine;
using System.Collections;

public class StartState : IState
{
	public void Initialize()
	{
		if(Application.loadedLevelName != "Start")
		{
			Application.LoadLevel("Start");
		}
		Debug.Log("StartState : Initialize");
	}

	public void Update()
	{

	}
	
	public void Exit()
	{

	}
}