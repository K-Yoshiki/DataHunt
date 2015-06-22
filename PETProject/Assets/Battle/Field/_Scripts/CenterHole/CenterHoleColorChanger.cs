using UnityEngine;
using System.Collections;


[RequireComponent(typeof(MeshRenderer))]
public class CenterHoleColorChanger : MonoBehaviour
{
	MeshRenderer rendererCache;
	Color defColor;

	void Awake()
	{
		rendererCache = GetComponent<MeshRenderer>();
		defColor = GetColor();
	}

	/// <summary>
	/// 色を変更する
	/// </summary>
	/// <param name="color">Color.</param>
	/// <param name="time">Time.</param>
	public void ChangeColor(Color color, float time)
	{
		StartCoroutine(MoveColor(color, time));
	}

	/// <summary>
	/// 元の色に戻す
	/// </summary>
	/// <param name="time">Time.</param>
	public void ResetColor(float time)
	{
		StartCoroutine(MoveColor(defColor, time));
	}

	/// <summary>
	/// 色変更コルーチン
	/// </summary>
	/// <returns>The color.</returns>
	/// <param name="targetColor">Target color.</param>
	/// <param name="time">Time.</param>
	IEnumerator MoveColor(Color targetColor, float time)
	{
		Color nowColor = GetColor();
		targetColor.a = nowColor.a;

		if (time <= 0f)
		{
			SetColor(targetColor);
			yield break;
		}

		Color moveAmount = (targetColor - nowColor) / time;

		while(time > 0f)
		{
			nowColor += moveAmount * Time.deltaTime;
			SetColor(nowColor);
			time -= Time.deltaTime;
			yield return null;
		}

		SetColor(targetColor);
	}

	Color GetColor()
	{
		return rendererCache.material.GetColor("_TintColor");
	}

	void SetColor(Color color)
	{
		rendererCache.material.SetColor("_TintColor", color);
	}
}
