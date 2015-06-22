using UnityEngine;
using System;
using System.Collections;


public class StartManager : MonoBehaviour
{
	public void NextGo()
	{
		StartCoroutine(DelayCall(delegate
		{
			SceneManager.Instance.SetState(SceneState.Title);
		}, 1f));
	}
	
	IEnumerator DelayCall(Action action, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		action();
	}
}