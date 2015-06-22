using UnityEngine;
using System.Collections;

public class SoundTestState : IState {

	public void Initialize ()
	{
		if(Application.loadedLevelName != "SoundTest")
		{
			Fade.Instance.FadeSceneLoad("SoundTest", Color.black, 1.0f, 0f);
		}
		Debug.Log("SoundTest::Initialize");
	}

	public void Update () {
		
	}

	public void Exit ()
	{

	}
}
