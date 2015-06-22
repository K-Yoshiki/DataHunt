using UnityEngine;
using System;
using System.Collections;
using AppUtils;

/// <summary>
/// フェーズの表示アニメーション実行クラス
/// </summary>
public class PhaseLabel : MonoBehaviour
{
	public bool onTimeEnd;
	public float time;
	public AudioClip sound;
	public float soundDelay;
	public event Action OnCallBack;
	
	private void Start()
	{
		OnCallBack += delegate{};
		if (onTimeEnd)
		{
			Destroy(this.gameObject, time);
		}
		StartCoroutine(PlaySound());
	}

	public void End()
	{
		Destroy(this.gameObject);
	}
	
	void OnDestroy()
	{
		OnCallBack();
	}

	IEnumerator PlaySound()
	{
		if (sound == null) yield break;
		if (soundDelay > 0f)
			yield return new WaitForSeconds(soundDelay);
		Sound.Instance.PlaySE(sound);
	}
}
