using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;


public class FadeScreen : MonoBehaviour
{
	[SerializeField]
	Image image;

	[SerializeField]
	Text label;

	public void FadeStart(Color color, string text, float inSec, float waitSec, float outSec, Action midAct, Action endAct)
	{
		label.text = text;
		image.color = color;
		StartCoroutine(FadeAnimation(inSec, waitSec, outSec, midAct, endAct));
	}

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	IEnumerator FadeAnimation(float inSec, float waitSec, float outSec, Action midAct, Action endAct)
	{
		float fadeAmount;
		SetAlpha(image, 0f);
		SetAlpha(label, 0f);
		inSec = Mathf.Abs(inSec);
		outSec = Mathf.Abs(outSec);

		// Fade Out
		fadeAmount = 1.0f / inSec;
		yield return StartCoroutine(AlphaMove(inSec, 0f, fadeAmount));
		SetAlpha(image, 1f);
		SetAlpha(label, 1f);

		// Wait
		if (waitSec > 0f)
			yield return new WaitForSeconds(waitSec);

		// Next Phase or Next Scene
		if (midAct != null) midAct();

		// Fade In
		fadeAmount = 1.0f / outSec;
		yield return StartCoroutine(AlphaMove(outSec, 1f, -fadeAmount));
		SetAlpha(image, 0f);
		SetAlpha(label, 0f);

		// End Call Back
		if (endAct != null) endAct();
		
		// Self Destroy
		Destroy(this.gameObject);
	}

	IEnumerator AlphaMove(float sec, float alpha, float amount)
	{
		while (sec > 0f)
		{
			sec -= Time.deltaTime;
			alpha += amount * Time.deltaTime;
			SetAlpha(image, alpha);
			SetAlpha(label, alpha);
			yield return null;
		}
	}

	void SetAlpha(Graphic colorTarget, float alpha)
	{
		Color temp = colorTarget.color;
		temp.a = alpha;
		colorTarget.color = temp;
	}
}