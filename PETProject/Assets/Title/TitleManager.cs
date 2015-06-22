using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;


public class TitleManager : MonoBehaviour
{
	GameObject titleUI;
	Text titleText;
	float alphaSpeed;
	float speed;
	Color startButtonColor;
	AttachedTween titleButtonTween;

	void OnEnable()
	{
		titleUI = gameObject.transform.FindChild("TitlePanel").gameObject;
		titleText = titleUI.transform.FindChild("Button/Text").GetComponent<Text>();
		titleButtonTween = titleText.GetComponent<AttachedTween>();
		startButtonColor = titleText.color;
		speed = 1;
	}

	void Update()
	{
		alphaSpeed = Mathf.Abs(Mathf.Sin(Time.time * speed));
		titleText.color = startButtonColor * alphaSpeed;

		ButtonFade();
	}

	public void OnClick()
	{
		Debug.Log("OnClick");
		
		StartCoroutine("Flash");
	}

	void NextGo()
	{
		StartCoroutine(DelayCall(delegate {
			SceneManager.Instance.SetState(SceneState.Home);
		}, 0f));
	}
		
	IEnumerator DelayCall(Action action, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		action();
	}

	IEnumerator Flash()
	{
		titleButtonTween.Execute();
		
		yield return new WaitForSeconds(0.5f);

		NextGo();
	}

	void ButtonFade()
	{
		speed = 2;
		alphaSpeed = Mathf.Abs(Mathf.Sin(Time.time * speed));
		titleText.color = startButtonColor * alphaSpeed;

	}
}