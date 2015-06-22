using UnityEngine;
using System;
using System.Collections;


public class Fade : MonoSingleton<Fade>
{
	[SerializeField]
	FadeScreen screen;

	#region Fade Start Overload
	public void FadeStart(Color color, float time, float wait, Action midAct, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, "", time, wait, time, midAct, endAct);
	}

	public void FadeStart(Color color, string text, float time, float wait, Action midAct, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, text, time, wait, time, midAct, endAct);
	}

	public void FadeStart(Color color, float inSec, float wait, float outSec, Action midAct, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, "", inSec, wait, outSec, midAct, endAct);
	}

	public void FadeStart(Color color, string text, float inSec, float wait, float outSec, Action midAct, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, text, inSec, wait, outSec, midAct, endAct);
	}
	#endregion


	#region Fade Scene Load Overload
	public void FadeSceneLoad(string sceneName, Color color, float time, float wait, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, "", time, wait, time, SceneAct(sceneName), endAct);
	}

	public void FadeSceneLoad(string sceneName, string text, Color color, float time, float wait, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, text, time, wait, time, SceneAct(sceneName), endAct);
	}

	public void FadeSceneLoad(string sceneName, Color color, float inSec, float wait, float outSec, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, "", inSec, wait, outSec, SceneAct(sceneName), endAct);
	}

	public void FadeSceneLoad(string sceneName, string text, Color color, float inSec, float wait, float outSec, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, text, inSec, wait, outSec, SceneAct(sceneName), endAct);
	}

	public void FadeSceneLoad(int sceneIndex, Color color, float time, float wait, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, "", time, wait, time, SceneAct(sceneIndex), endAct);
	}

	public void FadeSceneLoad(int sceneIndex, string text, Color color, float time, float wait, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, text, time, wait, time, SceneAct(sceneIndex), endAct);
	}
	
	public void FadeSceneLoad(int sceneIndex, Color color, float inSec, float wait, float outSec, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, "", inSec, wait, outSec, SceneAct(sceneIndex), endAct);
	}

	public void FadeSceneLoad(int sceneIndex, string text, Color color, float inSec, float wait, float outSec, Action endAct = null)
	{
		GenerateScreen().FadeStart(color, text, inSec, wait, outSec, SceneAct(sceneIndex), endAct);
	}
	#endregion

	Action SceneAct(string sceneName)
	{
		return delegate {
			Application.LoadLevel(sceneName);
		};
	}

	Action SceneAct(int index)
	{
		return delegate {
			Application.LoadLevel(index);
		};
	}

	FadeScreen GenerateScreen()
	{
		return Instantiate(screen) as FadeScreen;
	}
}
